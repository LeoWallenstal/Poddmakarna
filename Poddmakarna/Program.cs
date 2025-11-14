using BL;
using DAL;
using UI;
using Models;
using Services;

namespace Poddmakarna
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //Wiring
            MongoDbContext _dbContext = new MongoDbContext();
            IPodRepository podRepo = new PodcastRepository(_dbContext.Database.GetCollection<Podcast>("Pods"), _dbContext.Client);
            IRssReader rssReader = new RssReader();
            IPodService podService = new PodcastService(podRepo, rssReader);

            ICategoryRepository categoryRepo = new CategoryRepository(_dbContext.Database.GetCollection<Category>("Categories"), _dbContext.Client);
            ICategoryService categoryService = new CategoryService(categoryRepo);

            Application.Run(new Form2(podService, categoryService));
        }
    }
}