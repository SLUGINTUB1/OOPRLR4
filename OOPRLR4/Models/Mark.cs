using NuGet.DependencyResolver;

namespace OOPRLR4.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public int Value { get; set; }
        public int SubjectIdent { get; set; }
        public bool Evaluated { get; set; }

        public int ExamId { get; set; } // Required foreign key property
        public virtual Exam Exam { get; set; }
    }
}
