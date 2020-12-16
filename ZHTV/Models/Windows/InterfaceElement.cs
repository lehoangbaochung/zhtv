using System.Windows.Controls;

namespace ZHTV.Models.Windows
{
    class InterfaceElement
    {
        public string WindowName { set; get; }
        public MediaElement Player { set; get; }
        public Image Screen { set; get; }
        public Image SongImage1 { set; get; }
        public Image SongImage2 { set; get; }
        public Image SongImage3 { set; get; }
        public ProgressBar SongOrderCount1 { set; get; }
        public ProgressBar SongOrderCount2 { set; get; }
        public ProgressBar SongOrderCount3 { set; get; }
        public TextBlock SongName1 { set; get; }
        public TextBlock SongName2 { set; get; }
        public TextBlock SongName3 { set; get; }
        public TextBlock SongID1 { set; get; }
        public TextBlock SongID2 { set; get; }
        public TextBlock SongID3 { set; get; }
        public TextBlock PlayingSongName { set; get; }
        public TextBlock Info { set; get; }
        public TextBlock Playlist { set; get; }
        public TextBlock OrderText { set; get; }
        public Button OrderButton { set; get; }
    }
}
