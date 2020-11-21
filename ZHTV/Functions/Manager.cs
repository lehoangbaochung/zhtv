using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHTV.Models
{
    class Manager
    {
        static readonly string[] ArtistPhotoFolder = Directory.GetDirectories(@"D:\Youtube\Zither Harp TV\Artist\");
        static readonly string artistPhotoPath = @"D:\Youtube\Zither Harp TV\Artist\";
        public static List<Song> Playlist = new List<Song>();
        static readonly Random rd = new Random();
        static int count = Sheet.OrderList().Count;
        static int addIndex = 0;
        static int voteIndex = 0;

        // Số bài hát tối thiểu ở hàng chờ
        private static readonly int MinOrderSongNumber = 15;

        public static void CreateRandomPlaylist() // tạo 1 playlist chứa các bài hát ngẫu nhiên
        {
            Sheet.AccessSongSheet();
            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = Sheet.SongDict()[rd.Next(1, Sheet.SongDict().Count)];
                if (!Playlist.Contains(song))
                    Playlist.Add(song);
            }
        }

        public static string SongURL() // trả về địa chỉ của bài hát tiếp theo
        {
            return @"D:\Youtube\Zither Harp TV\Music\" + Playlist[0].ID + ".mp3";
        }

        public static string PlayingSongName() // trả về tên của bài hát đang phát
        {
            return " ▶ " + Playlist[0].ID + ": " + Sheet.SongDict()[Playlist[0].ID].Name + " - " + Sheet.SongDict()[Playlist[0].ID].Artist.Replace('&', '/');
        }   
        
        public static void FillNextSongs() // thêm bài hát mới sau khi phát xong một bài hát
        {
            Playlist.RemoveAt(0);
            while (Playlist.Count < MinOrderSongNumber)
            {
                var song = Sheet.SongDict()[rd.Next(1, Sheet.SongDict().Count)];
                if (!Playlist.Contains(song))
                    Playlist.Add(song);
            }

            if (addIndex > 0)
                addIndex--;
            if (voteIndex > 0)
                voteIndex--;
        }

        public static void OrderMultipleSongs(Dictionary<string, Order> orders)
        {
            if (Sheet.OrderList().Count > count)
            {
                for (int i = count; i < Sheet.OrderList().Count; i++)
                {
                    OrderSong(Sheet.OrderList().ElementAt(i).Value);
                }
                count = Sheet.OrderList().Count;
            }
        }

        private static void OrderSong(Order order)
        {
            var index = Playlist.FindIndex(s => s.ID == order.SongID);
            
            if (index == -1) // Bài hát chưa được order, thêm vào list
            {                
                var addSong = Sheet.SongDict()[order.SongID];
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

        public static string DisplayPlaylist()
        {
            string playlist = null;
            for (int i = 0; i < 15; i++)
                playlist += Playlist[i].ID + ": " + Playlist[i].Name + " ";
            return playlist;
        }

        public static string ArtistPhotoPath(string url)
        {
            string[] art = Sheet.SongDict()[Convert.ToInt32(url)].Artist.Split('&');
            string path = "";
            // check exist
            for (int i = art.Length - 1; i >= 0; i--)
            {
                if (Array.Exists(ArtistPhotoFolder, s => s.Equals(artistPhotoPath + art[i].Trim())) == true
                    || Array.Exists(ArtistPhotoFolder, s => s.Equals(artistPhotoPath + art[i].Replace('(', ' ').Replace(')', ' ').Trim())) == true)
                {
                    string[] photoAlbum = Directory.GetFiles(artistPhotoPath + art[i].Trim());
                    path = photoAlbum[rd.Next(0, photoAlbum.Length)];
                    break;
                }
                else
                    path = @"D:\Youtube\Zither Harp TV\Background\QC1.png";
            }
            return path;

            /*string[] photoAlbum = Directory.GetFiles(artistPhotoPath + art[rd.Next(0, art.Length)].Trim());
            path = photoAlbum[rd.Next(0, photoAlbum.Length)];*/
        }
    }
}
