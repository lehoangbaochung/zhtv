using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZHTV.Functions.Online;
using ZHTV.Models.Windows;

namespace ZHTV.Functions
{
    class Display : DisplayElement
    {
        static readonly Random rd = new Random();

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

        public static string[] Info()
        {
            string[] info = { "Mọi thông tin đóng góp và ý kiến vui lòng gửi tin nhắn về trang facebook của kênh Zither Harp tại địa chỉ: http://m.me/zither.harp",
                "Nếu các bạn yêu thích Zither Harp xin hãy ủng hộ cho kênh qua tài khoản ngân hàng Viettinbank với số tài khoản 108869372829",
                "Để xem mã số của tất cả các bài hát vui lòng truy cập địa chỉ http://megaurl.in/cL4t",
                "Mọi yêu cầu vietsub bài hát vui lòng điền thông tin của bài hát đó tại địa chỉ http://megaurl.in/requestsong",
                "Để bình chọn cho ca khúc mình yêu thích vui lòng soạn tin theo cú pháp ZM mã_bài_hát hoặc ZMT tên_bài_hát (tên_ca_sĩ) gửi vào hộp thoại trò chuyện",
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
            return "Soạn tin: ZM " + Sheet.SongDictionary.ElementAt(rd.Next(1, Sheet.SongDictionary.Keys.Count)).Key + " gửi 6" + rd.Next(1, 7) + "77";
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
                element.Playlist.Text += Manage.Playlist[i].ID + ": " + Manage.Playlist[i].Name + " ";
        }

        public static void PlayingSongName(InterfaceElement element)
        {
            if (Manage.Playlist[0].User.Count == 0)
                element.PlayingSongName.Text = " Đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist 
                + " \n Mã số: " + Manage.Playlist[0].ID + " ";
            else
                element.PlayingSongName.Text = " Khán giả yêu cầu nhiều nhất: " + Manage.Playlist[0].User.ElementAt(Manage.Playlist[0].User.Count - 1).Value
                + " \n Đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist + " \n Mã số: " + Manage.Playlist[0].ID + " ";
        }

        public static void ArtistImage(InterfaceElement element)
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
                bi.UriSource = new Uri(@"D:\Code\C#\ZHTV\ZHTV\Images\Background\ZHTV.png");
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
                    bi.UriSource = new Uri(@"D:\Code\C#\ZHTV\ZHTV\Images\Background\zh-music-project.jpg");
                    bi.EndInit();
                }

                albumImage[i].Source = bi;
            }
        }
    }
}
