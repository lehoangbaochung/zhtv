using System.Windows;
using ZHTV.Models;
using ZHTV.Functions;

namespace ZHTV.Interface
{
    public partial class Music : Window
    {
        public Music()
        {
            InitializeComponent();

            var screen = new Screen
            {
                Image = imgScreen,
                PlayingSongName = txtNextSong
            };

            var element = new Element
            {
                Player = Player,
                Info = txtInfo,
                SyntaxOrder = txtOrder
            };

            var songBar = new MusicSongBar
            {
                SongName1 = txtSong1,
                SongName2 = txtSong2,
                SongName3 = txtSong3,
                SongOrderCount1 = prbSong1,
                SongOrderCount2 = prbSong2,
                SongOrderCount3 = prbSong3,
                Playlist = txtPlaylist
            };

            Manage.FillNextSongs();
            Manage.Play(element);
            Display.Screen(screen);
            Display.MusicSongBar(songBar);
            Timer.Music(element, screen, songBar);
        }   
    }
}