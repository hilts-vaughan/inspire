using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.GameEngine.Services;
using Inspire.Network.Packets.Server.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Services
{
    public class EntitySyncService : Service
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void HandleInput(InputState inputState)
        {

        }

        public override void Initialize()
        {
            PacketService.RegisterPacket<EntityAddPacket>(EntityRecieved);
            PacketService.RegisterPacket<EntityRemovePacket>(EntityRemoved);
        }

        private void EntityRemoved(EntityRemovePacket entityRemovePacket)
        {
            ServiceManager.RemoveEntityByID(entityRemovePacket.EntityID);
        }

        private void EntityRecieved(EntityAddPacket entityAddPacket)
        {
            // Add the entity
            ServiceManager.AddEntity(entityAddPacket.Entity);
        }


    }
}
