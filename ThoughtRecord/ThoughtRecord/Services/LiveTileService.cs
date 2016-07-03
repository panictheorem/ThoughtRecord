using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace ThoughtRecordApp.Services
{
    public static class LiveTileService
    {
        public static void SetLiveTile()
        {
            // Construct the tile content as a string
            string content = $@"
                             <tile>
                                    <visual>
                                        <binding template='TileSquarePeekImageAndText04'>
                                            <image id='1' src='Assets/LiveTile.png' alt='Thought Record'/>
                                            <text id='1' hint-wrap='true'>Have you written a thought record today?</text>
                                        </binding>
                                    </visual>
                             </tile>";

            // Load the string into an XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            var tileNotification = new TileNotification(doc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
