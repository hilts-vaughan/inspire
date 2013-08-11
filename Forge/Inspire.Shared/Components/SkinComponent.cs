namespace Inspire.Shared.Components
{
    /// <summary>
    /// A skin component has information detailing how a sprite object will be constructed on the client side for a particular entity.
    /// </summary>
    public class SkinComponent : Component
    {
        public SkinComponent()
        {
            
        }

        public SkinComponent(string spriteDescriptorName)
        {
            SpriteDescriptorName = spriteDescriptorName;
        }

        /// <summary>
        /// The name of the sprite descriptor to be retrieved for this component.
        /// </summary>
        public string SpriteDescriptorName { get; set; }

    }
}
