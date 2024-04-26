namespace OOPRLR4.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public int NumberOfStudents { get; set; }

        public List<Teacher> Teachers { get; set; } = [];
        public List<Applicant> Applicants { get; set; } = [];
    }
}
