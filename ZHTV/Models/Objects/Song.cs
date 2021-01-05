using System.Collections.Generic;

namespace ZHTV.Models.Objects
{
    class Song
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public double Duration { get; set; }
        public string PlayerUri { get; set; }
        public string AlbumUri { get; set; }
        public string ArtistUri { get; set; }
        public int Code { get; set; }
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
