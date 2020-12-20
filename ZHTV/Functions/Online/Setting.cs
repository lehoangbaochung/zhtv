using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHTV.Models;
using ZHTV.Models.Objects;
using ZHTV.Models.Windows;

namespace ZHTV.Functions.Online
{
    class Setting
    {
        public static Dictionary<int, Song> SongDict = new Dictionary<int, Song>();

        public static void Start()
        {
            
        }

        public static string MusicFolderPath(Song song)
        {
            MainWindowElement element = new MainWindowElement();

            return element.MusicFolderPath + @"\" + song.ID + "." + element.FormatMusicFile;
        }

        public static void Save()
        {
            
        }
    }
}
