using BL;
using Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.ComponentModel;
using System.Data;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly IPodService _podService;
        private readonly ICategoryService _categoryService;
        private Podcast? selectedPodcast;
        private PodCard? selectedPodCard;
        private BindingList<Category>? _categoryDataSource;
        private readonly AppSettings appSettings;
        private readonly PodUpdater podUpdater;


        public MainForm(IPodService podService, ICategoryService categoryService)
        {
            InitializeComponent();

            _podService = podService;
            _categoryService = categoryService;
            appSettings = SettingsSerializer.Deserialize() ?? new AppSettings(UpdateInterval.OneDay);
            podUpdater = new PodUpdater(appSettings);
            podUpdater.OnUpdatePodcasts += UpdatePodcasts;

            //Satt förut på this.Load
            LoadPodcast();
            InitCategoriesAsync();
            InitCbUpdateFrequency();

            //Knappen ska vara osynlig tills man hämtat en RSS som inte finns i DB
            btnSave.Visible = false;
            btnDelete.Visible = false;

            //Instansiera CategoryPanel och hantera events
            CategoryPanel categoryPanel = new CategoryPanel();

            //Koppla på metoder från categoryService till categoryPanel's delegat
            categoryPanel.GetAllCategoriesAsync = categoryService.GetAllAsync;
            categoryPanel.CategoryExistsAsync = categoryService.CategoryExistsAsync;

            categoryPanel.OnCategoryAdded += async (toAdd) => {
                _categoryDataSource.Add(toAdd);

                //Lägg till i DB
                await categoryService.InsertAsync(toAdd);

                //Säg till podpanel
                RefreshPodPanelCategoryDataSource();

            };

            categoryPanel.OnCategoryRemoved += async (toRemove) => {

                //Tar bort från comboboxen :)
                Category listCategoryToRemove = _categoryDataSource.Where(category => category.Id == toRemove.Id).FirstOrDefault();
                if (listCategoryToRemove != null) { 
                    _categoryDataSource.Remove(listCategoryToRemove);
                }

                //Ta bort från DB
                await categoryService.DeleteAsync(toRemove);

                //Tar bort en borttagen kategori från PodCards som hade den innan,
                //samt sätter ObjectId för Podcasten till ObjectId.Empty
                flpMyPods.Controls
                    .OfType<PodCard>()
                    .Where(pc => pc.Podcast.Category == toRemove.Id)
                    .ToList()
                    .ForEach(async pc => { 
                        pc.SetCategoryText("");
                        await _podService.UpdateCategoryAsync(pc.Podcast, ObjectId.Empty);
                    });

                //Säg till podpanel
                RefreshPodPanelCategoryDataSource();


                cbCategories.SelectedIndex = 0;
                LoadPodcast();

            };

            categoryPanel.OnCategoryTextChanged += async (changedCategory) =>
            {
                //Reflekterar nya kategorin i comboboxen
                Category toChange = _categoryDataSource.Where(category => category.Id == changedCategory.Id).FirstOrDefault();
                if (toChange != null) {
                    toChange.Text = changedCategory.Text;            
                    //Den här raden löste det, men kanske läsa lite på varför...?
                    _categoryDataSource.ResetBindings();
                }

                //Ändrar kategori-texten på PodCards till vänster
                flpMyPods.Controls
                    .OfType<PodCard>()
                    .Where(pc => pc.Podcast.Category == changedCategory.Id)
                    .ToList()
                    .ForEach(pc => pc.SetCategoryText(changedCategory.Text));

                //Ändra i DB
                await categoryService.ReplaceAsync(changedCategory);

                //Tala om för podpanel att ändra sin kategori
                RefreshPodPanelCategoryDataSource();
            };

            pCategoryPanel.Controls.Add(categoryPanel);
        }


        private void InitCbUpdateFrequency() {
            cbUpdateFreq.SelectionChangeCommitted += (s, e) =>
            {
                if (cbUpdateFreq.SelectedValue is UpdateInterval selected &&
                    selected != appSettings.UpdateInterval) {
                    appSettings.SetUpdateInterval(selected);
                    //Skriv till .json
                    SettingsSerializer.Serialize(appSettings);
                }
            };

            //Fyller comboboxen med värden från enum
            var items = UpdateIntervalExtensions.Values
                .Select(updateInterval => new {         //Anonymt objekt
                    Value = updateInterval,
                    Name = updateInterval.ToDisplayString()
                })
                .ToList();

            cbUpdateFreq.DisplayMember = "Name"; //Vad som visas i comboboxen
            cbUpdateFreq.ValueMember = "Value";  //Vad som returneras av combobox.SelectedValue
            cbUpdateFreq.DropDownStyle = ComboBoxStyle.DropDownList;

            cbUpdateFreq.DataSource = items;
            cbUpdateFreq.SelectedValue = appSettings.UpdateInterval;
        }

        private async void InitCategoriesAsync() {
            List<Category> allCategories = await _categoryService.GetAllAsync();
            _categoryDataSource = new BindingList<Category>(allCategories);


            allCategories.Insert(0, new Category { Id = ObjectId.Empty, Text = "Alla Poddar" });
            cbCategories.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategories.DataSource = _categoryDataSource;


            //HÄR HÄNDER VÄLJANDET AV ETT ITEM I COMBOBOX
            cbCategories.SelectionChangeCommitted += async (s, e) =>
            {
                if (cbCategories.SelectedIndex == 0) {
                    LoadPodcast();
                }
                else
                {
                    Category? selectedCategory = cbCategories.SelectedItem as Category;
                    if (selectedCategory != null) {
                        List<Podcast> sortedByCategory = await _podService.GetByCategoryAsync(selectedCategory.Id);

                        ClearAndRefreshPodList(sortedByCategory);
                    }
                }
            };
        }

        private async void LoadPodcast()
        {
            List<Podcast> allaPoddar = await _podService.GetAllAsync();

            ClearAndRefreshPodList(allaPoddar);

            if (allaPoddar.Count > 0)
                DisplayPodPanel(allaPoddar.First());
        }

        private async void DisplayPodPanel(Podcast podcast)
        {
            PodPanel toShow = new PodPanel(podcast, _categoryDataSource);
            pPodPanel.Controls.Clear();
            pPodPanel.Controls.Add(toShow);
            toShow.OnTitleChanged += ReflectTitleChange;
            toShow.OnCategoryChanged += ReflectCategoryChange;

            selectedPodcast = podcast;

            HandlePodCardSelection(podcast);

            //Kollar om podcasten finns i DB för att visa rätt knappar
            if (await _podService.RssExistsAsync(podcast.RssUrl))
            {
                btnSave.Visible = false;
                btnDelete.Visible = true;
            }
            else
            {
                btnSave.Visible = true;
                btnDelete.Visible = false;
            }
        }

        private async void btnGetRss_Click(object sender, EventArgs e)
        {
            if (await _podService.RssExistsAsync(tbRssUrl.Text)) {
                PodCard? alreadyExists = FindPodCardByRss(tbRssUrl.Text);
                if (alreadyExists != null) {
                    DisplayPodPanel(alreadyExists.Podcast);
                    flpMyPods.ScrollControlIntoView(alreadyExists);
                }
            }
            else { 
                Podcast? pendingPodcast = await _podService.FetchPodFromRssAsync(tbRssUrl.Text);
                if (pendingPodcast != null)
                {
                    DisplayPodPanel(pendingPodcast);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedPodcast != null)
            {
                await _podService.InsertAsync(selectedPodcast);
                
                //Växla synlighet på knapparna
                (btnDelete.Visible, btnSave.Visible) = (btnSave.Visible, btnDelete.Visible);
                PodCard podCard = CreateAndAddPodCard(selectedPodcast);
                flpMyPods.ScrollControlIntoView(podCard); //Scrollar ner till senast tillagda podcard
                HandlePodCardSelection(podCard.Podcast);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(selectedPodcast != null)
            {
                await _podService.DeleteAsync(selectedPodcast);
                PodCard? toRemove = FindPodCardByRss(selectedPodcast.RssUrl);
                if (toRemove != null)
                {
                    int index = flpMyPods.Controls.IndexOf(toRemove);
                    flpMyPods.Controls.Remove(toRemove);
                    DisplayAfterDelete(index);
                }
            }
        }

        private void DisplayAfterDelete(int index) {
            if (flpMyPods.Controls.Count == 0) {
                btnSave.Visible = false;
                btnDelete.Visible = false;
                pPodPanel.Controls.Clear(); //Om flpMyPods är tom så visas ingenting i högra panelen
                return;
            }
            else if (index == flpMyPods.Controls.Count)
            {
                index--;
            }
            PodCard toDisplay = flpMyPods.Controls.OfType<PodCard>().ToList()[index];
            DisplayPodPanel(toDisplay.Podcast);
        }

        private async void ReflectTitleChange(object? sender, EventArgs e) {
            if (sender is PodPanel senderPanel) {
                var saveSucceeded = await _podService.UpdateTitleAsync(senderPanel.Podcast, senderPanel.PodTitle);
                if (saveSucceeded) {                         
                    PodCard? toChange = FindPodCardByRss(senderPanel.Podcast.RssUrl);
                    if (toChange != null) { 
                        //Ändrar objektet i minnet
                        toChange.Podcast.Title = senderPanel.Podcast.Title;
                        //Ändrar labeln i GUI:t
                        toChange.TitleLabel.Text = senderPanel.Podcast.Title;
                        senderPanel.Refresh();
                    }
                }
            }
        }

        private async void ReflectCategoryChange(Podcast changedPodcast) {
            PodCard? toChange = FindPodCardById(changedPodcast.Id);

            if (toChange == null) {

                return; //toChange finns i DB ännu
            }

            Category newCategory = _categoryDataSource
                .Where(c => c.Id == changedPodcast.Category)
                .First();
            if (newCategory.Id != ObjectId.Empty)
            {
                toChange.SetCategoryText(newCategory.Text);
            }
            else {
                toChange.SetCategoryText("");
            }
            //Spara till DB
            await _podService.UpdateCategoryAsync(changedPodcast, changedPodcast.Category);
        }

        private PodCard? FindPodCardById(ObjectId podcastId)
        {
            return flpMyPods.Controls
                .OfType<PodCard>()
                .FirstOrDefault(pc => pc.Podcast.Id == podcastId);
        }

        private PodCard? FindPodCardByRss(string rssUrl)
        {
            return flpMyPods.Controls
                .OfType<PodCard>()
                .FirstOrDefault(pc => pc.Podcast.RssUrl == rssUrl);
        }


        private void HandlePodCardSelection(Podcast podcast) {
            //Kollar om det finns en vald podcard för att då "deselecta" den
            if (selectedPodCard != null)
            {
                selectedPodCard.BackColor = SystemColors.Menu;
                selectedPodCard.BorderStyle = BorderStyle.None;
            }

            selectedPodCard = FindPodCardById(podcast.Id);

            if (selectedPodCard != null)
            {
                selectedPodCard.BackColor = Color.LightBlue;
                selectedPodCard.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private async void UpdatePodcasts() {
            List<Podcast> allPodcasts = flpMyPods.Controls.OfType<PodCard>().Select(pc => pc.Podcast).ToList();

            List<Podcast> updatedPodcasts = await _podService.FetchNewEpisodes(allPodcasts);

            foreach (PodCard aPodCard in flpMyPods.Controls.OfType<PodCard>()) {
                Podcast? updated = updatedPodcasts.FirstOrDefault(p => p.Id == aPodCard.Podcast.Id);

                if (updated != null) {
                    aPodCard.Podcast = updated;
                    if (selectedPodCard != null && selectedPodCard.Podcast.Id == aPodCard.Podcast.Id) {
                        //Invokar på UI-tråden istället för timer-tråden (podUpdater)
                        Invoke((MethodInvoker)(() => {
                            DisplayPodPanel(aPodCard.Podcast);
                        }));
                    }
                }
            }
        }

        private string GetCategoryText(ObjectId objectId)
        {
            if (objectId == ObjectId.Empty)
                return string.Empty;

            Category? category = _categoryDataSource.FirstOrDefault(c => c.Id == objectId);
            return category?.Text ?? string.Empty;
        }

        private PodCard CreateAndAddPodCard(Podcast pod)
        {
            var podCard = new PodCard(pod);
            podCard.SetCategoryText(GetCategoryText(pod.Category));

            flpMyPods.Controls.Add(podCard);
            podCard.MouseClick += (s, e) =>
            {
                if (s is PodCard podCard)
                {
                    DisplayPodPanel(podCard.Podcast);
                }
            };

            return podCard;
        }

        private void RefreshPodPanelCategoryDataSource()
        {
            PodPanel? toNotify = pPodPanel.Controls.OfType<PodPanel>().FirstOrDefault();
            toNotify?.UpdateDataSource(_categoryDataSource);
        }

        private void ClearAndRefreshPodList(IEnumerable<Podcast> podcasts)
        {
            flpMyPods.Controls.Clear();

            foreach(Podcast pod in podcasts)
            {
                CreateAndAddPodCard(pod);
            }
        }

    }


}
