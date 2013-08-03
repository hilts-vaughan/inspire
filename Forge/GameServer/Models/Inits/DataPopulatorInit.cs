using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Crypto;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Enums;
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
                    "Sinjo Town/Interior"
                };

            // Create some mock items
            for (int i = 0; i < 20; i++)
            {
                var itemTemplate = new ItemTemplate(0, "New Item", "", ItemType.Consumable, 0, false, 0);
                context.ItemTemplates.Add(itemTemplate);

                var skillTemplate = new SkillTemplate("", 0, "New Skill", "");
                context.SkillTemplates.Add(skillTemplate);

                var mapTemplate = new MapTemplate(0, "New Map", list[i % (list.Count - 1) ] );
                context.MapTemplates.Add(mapTemplate);


            }

            // Add a new user
            var rootUser = new Account(0, "root", HashHelper.CalculateSha512Hash("root"), DateTime.UtcNow);
            rootUser.EditorAllowed = true;
            context.Accounts.Add(rootUser);

            context.SaveChanges();
            base.Seed(context);
        }

    }
}
