using NuGet.Protocol.Plugins;

namespace OOPRLR4.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Mark> Subjects { get; set; } = new List<Mark>();
    }
}
