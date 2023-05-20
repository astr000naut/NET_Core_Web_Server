namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class EmployeeFilteredListDto
    {
        public int TotalRecord { get; set; }
        public List<EmployeeDto> FilteredList { get; set; }
    }
}