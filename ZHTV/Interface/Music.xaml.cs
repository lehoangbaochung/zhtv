using System.Windows;
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
        }

        private void MusicUI_Loaded(object sender, RoutedEventArgs e)
        {
            wbPlayer.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
        }

        private void OnIsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (wbPlayer.IsBrowserInitialized)
            {
                var element = new InterfaceElement
                {
                    WindowName = Name,
                    WebBrowser = wbPlayer,
                    Info = txtInfo,
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

                Manage.Play(element);
                Display.SongBar(element);
                MusicTimer.Start(element);
            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }
    }
}