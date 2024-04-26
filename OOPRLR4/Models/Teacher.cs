namespace OOPRLR4.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int SubjectIdent { get; set; }

        public List<Faculty> Faculties { get; set; } = [];
    }
}
