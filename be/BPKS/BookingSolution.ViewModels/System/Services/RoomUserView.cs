namespace BookingSolution.ViewModels.System.Services
{
    public class RoomUserView
    {
        public int RoomId { get; set; }

        public string? RoomName { get; set; }

        public string? RoomUrl { get; set; }

        public string? RoomType { get; set; }

        public double? Price { get; set; }

        //public string? RoomStatus { get; set; }
        public List<ProductUserView> productUserViews { get; set; }
    }
}
