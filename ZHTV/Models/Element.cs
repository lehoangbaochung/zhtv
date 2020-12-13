using System.Windows.Controls;

namespace ZHTV.Models
{
    class Main
    {
        public string VideoId { set; get; }
    }

    class Screen
    {
        public Image Image { set; get; }
        public TextBlock PlayingSongName { set; get; }
    }

    class Element
    {
        public MediaElement Player { set; get; }
        public TextBlock SyntaxOrder { set; get; }
        public TextBlock Info { set; get; }
    }

    class MusicSongBar
    {
        public ProgressBar SongOrderCount1 { set; get; }
        public ProgressBar SongOrderCount2 { set; get; }
        public ProgressBar SongOrderCount3 { set; get; }
        public TextBlock SongName1 { set; get; }
        public TextBlock SongName2 { set; get; }
        public TextBlock SongName3 { set; get; }
        public TextBlock Playlist { set; get; }
    }
    
    class ChatSongBar
    {
        public ProgressBar SongOrderCount1 { set; get; }
        public ProgressBar SongOrderCount2 { set; get; }
        public ProgressBar SongOrderCount3 { set; get; }
        public TextBlock SongID1 { set; get; }
        public TextBlock SongID2 { set; get; }
        public TextBlock SongID3 { set; get; }
        public Image SongImage1 { set; get; }
        public Image SongImage2 { set; get; }
        public Image SongImage3 { set; get; }
        public TextBlock Playlist { set; get; }
        public Button SyntaxOrder { set; get; }
    }
}
