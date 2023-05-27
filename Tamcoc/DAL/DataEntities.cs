using System.Data.Entity;
using Tamcoc.Models;

namespace Tamcoc.DAL
{
    public class DataEntities : DbContext
    {
        public DataEntities() : base("name=DataEntities") { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ConfigSite> ConfigSites { get; set; }
        public DbSet<Subcribe> Subcribes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<KindOfRoom> KindOfRooms { get; set; }
    }
}