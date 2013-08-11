using System.Data.Entity.ModelConfiguration;
using Inspire.Shared.Models;

namespace GameServer.Models.Mapping
{
    public class CharacterMap : EntityTypeConfiguration<Character>
    {
        public CharacterMap()
        {
            // Primary Key
            this.HasKey(t => t.CharacterId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);



        }
    }
}
