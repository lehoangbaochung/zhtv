﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZHTV.Models;
using ZHTV.Functions;
using System.Linq;

namespace ZHTV
{
    public partial class Chat : Window
    {
        readonly Random rd = new Random();
        //static Song nextSong;

        public Chat()
        {
            InitializeComponent();
            Player.Source = new Uri(Display.SongURL(Manage.SongDict.ElementAt(rd.Next(1, Manage.SongDict.Count)).Value));
            Manage.FillNextSongs();
            Manage.Playlist.RemoveAt(0);
            Interface();
            Timer();
        }

        private void Timer()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };
            var SyntaxVoteTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
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

                if (x2 == -1500)
                {
                    x2 = 600;
                    txtInfo.Margin = new Thickness(x2, txtInfo.Margin.Top, txtInfo.Margin.Right, txtInfo.Margin.Bottom);
                    txtInfo.Text = Display.Info()[rd.Next(0, Display.Info().Length)];
                }   
            };

            SyntaxVoteTimer.Tick += (s, e) =>
            {
                //btnSyntax.Content = Display.SyntaxOrder();
                //btnSyntax.Foreground = Brushes.White;
                //btnSyntax.FontWeight = FontWeights.Bold;

                //if (Player.NaturalDuration.HasTimeSpan != true) return; // kiểm tra thời lượng media
                //txtTilte.Text = Math.Truncate(Player.Position.TotalSeconds) + "";
                //if (Math.Truncate(Player.Position.TotalSeconds) == Math.Truncate(Player.NaturalDuration.TimeSpan.TotalSeconds - 20))
                //{
                //    nextSong = Manager.Playlist[0];
                //    if (nextSong.User.Count == 0)
                //        txtNextSong.Text = " Bài tiếp theo: " + nextSong.Name + "\n Thể hiện: " + nextSong.Artist;
                //    else
                //        txtNextSong.Text = " Khán giả yêu cầu nhiều nhất: " + nextSong.User.ElementAt(0).Value + "\n Bài tiếp theo: " + nextSong.Name + "\n Thể hiện: " + nextSong.Artist;

                //    txtNextSong.Background = Brushes.White;
                //}

                //if (Math.Truncate(Player.Position.TotalSeconds) == Math.Truncate(Player.NaturalDuration.TimeSpan.TotalSeconds - 10))
                //{
                //    txtNextSong.Text = null;
                //    txtNextSong.Background = Brushes.Transparent;
                //}

                if (Player.Position == Player.NaturalDuration)
                {
                    Player.Source = new Uri(Display.SongURL(Manage.Playlist[0]));
                    DisplayScreen();
                    Manage.FillNextSongs();
                    Interface();
                }  
            };

            OrderTimer.Tick += (s, e) =>
            {
                Manage.OrderMultipleSongs();
                Interface();
            };

            timer.Start();
            SyntaxVoteTimer.Start();
            OrderTimer.Start();
        }

        private void DisplayScreen()
        {
            // hiển thị tên bài đang phát
            if (Manage.Playlist[0].User.Count == 0)
                txtNextSong.Text = " Bài đang phát: " + Manage.Playlist[0].Name + "\n Thể hiện: " + Manage.Playlist[0].Artist
               + "\n Mã số: " + Manage.Playlist[0].ID;
            else
                txtNextSong.Text = " Khán giả yêu cầu nhiều nhất: " + Manage.Playlist[0].User.ElementAt(0).Value
               + "\n Bài đang phát: " + Manage.Playlist[0].Name + "\n Thể hiện: " + Manage.Playlist[0].Artist + "\n Mã số: " + Manage.Playlist[0].ID;
            txtNextSong.Background = Brushes.White;

            // hiển thị ảnh ca sĩ
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi.UriSource = new Uri(Display.ArtistPhotoPath(Manage.Playlist[0]));
            bi.EndInit();
            imgScreen.Source = bi;
            imgScreen.Stretch = Stretch.Uniform;
            //imgFrame.Source = new BitmapImage(new Uri(@"C:\Users\Asus\Downloads\istockphoto-1080953840-1024x1024-removebg-preview.png"));
            //imgFrame.Stretch = Stretch.Fill;

            //if (imgScreen.Width < imgScreen.Height)
            //    imgFrame.RenderTransform = new RotateTransform(90);
        }

        private void Interface()
        {
            // hiển thị playlist
            txtPlaylist.Text = Display.Playlist();
            // hiển thị mã số bài hát
            txt1.Text = Display.SongID(Manage.Playlist[0]);
            txt2.Text = Display.SongID(Manage.Playlist[1]);
            txt3.Text = Display.SongID(Manage.Playlist[2]);
            // hiển thị ảnh bìa bài hát
            var bi1 = new BitmapImage();
            bi1.BeginInit();
            bi1.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi1.UriSource = new Uri(Display.SongImage(Manage.Playlist[0]));
            bi1.EndInit();
            img1.Source = bi1;
            //
            var bi2 = new BitmapImage();
            bi2.BeginInit();
            bi2.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi2.UriSource = new Uri(Display.SongImage(Manage.Playlist[1]));
            bi2.EndInit();
            img2.Source = bi2;
            //
            var bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi3.UriSource = new Uri(Display.SongImage(Manage.Playlist[2]));
            bi3.EndInit();
            img3.Source = bi3;
            // hiển thị số lượt vote
            if (prbar1.Value < prbar1.Maximum && prbar2.Value < prbar2.Maximum && prbar3.Value < prbar3.Maximum)
            {
                prbar1.Value = Display.OrderCount(Manage.Playlist[0]) * 10 + 10;
                prbar2.Value = Display.OrderCount(Manage.Playlist[1]) * 10 + 10;
                prbar3.Value = Display.OrderCount(Manage.Playlist[2]) * 10 + 10;
            } 
        }

        private void btnSyntax_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
