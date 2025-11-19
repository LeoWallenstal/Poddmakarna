using BL;
using DAL;
using Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace UI
{
    public partial class Form2 : Form
    {
        private readonly IPodService _podService;
        private readonly ICategoryService _categoryService;
        //private Dictionary<ObjectId, Category> categoryDict = new Dictionary<ObjectId, Category>();
        private Podcast selectedPodcast;
        private BindingList<Category> _categoryDataSource;


        //DEBUG
        private readonly List<string> podcastUrls = new List<string>() {
            "https://feed.pod.space/krimrummet",
            "https://feed.pod.space/svenskafallpodcast",
            "https://feeds.acast.com/public/shows/d870d9c3-9eb0-4b09-90b4-5c727d6ac077",
            "https://api.sr.se/api/rss/pod/itunes/3966",
            "https://feeds.acast.com/public/shows/mer-an-bara-morsa",
            "https://feeds.acast.com/public/shows/67574d18-1814-47f9-9acb-e04766b06b5f",
            "https://feed.pod.space/filipandfredrik",
            "https://feed.pod.space/krogtipsetmedmorbergs",
            "https://rss.podplaystudio.com/1477.xml",
            "https://feed.pod.space/alexosigge"
        };
        public Form2(IPodService podService, ICategoryService categoryService)
        {
            InitializeComponent();

            _podService = podService;
            _categoryService = categoryService;

            this.Load += LoadPodcast;
            this.Load += InitCategories;
            btnSave.Hide();

            CategoryPanel categoryPanel = new CategoryPanel(categoryService);

            categoryPanel.OnCategoryAdded += async (toAdd) => {
                _categoryDataSource.Add(toAdd);

                //Lägg till i DB
                await categoryService.InsertAsync(toAdd);

                //Säg till podpanel
                PodPanel toNotify = pPodPanel.Controls.OfType<PodPanel>().FirstOrDefault();
                if (toNotify != null) {
                    toNotify.UpdateDataSource(_categoryDataSource);
                }
                
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
                PodPanel toNotify = pPodPanel.Controls.OfType<PodPanel>().FirstOrDefault();
                if (toNotify != null)
                {
                    toNotify.UpdateDataSource(_categoryDataSource);
                }
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
                PodPanel toNotify = pPodPanel.Controls.OfType<PodPanel>().FirstOrDefault();
                if (toNotify != null)
                {
                    toNotify.UpdateDataSource(_categoryDataSource);
                }
            };

            pCategoryPanel.Controls.Add(categoryPanel);

            //Debug
            btnDebugFetchPods.MouseClick += LoadDebugPodcasts;
            btnDebugRemovePodcasts.MouseClick += RemoveDebugPodcasts;
            //Debug
        }

        //DEBUG
        private async void LoadDebugPodcasts(object sender, EventArgs e) {
            foreach (string url in podcastUrls)
            {
                Podcast? pendingPodcast = await _podService.FetchPodFromRssAsync(url);
                if (pendingPodcast != null)
                {
                    flpMyPods.Controls.Add(new PodCard(pendingPodcast));
                    await _podService.InsertAsync(pendingPodcast);
                }
            }
            DisplayPodPanel(flpMyPods.Controls.OfType<PodCard>().ToList().FirstOrDefault().Podcast);
        }

        //DEBUG
        private async void RemoveDebugPodcasts(object sender, EventArgs e) {
            foreach (PodCard podcard in flpMyPods.Controls) {
                await _podService.DeleteAsync(podcard.Podcast);
                flpMyPods.Controls.Remove(podcard);
            }
        }

        //Kanske 'async' i namnet...?? 
        private async void InitCategories(object sender, EventArgs e) {
            List<Category> allCategories = await _categoryService.GetAllAsync();
            _categoryDataSource = new BindingList<Category>(allCategories);


            allCategories.Insert(0, new Category { Id = ObjectId.Empty, Text = "Alla Poddar" });
            cbCategories.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategories.DataSource = _categoryDataSource;


            //HÄR HÄNDER VÄLJANDET AV ETT ITEM I COMBOBOX
            cbCategories.SelectionChangeCommitted += async (s, e) =>
            {
                Debug.WriteLine("SelectionChangeCommitted!");
                if (cbCategories.SelectedIndex == 0) {
                    //Clear kanske ska sitta nån annanstans dåra
                    flpMyPods.Controls.Clear();
                    LoadPodcast(this, EventArgs.Empty);
                }
                else
                {
                    Category? selectedCategory = cbCategories.SelectedItem as Category;
                    if (selectedCategory != null) {
                        List<Podcast> sortedByCategory = await _podService.GetByCategoryAsync(selectedCategory.Id);

                        flpMyPods.Controls.Clear();

                        foreach (Podcast pod in sortedByCategory)
                        {
                            PodCard podCard = new PodCard(pod);
                            string category = "";
                            if (pod.Category != ObjectId.Empty)
                            {
                                Category cat = _categoryDataSource.FirstOrDefault(c => c.Id == pod.Category);
                                if (cat != null) {
                                    category = cat.Text;
                                }
                            }
                            podCard.SetCategoryText(category);
                            flpMyPods.Controls.Add(podCard);
                            podCard.MouseClick += PodCard_Clicked;
                        }
                    }
                }
            };
        }

        private async void LoadPodcast(object sender, EventArgs e)
        {
            List<Podcast> allaPoddar = await _podService.GetAllAsync();

            foreach (Podcast pod in allaPoddar)
            {
                PodCard podCard = new PodCard(pod);
                string category = "";
                if (pod.Category != ObjectId.Empty)
                {
                    Category cat = _categoryDataSource.FirstOrDefault(c => c.Id == pod.Category);
                    if (cat != null)
                    {
                        category = cat.Text;
                    }
                }
                podCard.SetCategoryText(category);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }

            if (allaPoddar.Count > 0)
                DisplayPodPanel(allaPoddar.First());
        }

        public void PodCard_Clicked(object sender, EventArgs e)
        {
            if (sender is PodCard podCard)
            {
                Debug.WriteLine(flpMyPods.Controls.IndexOf(podCard));
                DisplayPodPanel(podCard.Podcast);
            }
        }

        private async void DisplayPodPanel(Podcast podcast)
        {
            PodPanel toShow = new PodPanel(podcast, _categoryDataSource);
            pPodPanel.Controls.Clear();
            pPodPanel.Controls.Add(toShow);
            toShow.OnTitleChanged += ReflectTitleChange;
            toShow.OnCategoryChanged += ReflectCategoryChange;

            selectedPodcast = podcast;

            if (await _podService.RssExistsAsync(podcast.RssUrl))
            {
                btnSave.Hide();
                btnDelete.Show();
            }
            else
            {
                btnSave.Show();
                btnDelete.Hide();
            }
        }

        private async void btnGetRss_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Podcast? pendingPodcast = await _podService.FetchPodFromRssAsync(tbRssUrl.Text);
            if (pendingPodcast != null)
            {
                DisplayPodPanel(pendingPodcast);
            }
            this.Cursor = Cursors.Default;

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedPodcast != null)
            {
                await _podService.InsertAsync(selectedPodcast);
                btnSave.Hide();
                btnDelete.Show();
                PodCard podCard = new PodCard(selectedPodcast);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(selectedPodcast != null)
            {
                await _podService.DeleteAsync(selectedPodcast);
                btnDelete.Hide();
                btnSave.Show();
                foreach (var card in flpMyPods.Controls.OfType<PodCard>().ToList())
                {
                    //Refaktorisera med GetMyPodsIndex() sen
                    if (card.Podcast.RssUrl == selectedPodcast.RssUrl) {
                        int index = flpMyPods.Controls.IndexOf(card);
                        flpMyPods.Controls.Remove(card);
                        DisplayAfterDelete(index);
                        return;
                    }
                }
                
            }
        }

        private void DisplayAfterDelete(int index) {
            if (flpMyPods.Controls.Count == 0) {
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

        private async void ReflectTitleChange(object sender, EventArgs e) {
            if (sender is PodPanel senderPanel) {
                var saveSucceeded = await _podService.UpdateTitleAsync(senderPanel.Podcast, senderPanel.PodTitle);
                if (saveSucceeded) { 
                    int index = GetMyPodsIndex(senderPanel.Podcast.RssUrl);
                    PodCard toChange = flpMyPods.Controls.OfType<PodCard>().ToList()[index];

                    //Ändrar objektet i minnet
                    toChange.Podcast.Title = senderPanel.Podcast.Title;
                    //Ändrar labeln i GUI:t
                    toChange.TitleLabel.Text = senderPanel.Podcast.Title;
                    senderPanel.Refresh();
                }
            }
            Debug.WriteLine("Form2 hör att titeln har ändrats : )))))");
        }

        private async void ReflectCategoryChange(Podcast changedPodcast) {
            PodCard toChange = flpMyPods.Controls
                .OfType<PodCard>()
                .Where(pc => pc.Podcast.Id == changedPodcast.Id)
                .First();
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

        private int GetMyPodsIndex(string rssUrl) {
            foreach (var card in flpMyPods.Controls.OfType<PodCard>().ToList())
            {
                if (card.Podcast.RssUrl == rssUrl)
                {
                    return flpMyPods.Controls.IndexOf(card);
                }
            }
            Debug.WriteLine("Returned -1! Check rssUrl?");
            return -1;
        }
    }
}
