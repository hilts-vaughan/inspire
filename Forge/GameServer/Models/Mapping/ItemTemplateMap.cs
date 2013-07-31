using System.Data.Entity.ModelConfiguration;
using Inspire.Shared.Models;

namespace GameServer.Models.Mapping
{
    public class ItemTemplateMap : EntityTypeConfiguration<ItemTemplate>
    {
        public ItemTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

        }
    }
}
