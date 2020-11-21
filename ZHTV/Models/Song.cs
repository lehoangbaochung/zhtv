using System.Collections.Generic;

namespace ZHTV.Models
{
    public class Song
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Artist { set; get; }
        public List<string> UserID = new List<string>();

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Song song = (Song)obj;
            return this.ID == song.ID;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
