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
    /// A content category maps nodes and a given list of content to a particular subsection
    /// </summary>
    public class ContentCategory
    {
        private ContentType _contentType;

        public ContentCategory(ContentType contentType)
        {
            _contentType = contentType;
        }

        // A list of all entries
        List<EditorTemplateEntry> _templateEntries = new List<EditorTemplateEntry>();

        public void SetContentEntries(List<EditorTemplateEntry> entries)
        {
            _templateEntries = entries;
        }

        public void UpdateContentEntry(EditorTemplateEntry editorTemplateEntry)
        {
            var oldEntry = _templateEntries.Find(x => x.ID == editorTemplateEntry.ID);
            
            // It's possible these are being created (but unlikely)
            if(oldEntry != null)
                _templateEntries.Remove(oldEntry);

            // Add new entry
            _templateEntries.Add(editorTemplateEntry);

        }

        public TreeNode GenerateContentCategory()
        {
             
            // Create our root node
            var root = new TreeNode(_contentType.ToString(), 0, 0);

            foreach (var editorTemplateEntry in _templateEntries)
            {
                var node = new TreeNode(editorTemplateEntry.Name, (int)_contentType + 3, (int)_contentType + 3);
                node.Tag = editorTemplateEntry;
                root.Nodes.Add(node);
            }

            return root;
        }

        private void VerifyCategoryExists(TreeNode root, string virtualCategory)
        {
            
        }


    }
}
