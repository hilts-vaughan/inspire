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

            // Create some mock items
            for (int i = 0; i < 1000; i++)
            {
                var itemTemplate = new ItemTemplate(0, "Empty Item", "", ItemType.Consumable, 0, false, 0);
                context.ItemTemplates.Add(itemTemplate);

                var skillTemplate = new SkillTemplate("", 0, "Empty Skill", "");
                context.SkillTemplates.Add(skillTemplate);


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
