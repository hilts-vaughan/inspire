using System.Data.Entity;
using GameServer.Models.Inits;
using GameServer.Models.Mapping;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Templates;

namespace GameServer.Models
{
    public partial class ServerContext : DbContext
    {
        static ServerContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ServerContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ServerContext>());
            Database.SetInitializer(new CustomInitializer());
        }

        public ServerContext()
            : base("serverContext")
        {
            Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;          

        }

        public DbSet<ItemTemplate> ItemTemplates { get; set; }
        public DbSet<Account> Accounts { get; set; }
    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new blastersmemberMap());
            modelBuilder.Configurations.Add(new ItemTemplateMap());
            modelBuilder.Configurations.Add(new AccountMap());


        }

   

    }
}
