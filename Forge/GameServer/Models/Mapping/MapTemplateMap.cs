using System.Data.Entity.ModelConfiguration;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Templates;

namespace GameServer.Models.Mapping
{
    public class MapTemplateMap : EntityTypeConfiguration<MapTemplate>
    {
        public MapTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

        }
    }
}
