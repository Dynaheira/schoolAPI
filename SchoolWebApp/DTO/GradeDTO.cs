using SchoolWebApp.Models;

namespace SchoolWebApp.DTO {
    public class GradeDTO {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }
    }
}
