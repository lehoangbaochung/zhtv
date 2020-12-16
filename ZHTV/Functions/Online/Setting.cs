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
        public static void SongDictionary(MainWindowElement element)
        {
            if (element.SheetTab == null)
                Sheet.Bind(Sheet.Get(element.SheetId, element.SheetRange));
            else
                Sheet.Bind(Sheet.Get(element.SheetId, element.SheetTab + "!" + element.SheetRange));
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
