namespace ServiceApp.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
    }
}