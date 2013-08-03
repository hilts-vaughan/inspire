using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;

namespace Toolkit.ContentExplorer
{
    /// <summary>
    /// A class that helps map between a list of content that has been sent down and map it a virtual directory
    /// </summary>
    public class ContentMapper
    {
        // A lookup map
        Dictionary<ContentType, ContentCategory> _contentCategories = new Dictionary<ContentType, ContentCategory>();

        public ContentMapper()
        {
            // Generate the ContentMap dynamically, assinging everyone a backing
            foreach (var contentType in GetValues<ContentType>())
                _contentCategories.Add(contentType, new ContentCategory(contentType));

        }


        /// <summary>
        /// Generates the tree structure and the assosciated meta deta for a particular content type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public TreeNode GenerateContentCategory(ContentType contentType)
        {
            return _contentCategories[contentType].GenerateContentCategory();
        }

        public void SetEntries(ContentType contentType, List<EditorTemplateEntry> entries)
        {
            _contentCategories[contentType].SetContentEntries(entries);
        }

        public void UpdateContentEntry(ContentType contentType, EditorTemplateEntry editorTemplateEntry)
        {
            var category = _contentCategories[contentType];
            category.UpdateContentEntry(editorTemplateEntry);
        }



        private static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

    }
}
