using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Components;
using Inspire.Shared.Models;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace GameServer.Game
{
    public static class EntityFactory
    {

        public static Entity CreateCharacter(Character character, NetConnection connection)
        {
            var entity = new Entity();

            var size = new Vector2(96, 96);
            var position = new Vector2(character.WorldX, character.WorldY);
            entity.AddComponent(new TransformComponent(position, size));

            entity.AddComponent(new CharacterComponent(connection));
            entity.AddComponent(new NameComponent(character.Name));
            entity.AddComponent(new SkinComponent("HumanBase"));


            return entity;
        }


    }
}
