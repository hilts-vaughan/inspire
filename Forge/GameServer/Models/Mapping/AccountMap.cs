using System.Data.Entity.ModelConfiguration;
using Inspire.Shared.Models;

namespace GameServer.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountId);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(45);

            // Properties
            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(45);

             // Properties
            this.Property(t => t.CreationDate)
                .IsRequired();


        }
    }
}
