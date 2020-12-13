using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZHTV.Models;

namespace ZHTV.Functions
{
    class Display
    {
        static readonly Random rd = new Random();

        public static void Screen(Screen screen)
        {
            DisplayElement.ArtistImage(screen.Image);
            DisplayElement.PlayingSongName(screen.PlayingSongName);
        }

        public static void MusicSongBar(MusicSongBar songBar)
        {
            DisplayElement.SongName(songBar.SongName1, songBar.SongName2, songBar.SongName3);
            DisplayElement.SongOrderCount(songBar.SongOrderCount1, songBar.SongOrderCount2, songBar.SongOrderCount3);
            DisplayElement.Playlist(songBar.Playlist);
        }

        public static void ChatSongBar(ChatSongBar songBar)
        {
            DisplayElement.SongID(songBar.SongID1, songBar.SongID2, songBar.SongID3);
            DisplayElement.SongOrderCount(songBar.SongOrderCount1, songBar.SongOrderCount2, songBar.SongOrderCount3);
            DisplayElement.AlbumImage(songBar.SongImage1, songBar.SongImage2, songBar.SongImage3);
            DisplayElement.Playlist(songBar.Playlist);
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
            return "Soạn tin: ZM " + Manage.SongDict.ElementAt(rd.Next(1, Manage.SongDict.Keys.Count)).Key + " gửi 6" + rd.Next(1, 7) + "77";
        }
    }

    class DisplayElement
    {
        public static void SongID(TextBlock txtb1, TextBlock txtb2, TextBlock txtb3)
        {
            txtb1.Text = Manage.Playlist[0].ID.ToString();
            txtb2.Text = Manage.Playlist[1].ID.ToString();
            txtb3.Text = Manage.Playlist[2].ID.ToString();
        }

        public static void SongName(TextBlock txtb1, TextBlock txtb2, TextBlock txtb3)
        {
            txtb1.Text = Manage.Playlist[0].ID + ": " + Manage.Playlist[0].Name;
            txtb2.Text = Manage.Playlist[1].ID + ": " + Manage.Playlist[1].Name;
            txtb3.Text = Manage.Playlist[2].ID + ": " + Manage.Playlist[2].Name;
        }

        public static void SongOrderCount(ProgressBar prb1, ProgressBar prb2, ProgressBar prb3)
        {
            prb1.Value = Manage.Playlist[0].User.Count * 10 + 10;
            prb2.Value = Manage.Playlist[1].User.Count * 10 + 10;
            prb3.Value = Manage.Playlist[2].User.Count * 10 + 10;
        }

        public static void Playlist(TextBlock txtb)
        {
            txtb.Text = null;

            for (int i = 0; i < 15; i++)
                txtb.Text += Manage.Playlist[i].ID + ": " + Manage.Playlist[i].Name + " ";
        }

        public static void PlayingSongName(TextBlock txtb)
        {
            if (Manage.Playlist[0].User.Count == 0)
                txtb.Text = " Bài đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist
               + " \n Mã số: " + Manage.Playlist[0].ID;
            else
                txtb.Text = " Khán giả yêu cầu nhiều nhất: " + Manage.Playlist[0].User.ElementAt(Manage.Playlist[0].User.Count - 1).Value
               + " \n Bài đang phát: " + Manage.Playlist[0].Name + " \n Thể hiện: " + Manage.Playlist[0].Artist + " \n Mã số: " + Manage.Playlist[0].ID;
        }

        public static void ArtistImage(Image img)
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            try
            {      
                bi.UriSource = new Uri(Manage.Playlist[0].ArtistUri);
            }
            catch (Exception)
            {
                bi.UriSource = new Uri(@"D:\Code\C#\ZHTV\ZHTV\Images\Background\ZHTV.png");
            }
            bi.EndInit();

            img.Source = bi;
        }

        public static void AlbumImage(Image img1, Image img2, Image img3)
        {
            var albumImage = new Image[3] { img1, img2, img3 };
            var bi = new BitmapImage();

            for (int i = 0; i < 3; i++)
            {
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                try
                {
                    bi.UriSource = new Uri(Manage.Playlist[i].AlbumUri);
                }
                catch (Exception)
                {
                    bi.UriSource = new Uri(@"D:\Code\C#\ZHTV\ZHTV\Images\Background\zh-music-project.jpg");
                }
                bi.EndInit();

                albumImage[i].Source = bi;
            }
        }
    }
}
