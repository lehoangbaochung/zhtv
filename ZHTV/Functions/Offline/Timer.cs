using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ZHTV.Models;

namespace ZHTV.Functions
{
    class Timer
    {
        static readonly Random rd = new Random();

        public static void Music(Element element, Screen screen, MusicSongBar songBar)
        {
            Info(element.Info, -1500, 1150);
            Playlist(songBar.Playlist, -3500, 1200);
            MusicPlayer(element, screen, songBar);
            MusicOrder(songBar);
        }

        public static void Chat(Element element, Screen screen, ChatSongBar songBar)
        {
            Info(element.Info, -1500, 600);
            Playlist(songBar.Playlist, -3500, 900);
            ChatPlayer(songBar, screen, element);
            //Order();
        }

        private static void Info(TextBlock textBlock, double d1, double d2)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };

            timer.Tick += (s, e) =>
            {
                var left = textBlock.Margin.Left;
                left--;
                textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);

                if (left == d1)
                {
                    left = d2;
                    textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
                    textBlock.Text = Display.Info()[rd.Next(0, Display.Info().Length)];
                }
            };

            timer.Start();
        }

        private static void Playlist(TextBlock textBlock, double d1, double d2)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };

            timer.Tick += (s, e) =>
            {
                var left = textBlock.Margin.Left;
                left--;
                textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);

                if (left == d1)
                {
                    left = d2;
                    textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
                }
            };

            timer.Start();
        }

        private static void MusicPlayer(Element element, Screen screen, MusicSongBar songBar)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };

            timer.Tick += (s, e) =>
            {
                element.SyntaxOrder.Text = Display.SyntaxOrder();

                if (element.Player.Position == element.Player.NaturalDuration)
                {
                    Manage.Play(element);
                    Display.Screen(screen);
                    Manage.FillNextSongs();
                    Display.MusicSongBar(songBar);
                }
            };

            timer.Start();
        }

        private static void ChatPlayer(ChatSongBar songBar, Screen screen, Element element)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };

            timer.Tick += (s, e) =>
            {
                element.SyntaxOrder.Text = Display.SyntaxOrder();

                if (element.Player.Position == element.Player.NaturalDuration)
                {
                    Manage.Play(element);
                    Display.Screen(screen);
                    Manage.FillNextSongs();
                    Display.ChatSongBar(songBar);
                }
            };

            timer.Start();
        }

        private static void MusicOrder(MusicSongBar songBar)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(20) };
            var main = new Main() { VideoId = ((MainWindow)Application.Current.Windows[0]).tbxVideoID.Text };

            timer.Tick += (s, e) =>
            {
                try
                {
                    new Youtube().Run(main.VideoId).Wait();
                    Display.MusicSongBar(songBar);
                }
                catch (Exception)
                {
                    timer.Stop();
                }
            };

            timer.Start();
        }
    }
}
