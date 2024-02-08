using System;

namespace BPKS.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public int PartyHostId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
        public string ImageStyle { get; set; }
        public string ImageStatus { get; set; }
    }
}
