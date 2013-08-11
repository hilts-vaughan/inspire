using Lidgren.Network;

namespace Inspire.Shared.Components
{
    public class CharacterComponent : Component 
    {

        [DoNotSerialize]
        public NetConnection Connection { get; set; }

        public CharacterComponent(NetConnection connection)
        {
            Connection = connection;
        }

        public CharacterComponent()
        {
                
        }

    }
}
