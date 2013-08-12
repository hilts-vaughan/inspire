using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using Inspire.Shared;
using Inspire.Shared.Crypto;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Models.Templates;

namespace GameServer.Models.Inits
{
    public class CustomInitializer : DropCreateDatabaseIfModelChanges<ServerContext>
    {

        protected override void Seed(ServerContext context)
        {

            var list = new List<string>
                {
                    "Lavender Town",
                    "Sinjo Town",
                    "Battle Tower",
                    "Battle Tower/Interior",
                    "Battle Tower/Interior/Master Room",
                    "Sinjo Town/Interior",
                    "Kruos Fields",
                    "The Grand Clocktower",
                    "The Grand Clocktower/Secrets"
                };

            // Create some mock items
            for (int i = 0; i < 50; i++)
            {
                var itemTemplate = new ItemTemplate(0, "New Item", "", ItemType.Consumable, 0, false, 0);
                context.ItemTemplates.Add(itemTemplate);

                var skillTemplate = new SkillTemplate("", 0, "New Skill", "");
                context.SkillTemplates.Add(skillTemplate);



                var mapTemplate = new MapTemplate(0, "New Map", list[i % (list.Count) ] );

                // Create some blank maps                
                var bytes = SerializationHelper.ObjectToByteArray(new GameMap());
                mapTemplate.BinaryData = bytes;

                context.MapTemplates.Add(mapTemplate);


            }

            // Add a new user
            var rootUser = new Account(0, "root", HashHelper.CalculateSha512Hash("root"), DateTime.UtcNow);
            rootUser.EditorAllowed = true;

            var rootUser2 = new Account(0, "root2", HashHelper.CalculateSha512Hash("root2"), DateTime.UtcNow);
            rootUser2.EditorAllowed = true;

            var character = new Character(1, 1, "Beatrix", 1, 0, 0, 0);
            var character2 = new Character(2, 2, "Galebri", 1, 0, 0, 0);

            context.Accounts.Add(rootUser);
            context.Accounts.Add(rootUser2);

            context.Characters.Add(character);
            context.Characters.Add(character2);

            context.SaveChanges();
            base.Seed(context);
        }

    }
}
