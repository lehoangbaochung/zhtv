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
        public static Dictionary<string, Order> OrderList = Sheet.OrderList(Sheet.GetData("1oycbgNJEVF9cqLE8l701YZyyk4fkRT5P9XbMov4UiAI", "11/2020!A3:H"));
        public static string[] BannedUser = { "UCXFJWYITT6jlkuzZOoa980g", "UCcsoaAFMYO9gOXDCjiB6fEw", "UCXiF7lSlg3tYTynabRiNURA"};
        static readonly Random rd = new Random();
        static int count = OrderList.Count;
        static int addIndex = 0;

        private static readonly int MinOrderSongNumber = 15; // Số bài hát tối thiểu ở hàng chờ

        public static void FillNextSongs() // thêm bài hát mới sau khi phát xong một bài hát
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

            if (addIndex > 0)
                addIndex--;
        }

        public static void OrderMultipleSongs()
        {
            OrderList = Sheet.OrderList(Sheet.GetData("1oycbgNJEVF9cqLE8l701YZyyk4fkRT5P9XbMov4UiAI", "11/2020!A3:H"));
            if (OrderList.Count > count)
            {
                for (int i = count; i < OrderList.Count; i++)
                {
                    if (!BannedUser.Contains(OrderList.ElementAt(i).Value.UserID))
                        OrderSong(OrderList.ElementAt(i).Value);
                }
                count = OrderList.Count;

                //if (!BannedUser.Contains(OrderList.ElementAt(count).Value.UserID))
                //    OrderSong(OrderList.ElementAt(count).Value);
                //count += 1;
            }
        }

        private static void OrderSong(Order order)
        {
            var index = Playlist.FindIndex(s => s.ID == order.SongID);
            
            if (index == -1) // Bài hát chưa được order, thêm vào list
            {                
                var oldSong = SongDict[order.SongID];               
                oldSong.User.Add(order.UserID, order.UserName);
                var newSong = oldSong;
                Playlist.Remove(oldSong);
                Sort(newSong);
            }
            else // nếu ng dùng vote bài đã có trong list
            {
                var oldSong = Playlist[index];

                if (!oldSong.User.ContainsKey(order.UserID)) // nếu ng dùng chưa vote bài này
                {
                    oldSong.User.Add(order.UserID, order.UserName);
                    var newSong = oldSong;
                    Playlist.Remove(oldSong);
                    Sort(newSong);
                }
            }      
        }

        private static void Sort(Song s)
        {
            for (int i = 0; i < Playlist.Count; i++)
            {
                if (s.User.Count > Playlist[i].User.Count)
                {
                    Playlist.Insert(i, s);
                    break;
                }
            }
        }
    }
}
