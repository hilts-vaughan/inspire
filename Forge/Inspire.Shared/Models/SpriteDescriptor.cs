using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Inspire.Shared.Models
{
    /// <summary>
    /// A sprite descriptor contains information regarding a sprite, 
    /// </summary>
    public class SpriteDescriptor
    {

        /// <summary>
        /// The name of a descriptor if it has any.
        /// This should map to the name of the sprite a particular entity is going to use.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The realative path to the sprite texture that is refered to on this descriptor.
        /// The "root directory" is the Content folder.
        /// </summary>
        public string SpritePath { get; set; }

        /// <summary>
        /// The size of a single frame on this sprite.
        /// This is required for drawing animations and spritesheets correctly.
        /// </summary>
        public Vector2 FrameSize { get; set; }

        /// <summary>
        /// The bounding box of the sprite.
        /// This is necessary for collision with dynamically sized sprites.
        /// </summary>
        public Rectangle BoundingBox { get; set; }

        /// <summary>
        /// The depth for a particular sprite; this is used for sorting before drawing.
        /// </summary>
        public int SpriteDepth { get; set; }

        public List<SpriteDescriptorAnimation> Animations { get; set; }

        public SpriteDescriptor()
        {
            Animations = new List<SpriteDescriptorAnimation>();
        }

        /// <summary>
        /// Persists a descriptor to the disk.
        /// </summary>
        /// <param name="filename">The location on the disk to persist to.</param>
        public void Persist(string filename)
        {
            var serializer = new XmlSerializer(GetType());
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                serializer.Serialize(fs, this);
        }

        /// <summary>
        /// Depersists a <see cref="SpriteDescriptor"/> directly from the disk.
        /// </summary>
        /// <param name="filename">The location on the disk to load from.</param>
        /// <returns>A sprite descriptor created from disk.</returns>
        public static SpriteDescriptor FromFile(string filename)
        {
            // If the file does not exist, create it
            if(File.Exists(filename) == false)
                new SpriteDescriptor().Persist(filename);

            var serializer = new XmlSerializer(typeof(SpriteDescriptor));
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                return (SpriteDescriptor) serializer.Deserialize(fs);
        }
    }

    public class SpriteDescriptorAnimation
    {

        public SpriteDescriptorAnimation(string name, float speed, int frameCount, int row)
        {
            Name = name;
            Speed = speed;
            FrameCount = frameCount;
            Row = row;
        }

        public SpriteDescriptorAnimation()
        {
            
        }

        /// <summary>
        /// The name of a given animation to be played.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The speed to play this particular animation at
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// The number of frames for this particular animation
        /// </summary>
        public int FrameCount { get; set; }

        /// <summary>
        /// The row ID of this animation
        /// </summary>
        public int Row { get; set; }


    }

}
