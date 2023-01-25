using System.ComponentModel.DataAnnotations.Schema;

namespace eBusiness.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        public IFormFile Image { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? InstagramUrl { get; set; }
        public int PositionId { get; set; }
    }
}
