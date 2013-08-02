namespace Inspire.Shared.Models.Templates
{

    /// <summary>
    /// The content template is implemented by any content that is within the game world.
    /// </summary>
    public interface IContentTemplate
    {

        /// <summary>
        /// The unique identifier associated with this piece of content
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name associated with this piece of content
        /// </summary>
        string Name { get; set; }
    }
}
