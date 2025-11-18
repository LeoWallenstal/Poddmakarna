using BL;
using DAL;
using Models;
using MongoDB.Bson;
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
        private Dictionary<ObjectId, Category> categoryDict = new Dictionary<ObjectId, Category>();
        private Podcast selectedPodcast;
        private BindingList<Category> _categories;


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

            categoryPanel.OnCategoryAdded += (aCategory) => {
                categoryDict.Add(aCategory.Id, aCategory);
                _categories.Add(aCategory);
                //DB delete
            };
            categoryPanel.OnCategoryRemoved += (aCategory) => { 
                categoryDict.Remove(aCategory.Id);
                _categories.Remove(aCategory);
                //DB delete
            };
            categoryPanel.OnCategoryChanged += (changedCategory) =>
            {
                //Jag antar att den skriver över existerande värde här
                categoryDict.Add(changedCategory.Id, changedCategory);
                //VILKEN SKA JAG ÄNDRA????????????????? ses imorrn
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
            _categories = new BindingList<Category>(allCategories);

            foreach (Category category in allCategories) {
                categoryDict[category.Id] = category;
            }

            allCategories.Insert(0, new Category { Id = ObjectId.Empty, Text = "Alla Poddar" });
            cbCategories.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategories.DataSource = _categories;

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
                    category = categoryDict[pod.Category].Text;
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
            PodPanel toShow = new PodPanel(podcast, categoryDict);
            pPodPanel.Controls.Clear();
            pPodPanel.Controls.Add(toShow);
            toShow.OnTitleChanged += ReflectTitleChange;

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
                    Debug.WriteLine($"ReflectTitleChange() - Index: {index}");
                    PodCard toChange = flpMyPods.Controls.OfType<PodCard>().ToList()[index];

                    //Ändrar objektet i minnet
                    toChange.Podcast.Title = senderPanel.Podcast.Title;
                    //Ändrar labeln i GUI:t
                    toChange.TitleLabel.Text = senderPanel.Podcast.Title;
                    Debug.WriteLine("podPanel PODCAST-title: " + senderPanel.Podcast.Title);
                    senderPanel.Refresh();
                }
            }
            Debug.WriteLine("Form2 hör att titeln har ändrats : )))))");
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
