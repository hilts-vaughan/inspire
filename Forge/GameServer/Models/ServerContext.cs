using System.Data.Entity;
using GameServer.Models.Mapping;
using Inspire.Shared.Models;

namespace GameServer.Models
{
    public partial class ServerContext : DbContext
    {
        static ServerContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ServerContext>());
        }

        public ServerContext()
            : base("serverContext")
        {
            Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ItemTemplate> ItemTemplates { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new blastersmemberMap());
            modelBuilder.Configurations.Add(new ItemTemplateMap());



        }
    }
}
