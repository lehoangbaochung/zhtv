using System.Collections.Generic;

namespace ZHTV.Models
{
    class Song
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Artist { set; get; }
        public string AlbumUri { set; get; }
        public string ArtistUri { set; get; }
        public Dictionary<string, string> User = new Dictionary<string, string>();

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Song song = (Song)obj;
            return ID == song.ID;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
