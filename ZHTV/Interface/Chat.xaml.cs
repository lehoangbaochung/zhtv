using System.Windows;
using ZHTV.Functions;
using ZHTV.Functions.Online;
using ZHTV.Models.Windows;

namespace ZHTV
{
    public partial class Chat : Window
    {
        public Chat()
        {
            InitializeComponent();

            var element = new InterfaceElement
            {
                Player = Player,
                Info = txtInfo,
                Screen = imgScreen,
                OrderButton = btnSyntax,
                PlayingSongName = txtNextSong,
                SongID1 = txtSong1,
                SongID2 = txtSong2,
                SongID3 = txtSong3,
                SongImage1 = imgSong1,
                SongImage2 = imgSong2,
                SongImage3 = imgSong3,
                SongOrderCount1 = prbSong1,
                SongOrderCount2 = prbSong2,
                SongOrderCount3 = prbSong3,
                Playlist = txtPlaylist
            };

            Manage.Play(element);
            Display.Screen(element);
            Display.SongBar(element);
            ChatTimer.Start(element);
        }
    }
}
