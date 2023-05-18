namespace MISA.WebFresher032023.Demo.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
    }
}
