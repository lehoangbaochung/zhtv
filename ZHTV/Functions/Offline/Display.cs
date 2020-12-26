using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZHTV.Functions.Online;
using ZHTV.Models.Objects;
using ZHTV.Models.Windows;

namespace ZHTV.Functions
{
    class Display : DisplayElement
    {
        static readonly Random rd = new Random();
        public static readonly List<string> InfoList = new List<string>();
        public static readonly List<Theme> ThemeList = new List<Theme>();

        public static void Screen(InterfaceElement element)
        {
            ArtistImage(element);
            PlayingSongName(element);
        }

        public static void SongBar(InterfaceElement element)
        {
            if (element.WindowName == "MusicUI")
            {
                SongName(element);
                SongOrderCount(element);
                Playlist(element);
            }
            else if (element.WindowName == "ChatUI")
            {
                SongID(element);
                AlbumImage(element);
                SongOrderCount(element);
                Playlist(element);
            }    
        }

        public static string SyntaxOrder()
        {
            return "Soạn tin: ZM " + Manage.Songlist[rd.Next(1, Manage.Songlist.Count)].ID + " gửi 6" + rd.Next(1, 7) + "77";
        }

        public static void Theme(InterfaceElement element)
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;

            try
            {
                bi.UriSource = new Uri(ThemeList[1].Background);
                bi.EndInit();
            }
            catch (Exception)
            {
                bi.UriSource = null;
                bi.EndInit();
            }

            element.Screen.Source = bi;
        }
    }

    class DisplayElement
    {
        protected static void SongID(InterfaceElement element)
        {
            element.SongID1.Text = Manage.Playlist[0].ID.ToString();
            element.SongID2.Text = Manage.Playlist[1].ID.ToString();
            element.SongID3.Text = Manage.Playlist[2].ID.ToString();
        }

        protected static void SongName(InterfaceElement element)
        {
            element.SongName1.Text = Manage.Playlist[0].ID + ": " + Manage.Playlist[0].Name;
            element.SongName2.Text = Manage.Playlist[1].ID + ": " + Manage.Playlist[1].Name;
            element.SongName3.Text = Manage.Playlist[2].ID + ": " + Manage.Playlist[2].Name;

            if (Manage.Playlist[0].Code == 1)
            {
                element.SongName1.Text = "?: ??? (" + Manage.Playlist[0].Artist + ")";
            }
            
            if (Manage.Playlist[1].Code == 1)
            {
                element.SongName2.Text = "?: ??? (" + Manage.Playlist[1].Artist + ")";
            }
            
            if (Manage.Playlist[2].Code == 1)
            {
                element.SongName3.Text = "?: ??? (" + Manage.Playlist[2].Artist + ")";
            } 
        }

        protected static void SongOrderCount(InterfaceElement element)
        {
            element.SongOrderCount1.Value = Manage.Playlist[0].User.Count * 10 + 10;
            element.SongOrderCount2.Value = Manage.Playlist[1].User.Count * 10 + 10;
            element.SongOrderCount3.Value = Manage.Playlist[2].User.Count * 10 + 10;
        }

        protected static void Playlist(InterfaceElement element)
        {
            element.Playlist.Text = null;

            for (int i = 0; i < 15; i++)
            {
                if (Manage.Playlist[i].Code != 1)
                    element.Playlist.Text += Manage.Playlist[i].ID + ": " + Manage.Playlist[i].Name + " ";
                else
                    element.Playlist.Text += "?: ??? (" + Manage.Playlist[i].Artist + ") ";
            }
        }

        protected static void PlayingSongName(InterfaceElement element)
        {
            if (Manage.Playlist[0].User.Count == 0)
                element.PlayingSongName.Text = " Đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist 
                + " \n Mã số: " + Manage.Playlist[0].ID + " ";
            else
                element.PlayingSongName.Text = " Khán giả yêu cầu nhiều nhất: " + Manage.Playlist[0].User.ElementAt(Manage.Playlist[0].User.Count - 1).Value
                + " \n Đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist + " \n Mã số: " + Manage.Playlist[0].ID + " ";
        }

        protected static void ArtistImage(InterfaceElement element)
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;

            try
            {      
                bi.UriSource = new Uri(Manage.Playlist[0].ArtistUri);
                bi.EndInit();
            }
            catch (Exception)
            {
                bi.UriSource = null;
                bi.EndInit();
            }

            element.Screen.Source = bi;
        }

        protected static void AlbumImage(InterfaceElement element)
        {
            var albumImage = new Image[3] { element.SongImage1, element.SongImage2, element.SongImage3 };
            var bi = new BitmapImage();

            for (int i = 0; i < 3; i++)
            {
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;

                try
                {
                    bi.UriSource = new Uri(Manage.Playlist[i].AlbumUri);
                    bi.EndInit();
                }
                catch (Exception)
                {
                    bi.UriSource = null;
                    bi.EndInit();
                }

                albumImage[i].Source = bi;
            }
        }
    }
}
