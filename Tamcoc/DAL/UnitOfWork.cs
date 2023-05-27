using System;
using Tamcoc.Models;
namespace Tamcoc.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DataEntities _context = new DataEntities();
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<ArticleCategory> _articategoryRepository;
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<Banner> _bannerRepository;
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<ConfigSite> _configRepository;
        private GenericRepository<Subcribe> _subcribeRepository;
        private GenericRepository<Language> _languageRepository;
        private GenericRepository<Feedback> _feedbackRepository;
        private GenericRepository<Room> _roomRepository;
        private GenericRepository<KindOfRoom> _kindOfRoomRepository;
        private GenericRepository<Order> _orderRepository;

        public GenericRepository<RoomEn> _roomEnRepository;
        public GenericRepository<FeedbackEn> _feedbackEnRepository;
        public GenericRepository<ArticleEn> _articleEnRepository;
        public GenericRepository<ArticleCategoryEn> _articleCategoryEnRepository;
        public GenericRepository<BannerEn> _bannerEnRepository;
        public GenericRepository<ConfigSiteEn> _configEnRepository;

        public GenericRepository<RoomFr> _roomFrRepository;
        public GenericRepository<FeedbackFr> _feedbackFrRepository;
        public GenericRepository<ArticleFr> _articleFrRepository;
        public GenericRepository<ArticleCategoryFn> _articleCategoryFrRepository;
        public GenericRepository<BannerFr> _bannerFrRepository;
        public GenericRepository<ConfigSiteFr> _configFrRepository;



        public GenericRepository<Order> OrderRepository =>
          _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context));
        public GenericRepository<KindOfRoom> KindOfRoomRepository =>
          _kindOfRoomRepository ?? (_kindOfRoomRepository = new GenericRepository<KindOfRoom>(_context));
        public GenericRepository<Room> RoomRepository =>
           _roomRepository ?? (_roomRepository = new GenericRepository<Room>(_context));
        public GenericRepository<RoomEn> RoomEnRepository =>
          _roomEnRepository ?? (_roomEnRepository = new GenericRepository<RoomEn>(_context));
        public GenericRepository<RoomFr> RoomFrRepository =>
                  _roomFrRepository ?? (_roomFrRepository = new GenericRepository<RoomFr>(_context));

        public GenericRepository<Feedback> FeedbackRepository =>
           _feedbackRepository ?? (_feedbackRepository = new GenericRepository<Feedback>(_context));
        public GenericRepository<FeedbackEn> FeedbackEnRepository =>
           _feedbackEnRepository ?? (_feedbackEnRepository = new GenericRepository<FeedbackEn>(_context));
        public GenericRepository<FeedbackFr> FeedbackFrRepository =>
           _feedbackFrRepository ?? (_feedbackFrRepository = new GenericRepository<FeedbackFr>(_context));
        public GenericRepository<ArticleFr> ArticleFrRepository =>
           _articleFrRepository ?? (_articleFrRepository = new GenericRepository<ArticleFr>(_context));
        public GenericRepository<ArticleCategoryFn> ArticleCategoryFrRepository =>
          _articleCategoryFrRepository ?? (_articleCategoryFrRepository = new GenericRepository<ArticleCategoryFn>(_context));
        public GenericRepository<BannerFr> BannerFrRepository =>
          _bannerFrRepository ?? (_bannerFrRepository = new GenericRepository<BannerFr>(_context));
        public GenericRepository<ConfigSiteFr> ConfigSiteFrRepository =>
         _configFrRepository ?? (_configFrRepository = new GenericRepository<ConfigSiteFr>(_context));

        public GenericRepository<Language> LanguageRepository =>
            _languageRepository ?? (_languageRepository = new GenericRepository<Language>(_context));
        public GenericRepository<ArticleEn> ArticleEnRepository =>
            _articleEnRepository ?? (_articleEnRepository = new GenericRepository<ArticleEn>(_context));
        public GenericRepository<ArticleCategoryEn> ArticleCategoryEnRepository =>
            _articleCategoryEnRepository ?? (_articleCategoryEnRepository = new GenericRepository<ArticleCategoryEn>(_context));
        public GenericRepository<BannerEn> BannerEnRepository =>
            _bannerEnRepository ?? (_bannerEnRepository = new GenericRepository<BannerEn>(_context));
        public GenericRepository<ConfigSiteEn> ConfigSiteEnRepository =>
            _configEnRepository ?? (_configEnRepository = new GenericRepository<ConfigSiteEn>(_context));
        public GenericRepository<Subcribe> SubcribeRepository =>
            _subcribeRepository ?? (_subcribeRepository = new GenericRepository<Subcribe>(_context));
        public GenericRepository<ConfigSite> ConfigSiteRepository =>
            _configRepository ?? (_configRepository = new GenericRepository<ConfigSite>(_context));
        public GenericRepository<Contact> ContactRepository =>
            _contactRepository ?? (_contactRepository = new GenericRepository<Contact>(_context));
        public GenericRepository<Banner> BannerRepository =>
            _bannerRepository ?? (_bannerRepository = new GenericRepository<Banner>(_context));
        public GenericRepository<Article> ArticleRepository =>
            _articleRepository ?? (_articleRepository = new GenericRepository<Article>(_context));
        public GenericRepository<ArticleCategory> ArticleCategoryRepository =>
            _articategoryRepository ?? (_articategoryRepository = new GenericRepository<ArticleCategory>(_context));
        public GenericRepository<Admin> AdminRepository =>
            _adminRepository ?? (_adminRepository = new GenericRepository<Admin>(_context));
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}