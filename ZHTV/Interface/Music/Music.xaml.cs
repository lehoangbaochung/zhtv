using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZHTV.Functions;
using ZHTV.Models;

namespace ZHTV.Interface.Music
{
    public partial class Music : Window
    {
        readonly Random rd = new Random();

        public Music()
        {
            InitializeComponent();
            Player.Source = new Uri(Display.SongURL(Manager.SongDict.ElementAt(rd.Next(1, Manager.SongDict.Count)).Value));
            Manager.FillNextSongs();
            Interface();
            Timer();
        }

        private void Timer()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };
            var SyntaxVoteTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };
            var OrderTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };

            timer.Tick += (s, e) =>
            {
                var x1 = txtPlaylist.Margin.Left;
                var x2 = txtInfo.Margin.Left;
                x1--;
                x2--;
                txtPlaylist.Margin = new Thickness(x1, txtPlaylist.Margin.Top, txtPlaylist.Margin.Right, txtPlaylist.Margin.Bottom);
                txtInfo.Margin = new Thickness(x2, txtInfo.Margin.Top, txtInfo.Margin.Right, txtInfo.Margin.Bottom);

                if (x1 == -3500)
                {
                    x1 = 1200;
                    txtPlaylist.Margin = new Thickness(x1, txtPlaylist.Margin.Top, txtPlaylist.Margin.Right, txtPlaylist.Margin.Bottom);
                }

                if (x2 == -1500)
                {
                    x2 = 1000;
                    txtInfo.Margin = new Thickness(x2, txtInfo.Margin.Top, txtInfo.Margin.Right, txtInfo.Margin.Bottom);
                    txtInfo.Text = Display.Info()[rd.Next(0, Display.Info().Length)];
                }
            };

            SyntaxVoteTimer.Tick += (s, e) =>
            {
                txtOrder.Text = Display.SyntaxOrder();

                if (Player.Position == Player.NaturalDuration)
                {
                    Player.Source = new Uri(Display.SongURL(Manager.Playlist[0]));
                    DisplayScreen();
                    Manager.FillNextSongs();
                    Interface();
                }
            };

            OrderTimer.Tick += (s, e) =>
            {
                Manager.OrderMultipleSongs();
                Interface();
            };

            timer.Start();
            SyntaxVoteTimer.Start();
            OrderTimer.Start();
        }

        private void DisplayScreen()
        {
            // hiển thị tên bài đang phát
            txtNextSong.Text = Display.PlayingSongName();
            txtNextSong.Background = Brushes.White;

            // hiển thị ảnh ca sĩ
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi.UriSource = new Uri(Display.ArtistPhotoPath(Manager.Playlist[0]));
            bi.EndInit();
            imgScreen.Source = bi;
            imgScreen.Stretch = Stretch.Uniform;
        }

        private void Interface()
        {
            // hiển thị playlist
            txtPlaylist.Text = Display.Playlist();
            // hiển thị mã số bài hát
            txtSong1.Text = Display.SongName(Manager.Playlist[0]);
            txtSong2.Text = Display.SongName(Manager.Playlist[1]);
            txtSong3.Text = Display.SongName(Manager.Playlist[2]);
            // hiển thị số lượt vote
            if (prbSong1.Value < prbSong1.Maximum && prbSong2.Value < prbSong2.Maximum && prbSong3.Value < prbSong3.Maximum)
            {
                prbSong1.Value = Display.OrderCount(Manager.Playlist[0]) * 10 + 10;
                prbSong2.Value = Display.OrderCount(Manager.Playlist[1]) * 10 + 10;
                prbSong3.Value = Display.OrderCount(Manager.Playlist[2]) * 10 + 10;
            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
