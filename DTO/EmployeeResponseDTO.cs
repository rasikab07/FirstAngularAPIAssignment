namespace AngularApiAssignment1.DTO
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public List<string> Skills {get;set;}

    }
}
