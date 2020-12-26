using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ZHTV.Models.Objects
{
    class Theme
    {
        public string Name { get; set; }
        public string Background { get; set; }
        public string Song { get; set; }
        public string OrderCount { get; set; }
        public string Info { get; set; }
        public string Playlist { get; set; }

        public List<Theme> Music { get; set; }
        public List<Theme> Chat { get; set; }
    }
}
