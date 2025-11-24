using Models;

namespace UI
{
    public partial class PodCard : UserControl
    {
        public Podcast Podcast { get; set; }

        public Label TitleLabel {
            get { return lblTitle; }
        }
        public PodCard(Podcast podcast)
        {
            Podcast = podcast;
            InitializeComponent();
            LoadPodCard();

            //Vidarebefordra MouseClick eventet från alla underkontroller till PodCard
            //så att klick på t.ex. bilden också räknas som klick på PodCard
            foreach (Control c in this.Controls)
            {
                c.MouseClick += (s, e) => 
                {
                    this.OnMouseClick(e);
                };
            }
        }

        private void LoadPodCard()
        {
            lblTitle.Text = $"{Podcast.Title}";
            pbThumbnail.ImageLocation = Podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetCategoryText(string categoryText)
        {
            lblCategory.Text = categoryText;
        }
    }
}
