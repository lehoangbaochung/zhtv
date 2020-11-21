using System;
using System.IO;
using System.Linq;
using ZHTV.Models;

namespace ZHTV.Functions
{
    class Display
    {
        static readonly string[] ArtistPhotoFolder = Directory.GetDirectories(@"D:\Youtube\Zither Harp TV\Artist\");
        static readonly string artistPhotoPath = @"D:\Youtube\Zither Harp TV\Artist\";
        static readonly Random rd = new Random();

        public static string SongURL() // trả về địa chỉ của bài hát tiếp theo
        {
            return @"D:\Youtube\Zither Harp TV\Music\" + Manager.Playlist[0].ID + ".mp3";
        }

        public static string SongID(Song s)
        {
            return s.ID.ToString();
        }

        public static string SongImage(Song s)
        {
            return @"D:\Youtube\Zither Harp TV\Album\" + s.ID + ".jpg";
        }

        public static string PlayingSongName() // trả về tên của bài hát đang phát
        {
            return " ▶ " + Manager.Playlist[0].ID + ": " + Manager.Playlist[0].Name + " - " + Manager.Playlist[0].Artist.Replace('&', '/');
        }

        public static int OrderCount(Song s)
        {
            return s.UserID.Count;
        }

        public static string Playlist()
        {
            string playlist = null;
            for (int i = 0; i < 15; i++)
                playlist += Manager.Playlist[i].ID + ": " + Manager.Playlist[i].Name + " ";
            return playlist;
        }

        public static string[] Info()
        {
            string[] info = { "Mọi thông tin đóng góp và ý kiến xin vui lòng gửi tin nhắn về trang facebook của kênh tại địa chỉ: m.me/zither.harp",
                "Nếu các bạn yêu mến Zither Harp xin hãy ủng hộ mình qua tài khoản ngân hàng Viettinbank với số tài khoản 108869372829",
                "Để xem mã số của tất cả các bài hát vui lòng truy cập địa chỉ http://megaurl.in/cL4t",
                "Nếu các bạn muốn yêu cầu vietsub bài hát hãy điền thông tin của bài hát đó tại địa chỉ http://megaurl.in/MAGJkie" };
            return info;
        }

        public static string SyntaxOrder()
        {
            return "ZM " + Manager.SongDict.ElementAt(rd.Next(1, Manager.SongDict.Keys.Count)).Key + "\ngửi\n6" + rd.Next(1, 7) + "77";
        }

        public static string ArtistPhotoPath(int id)
        {
            string[] art = Manager.SongDict[id].Artist.Split('&');
            // check exist
            string path = null;
            for (int i = art.Length - 1; i >= 0; i--)
            {
                if (Array.Exists(ArtistPhotoFolder, s => s.Equals(artistPhotoPath + art[i].Trim())) == true
                || Array.Exists(ArtistPhotoFolder, s => s.Equals(artistPhotoPath + art[i].Replace('(', ' ').Replace(')', ' ').Trim())) == true)
                {
                    string[] photoAlbum = Directory.GetFiles(artistPhotoPath + art[i].Trim());
                    path = photoAlbum[rd.Next(0, photoAlbum.Length)];
                }
                else
                {
                    path = @"D:\Youtube\Zither Harp TV\Background\QC1.png";
                }
            }
            return path;

            /*string[] photoAlbum = Directory.GetFiles(artistPhotoPath + art[rd.Next(0, art.Length)].Trim());
            path = photoAlbum[rd.Next(0, photoAlbum.Length)];*/
        }
    }
}
