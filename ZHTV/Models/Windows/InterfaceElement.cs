using System.Windows.Controls;
using CefSharp.Wpf;

namespace ZHTV.Models.Windows
{
    class InterfaceElement
    {
        public string WindowName { get; set; }
        public ChromiumWebBrowser WebBrowser { get; set; }
        public Image Background { get; set; }
        public Image Screen { get; set; }
        public Image SongImage1 { get; set; }
        public Image SongImage2 { get; set; }
        public Image SongImage3 { get; set; }
        public ProgressBar SongOrderCount1 { get; set; }
        public ProgressBar SongOrderCount2 { get; set; }
        public ProgressBar SongOrderCount3 { get; set; }
        public TextBlock SongName1 { get; set; }
        public TextBlock SongName2 { get; set; }
        public TextBlock SongName3 { get; set; }
        public TextBlock SongID1 { get; set; }
        public TextBlock SongID2 { get; set; }
        public TextBlock SongID3 { get; set; }
        public TextBlock PlayingSongName { get; set; }
        public TextBlock Info { get; set; }
        public TextBlock Playlist { get; set; }
        public TextBlock OrderText { get; set; }
        public Button OrderButton { get; set; }       
    }
}
