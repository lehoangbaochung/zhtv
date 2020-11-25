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

        public static string SongURL(Song s) // trả về địa chỉ của bài hát tiếp theo
        {
            return @"D:\Youtube\Zither Harp TV\Music\" + s.ID + ".mp3";
        }

        public static string SongID(Song s)
        {
            return s.ID.ToString();
        }

        public static string SongImage(Song s)
        {
            return @"D:\Youtube\Zither Harp TV\Album\" + s.ID + ".jpg";
        }

        public static string SongName(Song s)
        {
            return s.ID + ": " + s.Name;
        }

        public static string PlayingSongName() // trả về tên của bài hát đang phát
        {
            string PlayingSongName;
            if (Manager.Playlist[0].User.Count == 0)
                PlayingSongName = " Bài đang phát: " + Manager.Playlist[0].Name + " \n Thể hiện: " + Manager.Playlist[0].Artist
               + " \n Mã số: " + Manager.Playlist[0].ID;
            else
                PlayingSongName = " Khán giả yêu cầu nhiều nhất: " + Manager.Playlist[0].User.ElementAt(Manager.Playlist[0].User.Count - 1).Value
               + " \n Bài đang phát: " + Manager.Playlist[0].Name + " \n Thể hiện: " + Manager.Playlist[0].Artist + " \n Mã số: " + Manager.Playlist[0].ID;
            return PlayingSongName;
        }

        public static int OrderCount(Song s)
        {
            return s.User.Count;
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
            string[] info = { "Mọi thông tin đóng góp và ý kiến vui lòng gửi tin nhắn về trang facebook của kênh Zither Harp tại địa chỉ: http://m.me/zither.harp",
                "Nếu các bạn yêu thích Zither Harp xin hãy ủng hộ cho kênh qua tài khoản ngân hàng Viettinbank với số tài khoản 108869372829",
                "Để xem mã số của tất cả các bài hát vui lòng truy cập địa chỉ http://megaurl.in/cL4t",
                "Mọi yêu cầu vietsub bài hát vui lòng điền thông tin của bài hát đó tại địa chỉ http://megaurl.in/MAGJkie",
                "Để bình chọn cho ca khúc mình yêu thích vui lòng soạn tin theo cú pháp ZM mã_bài_hát hoặc ZMT tên_bài_hát gửi vào hộp thoại trò chuyện",
                "Kênh truyền hình âm nhạc tương tác trực tiếp dành cho giới trẻ ZHTV phát sóng đều đặn vào tối các ngày thứ 7 và chủ nhật hàng tuần. Kính mong các quý khán giả chú ý đón xem!",
                "Để giao lưu, học hỏi, trao đổi kiến thức với mọi người và chia sẻ, thảo luận về các ca khúc các bạn có thể tham gia vào nhóm I & Zither Harp " +
                "tại địa chỉ http://facebook.com/groups/zither.harp",
                "Từ ngày 1/11/2020, địa chỉ liên kết tải nhạc sẽ được để phía dưới phần bình luận của mỗi video được đăng tải (bình luận đã được ghim)",
                "Trước khi xem bất kỳ video vietsub nào trên kênh này hãy chắc chắn rằng phụ đề CC (phụ đề rời của Youtube) đã được bật 👌",
                "Từ ngày 9/9/2020, kênh ZHTV chính thức chuyển phát sóng từ kênh Zither Harp sang kênh Zither Harp TV trên hạ tầng của Youtube" };
            return info;
        }

        public static string SyntaxOrder()
        {
            return "Soạn tin: ZM " + Manager.SongDict.ElementAt(rd.Next(1, Manager.SongDict.Keys.Count)).Key + " gửi 6" + rd.Next(1, 7) + "77";
        }

        public static string ArtistPhotoPath(Song song)
        {
            string[] art = song.Artist.Split('&');
            string path = null;

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
