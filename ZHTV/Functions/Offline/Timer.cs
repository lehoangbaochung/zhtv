using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using ZHTV.Functions.Online;
using ZHTV.Models.Windows;

namespace ZHTV.Functions
{
    class Timer
    {
        static readonly Random rd = new Random();
        static int tick = 0;

        protected static void Info(InterfaceElement element, double d1, double d2, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                var left = element.Info.Margin.Left;
                left--;
                element.Info.Margin = new Thickness(left, element.Info.Margin.Top, element.Info.Margin.Right, element.Info.Margin.Bottom);

                if (left == d1)
                {
                    left = d2;
                    element.Info.Margin = new Thickness(left, element.Info.Margin.Top, element.Info.Margin.Right, element.Info.Margin.Bottom);
                    element.Info.Text = Display.InfoList[rd.Next(0, Display.InfoList.Count)];
                }
            };

            timer.Start();
        }

        protected static void Playlist(InterfaceElement element, double d1, double d2, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                var left = element.Playlist.Margin.Left;
                left--;
                element.Playlist.Margin = new Thickness(left, element.Playlist.Margin.Top, element.Playlist.Margin.Right, element.Playlist.Margin.Bottom);

                if (left == d1)
                {
                    left = d2;
                    element.Playlist.Margin = new Thickness(left, element.Playlist.Margin.Top, element.Playlist.Margin.Right, element.Playlist.Margin.Bottom);
                }
            };

            timer.Start();
        }

        protected static void Player(InterfaceElement element, double span, double duration, double delay)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                tick += 1;
                SongName(element);
                Player(element, duration, delay);
            };

            timer.Start();
        }  

        protected static void Order(InterfaceElement element, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };
            var main = new MainWindowElement() { VideoId = ((MainWindow)Application.Current.Windows[0]).tbxVideoID.Text };

            timer.Tick += (s, e) =>
            {
                try
                {
                    Youtube.Order(main.VideoId);
                    Display.SongBar(element);
                }
                catch (Exception) { }
            };

            timer.Start();
        }

        private static void SongName(InterfaceElement element)
        {
            element.OrderText.Text = Display.SyntaxOrder();

            if (tick > 10 && tick < 60 || tick > 70 && tick < 120 || tick > 190 && tick < 240 || tick > 240)
            {
                element.PlayingSongName.Background = Brushes.Transparent;
                element.PlayingSongName.Foreground = Brushes.Transparent;
            }
            else
            {
                element.PlayingSongName.Background = Brushes.White;
                element.PlayingSongName.Foreground = Brushes.Black;
            }
        }

        private static void Player(InterfaceElement element, double duration, double delay)
        {
            if (tick == duration + delay)
            {
                DisplayElement.PlayingSongName(element);
                Manage.Play(element);
                Manage.FillNextSongs();
                Display.SongBar(element);
                
                tick = 0;
            }
        }
    }

    class MusicTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Info(element, -3000, 1150, 0.001);
            Playlist(element, -4000, 1200, 0.001);
            Player(element, 1, Manage.Playlist[0].Duration, 0);
            Order(element, 20);
        }
    }

    class ChatTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Info(element, -1500, 600, 0.001);
            Playlist(element, -3500, 900, 0.001);
            Player(element, 1, Manage.Playlist[0].Duration, 0);
            Order(element, 20);
        }
    }
}
