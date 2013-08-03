using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit.ContentExplorer
{
    /// <summary>
    ///  Create a node sorter that implements the IComparer interface.
    /// </summary>
    public class NodeSorter : IComparer
    {
        // compare between two tree nodes
        public int Compare(object thisObj, object otherObj)
        {
            TreeNode thisNode = thisObj as TreeNode;
            TreeNode otherNode = otherObj as TreeNode;

            // Compare the types of the tags, returning the difference.
            if (otherNode.Tag == null && thisNode.Tag != null)
                return 1;
            //alphabetically sorting
            return thisNode.Text.CompareTo(otherNode.Text);
        }


    } 
}
