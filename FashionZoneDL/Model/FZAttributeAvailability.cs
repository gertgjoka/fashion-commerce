using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    public class FZAttributeAvailability
    {
        public int Id { get; set; }
        public int Availability { get; set; }
        public string Value{ get; set; }
    }

    [Serializable]
    public struct FZAttributeVersion
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
    }
}
