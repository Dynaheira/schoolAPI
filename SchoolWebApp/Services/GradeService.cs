using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using SchoolWebApp.ViewModels;

namespace SchoolWebApp.Services {
    public class GradeService {
        private ApplicationDbContext _dbContext;

        public GradeService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<GradesDropdownsViewModel> GetGradesDropdownsData() {
            var gradesDropdownsData = new GradesDropdownsViewModel() {
                Students = await _dbContext.Students.OrderBy(student => student.LastName).ToListAsync(),
                Subjects = await _dbContext.Subjects.OrderBy(subject => subject.Name).ToListAsync()
            };
            return gradesDropdownsData;
        }

        internal async Task CreateAsync(GradeDTO gradeDTO) {
            Grade gradeToInsert = await DTOToModel(gradeDTO);
            await _dbContext.AddAsync(gradeToInsert);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Grade> DTOToModel(GradeDTO gradeDTO) {
            return new Grade() {
                Date = DateTime.Today,
                Mark = gradeDTO.Mark,
                Topic = gradeDTO.Topic,
                Student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == gradeDTO.StudentId),
                Subject = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == gradeDTO.SubjectId),
                Id = gradeDTO.Id
            };
        }

        public async Task<IEnumerable<GradesViewModel>>
            GetAllVMsAsync() {
            List<Grade> grades = await _dbContext.Grades.Include(gr=>gr.Student).Include(gr=>gr.Subject).ToListAsync();
            List<GradesViewModel> gradeVMS = new List<GradesViewModel>();
            foreach (Grade grade in grades) {
                gradeVMS.Add(new GradesViewModel {
                    Date = grade.Date,
                    Id = grade.Id,
                    Mark = grade.Mark,
                    StudentName = $"{grade.Student.LastName} {grade.Student.FirstName}",
                    SubjectName = $"Subject: {grade.Subject.Name}",
                    Topic = $"Topic: {grade.Topic}"
                });
            }
            return gradeVMS;
        }

        internal async Task<Grade> GetByIdAsync(int id) {
            return await _dbContext.Grades.Include(gr=>gr.Student).Include(gr=>gr.Subject).FirstOrDefaultAsync(grade => grade.Id == id);
        }

        internal GradeDTO ModelToDTO(Grade grade) {
            return new GradeDTO {
                Id = grade.Id,
                Date = grade.Date,
                Mark = grade.Mark,
                StudentId = grade.Student.Id,
                Topic = grade.Topic
            };
        }

        internal async Task UpdateAsync(int id, GradeDTO gradeDTO) {
            Grade updateGrade = await DTOToModel(gradeDTO);
            _dbContext.Grades.Update(updateGrade);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var gradeToDelete = await _dbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
            _dbContext.Grades.Remove(gradeToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
