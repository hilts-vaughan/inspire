namespace Inspire.Shared.Components
{
    /// <summary>
    /// The name component is a simple container for data pertaining to entities that have a visible name.
    /// </summary>
    public class NameComponent : Component
    {
        public string Name { get; set; }

        public NameComponent(string name)
        {
            Name = name;
        }


        public NameComponent()
        {
            
        }
    }
}
