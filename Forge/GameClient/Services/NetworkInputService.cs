using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.Services;
using Inspire.Shared.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Services
{
    /// <summary>
    /// The network input service is used for relaying network presses coming from entities accross the network.
    /// This is mainly used for movement and actions.
    /// </summary>
    public class NetworkInputService : Service
    {
        private readonly ulong _idToMonitor;
        private Entity _player = null;

        public NetworkInputService(ulong idToMonitor)
        {
            _idToMonitor = idToMonitor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Do NOTHING!
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Write snc logic
        }

        public override void HandleInput(InputState inputState)
        {

            if (GameGlobals.EntityID == null)
                return;

            if (_player == null)
            {
                _player = ServiceManager.RetrieveEntityByID((ulong) GameGlobals.EntityID);
            }

            // Get the transform component
            var transformComponent = _player.GetComponent<TransformComponent>();

            const int ABSOLUTE_SPEED = 3;

            // We adjust instant velocity as needed here)
            Vector2 newVector = Vector2.Zero;

            if (inputState.NotMoving())
                newVector = Vector2.Zero;

            if (inputState.MoveLeftIssued())
                newVector -= new Vector2(ABSOLUTE_SPEED * 1, 0);

            if (inputState.MoveRightIssued())
                newVector += new Vector2(ABSOLUTE_SPEED * 1, 0);

            if (inputState.MoveUpIssued())
                newVector -= new Vector2(0, ABSOLUTE_SPEED * 1);

            if (inputState.MoveDownIssued())
                newVector += new Vector2(0, ABSOLUTE_SPEED * 1);

            if (newVector != transformComponent.Velocity)
                transformComponent.Velocity = newVector;


        }



        public override void Initialize()
        {


        }
    }
}
