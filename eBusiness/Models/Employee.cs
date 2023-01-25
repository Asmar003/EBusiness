using eBusiness.Models.Base;

namespace eBusiness.Models
{
    public class Employee:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramUrl { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
