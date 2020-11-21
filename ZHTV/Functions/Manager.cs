using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZHTV.Functions;

namespace ZHTV.Models
{
    class Manager
    {
        public static List<Song> Playlist = new List<Song>();
        public static Dictionary<int, Song> SongDict = Sheet.SongDict(Sheet.GetData("1ICOivODkrc4A86I1JVEQ0sVEa-8XriKoG5O4116xiKo", "A2:C"));
        public static Dictionary<string, Order> OrderList = Sheet.OrderList(Sheet.GetData("1oycbgNJEVF9cqLE8l701YZyyk4fkRT5P9XbMov4UiAI", "12/2020!A3:H"));
        static readonly Random rd = new Random();
        static int count = OrderList.Count;
        static int addIndex = 0;

        private static readonly int MinOrderSongNumber = 15; // Số bài hát tối thiểu ở hàng chờ

        public static void CreateRandomPlaylist() // tạo 1 playlist chứa các bài hát ngẫu nhiên
        {           
            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = SongDict.ElementAt(rd.Next(1, SongDict.Count)).Value;
                if (!Playlist.Contains(song))
                    Playlist.Add(song);   
            }
        }

        public static void FillNextSongs() // thêm bài hát mới sau khi phát xong một bài hát
        {
            Playlist.RemoveAt(0);
            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = SongDict.ElementAt(rd.Next(1, SongDict.Count)).Value;
                if (!Playlist.Contains(song))
                    Playlist.Add(song);
            }

            if (addIndex > 0)
                addIndex--;
        }

        public static void OrderMultipleSongs()
        {
            OrderList = Sheet.OrderList(Sheet.GetData("1oycbgNJEVF9cqLE8l701YZyyk4fkRT5P9XbMov4UiAI", "12/2020!A3:H"));
            if (OrderList.Count > count)
            {
                for (int i = count; i < OrderList.Count; i++)
                {
                    OrderSong(OrderList.ElementAt(i).Value);
                }
                count = OrderList.Count;
            }
        }

        private static void OrderSong(Order order)
        {
            var index = Playlist.FindIndex(s => s.ID == order.SongID);
            
            if (index == -1) // Bài hát chưa được order, thêm vào list
            {                
                var addSong = SongDict[order.SongID];
                addSong.UserID.Add(order.UserID);
                Playlist.Insert(addIndex, addSong);
                addIndex++; 
            }
            else // nếu ng dùng vote bài đã có trong list
            {
                var song = Playlist[index];

                if (song.UserID.Count == 0)
                {
                    song.UserID.Add(order.UserID);
                    Playlist.Insert(addIndex, song);
                    addIndex++;
                }
                else
                {
                    if (!song.UserID.Contains(order.UserID)) // nếu ng dùng chưa vote bài này
                    {
                        song.UserID.Add(order.UserID);
                        for (int i = 0; i < Playlist.Count; i++)
                        {
                            for (int j = i + 1; j < Playlist.Count; j++)
                            {
                                if (Playlist[j].UserID.Count > Playlist[i].UserID.Count)
                                {
                                    var tmp = Playlist[i];
                                    Playlist[i] = Playlist[j];
                                    Playlist[j] = tmp;
                                }
                            }
                        }
                    }
                }
            }      
        }
    }
}
