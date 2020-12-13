using System;
using System.Collections.Generic;
using System.Linq;

namespace ZHTV.Models
{
    class Manage
    {
        public static List<Song> Playlist = new List<Song>();
        public static Dictionary<int, Song> SongDict = Sheet.SongDict(Sheet.GetData("1ICOivODkrc4A86I1JVEQ0sVEa-8XriKoG5O4116xiKo", "Vietnamese!A2:E"));
        private static readonly string[] BannedUser = { "UCcsoaAFMYO9gOXDCjiB6fEw", "UCXiF7lSlg3tYTynabRiNURA"};
        static readonly Random rd = new Random();

        private static readonly int MinOrderSongNumber = 15;

        public static void Play(Element element)
        {
            element.Player.Source = new Uri(@"D:\Youtube\Zither Harp TV\Music\" + Playlist[0].ID + ".mp3"); 
        }

        public static void FillNextSongs()
        {
            if (Playlist.Count != 0)
            {
                Playlist[0].User.Clear();
                Playlist.RemoveAt(0);
            }

            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = SongDict.ElementAt(rd.Next(1, SongDict.Count)).Value;
                if (!Playlist.Contains(song))
                    Playlist.Add(song);
            }
        }

        public static void OrderSong(Order order)
        {
            if (Playlist.Where(s => s.User.ContainsKey(order.UserID)).Count() < 3) // mỗi acc chỉ được vote tối đa 3 bài 1 lần
            {
                var index = Playlist.FindIndex(s => s.ID == order.SongID);

                if (index == -1) // Bài hát chưa được order, thêm vào list
                {
                    var song = SongDict[order.SongID];
                    Sort(song, order);
                }
                else // nếu ng dùng vote bài đã có trong list
                {
                    var song = Playlist[index];

                    if (!song.User.ContainsKey(order.UserID)) // nếu ng dùng chưa vote bài này
                    {
                        Sort(song, order);
                    }
                }
            }       
        }

        private static void Sort(Song song, Order order)
        {
            song.User.Add(order.UserID, order.UserName);
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
