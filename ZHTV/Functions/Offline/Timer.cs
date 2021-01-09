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
        static double tick = 0;

        protected static void Info(InterfaceElement element, double d1, double d2, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                var left = element.Info.Margin.Left;
                left -= 2;
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
                left -= 2;
                element.Playlist.Margin = new Thickness(left, element.Playlist.Margin.Top, element.Playlist.Margin.Right, element.Playlist.Margin.Bottom);

                if (left == d1)
                {
                    left = d2;
                    element.Playlist.Margin = new Thickness(left, element.Playlist.Margin.Top, element.Playlist.Margin.Right, element.Playlist.Margin.Bottom);
                }
            };

            timer.Start();
        }

        protected static void Player(InterfaceElement element, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                tick += 1;
                element.OrderText.Text = Display.SyntaxOrder();

                if (Math.Truncate(tick / 10) % 5 == 0)
                {
                    element.PlayingSongName.Background = Brushes.White;
                    element.PlayingSongName.Foreground = Brushes.Black;
                }
                else
                {
                    element.PlayingSongName.Background = Brushes.Transparent;
                    element.PlayingSongName.Foreground = Brushes.Transparent;
                }

                if (tick == Manage.PlayingSong.Duration)
                {
                    DisplayElement.PlayingSongName(element);
                    Manage.Play(element);
                    Manage.FillNextSongs();
                    Display.SongBar(element);

                    tick = 0;
                }
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
    }

    class MusicTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Player(element, 1);
            Order(element, 20);
            Info(element, -3000, 1150, 0.001);
            Playlist(element, -4000, 1200, 0.001);
        }
    }

    class ChatTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Player(element, 1);
            Order(element, 20);
            Info(element, -1500, 600, 0.001);
            Playlist(element, -3500, 900, 0.001);  
        }
    }
}
