namespace OOPRLR4.Models
{
    public class Applicant
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public List<Faculty> Faculties { get; set; } = [];
    }
}
