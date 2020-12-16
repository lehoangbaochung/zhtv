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
                    element.Info.Text = Display.Info()[rd.Next(0, Display.Info().Length)];
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

        protected static void Player(InterfaceElement element, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                element.OrderText.Text = Display.SyntaxOrder();

                if (element.Player.NaturalDuration.HasTimeSpan == false) return;

                var position = (int)element.Player.Position.TotalSeconds;

                if (position > 10 && position < 60 || position > 70 && position < 120 || position > 190 && position < 240 || position > 240)
                {
                    element.PlayingSongName.Background = Brushes.Transparent;
                    element.PlayingSongName.Foreground = Brushes.Transparent;
                }

                if (position == 0 || position == 60 || position == 120 || position == 180)
                {
                    element.PlayingSongName.Background = Brushes.White;
                    element.PlayingSongName.Foreground = Brushes.Black;
                }

                if (element.Player.Position.TotalSeconds == element.Player.NaturalDuration.TimeSpan.TotalSeconds)
                {
                    Display.Screen(element);
                    Manage.Play(element);
                    Manage.FillNextSongs();
                    Display.SongBar(element);
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
                    new Youtube().Run(main.VideoId).Wait();
                    Display.SongBar(element);
                }
                catch (Exception)
                {
                    timer.Stop();
                }
            };

            timer.Start();
        }
    }

    class MusicTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Info(element, -1500, 1150, 0.0005);
            Playlist(element, -3500, 1200, 0.0005);
            Player(element, 1);
            Order(element, 20);
        }
    }

    class ChatTimer : Timer
    {
        public static void Start(InterfaceElement element)
        {
            Info(element, -1500, 600, 0.0005);
            Playlist(element, -3500, 900, 0.0005);
            Player(element, 1);
            Order(element, 20);
        }
    }
}
