using System.Windows;
using ZHTV.Models;
using ZHTV.Functions;
using ZHTV.Functions.Online;
using ZHTV.Models.Windows;

namespace ZHTV.Interface
{
    public partial class Music : Window
    {
        public Music()
        {
            InitializeComponent();

            var element = new InterfaceElement
            {
                WindowName = Name,
                Player = Player,
                Info = txtInfo,
                Screen = imgScreen,
                OrderText = txtOrder,
                PlayingSongName = txtNextSong,
                SongName1 = txtSong1,
                SongName2 = txtSong2,
                SongName3 = txtSong3,
                SongOrderCount1 = prbSong1,
                SongOrderCount2 = prbSong2,
                SongOrderCount3 = prbSong3,
                Playlist = txtPlaylist,
            };

            Manage.FillNextSongs();
            Manage.Play(element);
            Display.Screen(element);
            Display.SongBar(element);
            MusicTimer.Start(element);
        }   
    }
}