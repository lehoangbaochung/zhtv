using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZHTV.Models;
using ZHTV.Functions;

namespace ZHTV
{
    public partial class MainWindow : Window
    {
        readonly Random rd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Manager.CreateRandomPlaylist();
            Run();
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
                    x1 = 900;
                    txtPlaylist.Margin = new Thickness(x1, txtPlaylist.Margin.Top, txtPlaylist.Margin.Right, txtPlaylist.Margin.Bottom);    
                }

                if (x2 == -1200)
                {
                    x2 = 900;
                    txtInfo.Margin = new Thickness(x2, txtInfo.Margin.Top, txtInfo.Margin.Right, txtInfo.Margin.Bottom);
                    txtInfo.Text = Display.Info()[rd.Next(0, Display.Info().Length)];
                }
            };

            SyntaxVoteTimer.Tick += (s, e) =>
            {
                btnSyntax.Content = Display.SyntaxOrder();
                // play mp3
                if (Player.Position == Player.NaturalDuration)
                {
                    Run();
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

        private void Run()
        {
            Player.Source = new Uri(Display.SongURL());
            imgScreen.Source = new BitmapImage(new Uri(Display.ArtistPhotoPath(Manager.Playlist[0].ID)));
            imgScreen.Stretch = Stretch.Uniform;
            Manager.FillNextSongs();
        }

        private void Interface()
        {
            // hiển thị playlist
            txtPlaylist.Text = Display.Playlist();
            // hiển thị mã số bài hát
            txt1.Text = Display.SongID(Manager.Playlist[0]);
            txt2.Text = Display.SongID(Manager.Playlist[1]);
            txt3.Text = Display.SongID(Manager.Playlist[2]);
            // hiển thị ảnh bìa bài hát
            img1.Source = new BitmapImage(new Uri(Display.SongImage(Manager.Playlist[0])));
            img2.Source = new BitmapImage(new Uri(Display.SongImage(Manager.Playlist[1])));
            img3.Source = new BitmapImage(new Uri(Display.SongImage(Manager.Playlist[2])));
            // hiển thị số lượt vote
            if (prbar1.Value < prbar1.Maximum && prbar2.Value < prbar2.Maximum && prbar3.Value < prbar3.Maximum)
            {
                prbar1.Value = Display.OrderCount(Manager.Playlist[0]) * 10 + 10;
                prbar2.Value = Display.OrderCount(Manager.Playlist[1]) * 10 + 10;
                prbar3.Value = Display.OrderCount(Manager.Playlist[2]) * 10 + 10;
            }
            imgFrame.Source = new BitmapImage(new Uri(@"C:\Users\Asus\Downloads\free-digital-Christmas-frame-removebg-preview.png"));
            imgFrame.Stretch = Stretch.Fill;
        }
    }
}
