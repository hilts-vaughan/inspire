using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Network.Packets.Server;
using Inspire.Network.Packets.Server.Content;
using Toolkit.ContentExplorer;

namespace Toolkit.Docking
{
    public partial class ContentDockForm : ToolWindow
    {
        private ContentMapper _contentMapper = new ContentMapper();

        public ContentDockForm()
        {
            InitializeComponent();
            TabText = "Content Explorer";

            CheckForIllegalCrossThreadCalls = false;

            // Listen for content data incoming
            PacketService.RegisterPacket<ContentListResultPacket>(HandleContent);

        }

        private void HandleContent(ContentListResultPacket packet)
        {
            // If you recieve a whole set, time to create an content entry
            _contentMapper.SetEntries(packet.ContentType, packet.EditorTemplateEntries);
            var categoryNode = _contentMapper.GenerateContentCategory(packet.ContentType);

            // Add to the tree
            treeContent.Invoke(() => treeContent.Nodes.Add(categoryNode));

        }





    }
}
