using System;
using System.Linq;
using ZHTV.Models.Windows;
using ZHTV.Models.Objects;
using System.Collections.Generic;

namespace ZHTV.Functions.Online
{
    class Manage
    {
        static readonly Random rd = new Random();
        static readonly int MinOrderSongNumber = 15;
        public static readonly List<Song> Playlist = new List<Song>();
        public static readonly List<Song> SongList = new List<Song>();
        public static Song NextSong;

        public static void Play(InterfaceElement element)
        {
            element.WebBrowser.Load("https://www.youtube.com/embed/" + Playlist[0].PlayerUri + "?autoplay=1");
        }

        public static void FillNextSongs()
        {
            if (Playlist.Count != 0)
            {
                NextSong = Playlist[0];
                Playlist[0].User.Clear();
                Playlist[0].Code = -1;
                Playlist.RemoveAt(0);
            }

            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = SongList[rd.Next(0, SongList.Count - 1)];

                if (!Playlist.Contains(song)) Playlist.Add(song);
            }
        }

        public static void OrderSong(Order order)
        {
            if (Playlist.Where(s => s.User.ContainsKey(order.UserID)).Count() < 3) // mỗi acc chỉ được vote tối đa 3 bài 1 lần
            {
                var index = Playlist.FindIndex(s => s.ID == order.SongID);

                if (index == -1) // Bài hát chưa được order, thêm vào list
                {
                    var song = SongList.Find(s => s.ID == order.SongID);

                    Sort(song, order);
                }
                else // nếu ng dùng vote bài đã có trong list
                {
                    var song = Playlist[index];

                    if (!song.User.ContainsKey(order.UserID)) Sort(song, order);
                }
            }   
        }

        private static void Sort(Song song, Order order)
        {
            song.User.Add(order.UserID, order.UserName);
            song.Code = order.Code;

            /// if (order.UserID == "UCux2CF0xv1BgErYwysx-NyA") song.User.Add("ClownLord", order.UserName);

            var newSong = song;
            Playlist.Remove(song);

            for (int i = 0; i < Playlist.Count; i++)
            {
                if (newSong.User.Count > Playlist[i].User.Count)
                {
                    Playlist.Insert(i, newSong);
                    break;
                }
            }
        }
    }
}