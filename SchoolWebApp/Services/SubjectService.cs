using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using System.Runtime.ExceptionServices;

namespace SchoolWebApp.Services {
    public class SubjectService {
        private ApplicationDbContext _dbContext;

        public SubjectService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<SubjectDTO> GetSubjects() {
            var allSubjects = _dbContext.Subjects;
            var subjectsDTOs = new List<SubjectDTO>();
            foreach (var subject in allSubjects) {
                subjectsDTOs.Add(modelToDTO(subject));
            }
            return subjectsDTOs;
        }



        public async Task AddSubjectAsync(SubjectDTO subjectDTO) {
            await _dbContext.Subjects.AddAsync(DTOToModel(subjectDTO));
            await _dbContext.SaveChangesAsync();
        }

        private static Subject DTOToModel(SubjectDTO subjectDTO) {
            return new Subject {
                Id = subjectDTO.Id,
                Name = subjectDTO.Name
            };
        }

        private async Task<Subject> VerifyIfExist(int id) {
            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
            if (subject == null) {
                return null;
            }
            return subject;
        }

        internal async Task<SubjectDTO> GetSubjectByIdAsync(int id) {
            var subject = await VerifyIfExist(id);
            return modelToDTO(subject);
        }

        private static SubjectDTO modelToDTO(Subject subject) {
            return new SubjectDTO {

                Id = subject.Id,
                Name = subject.Name
            };
        }

        internal async Task UpdateAsync(int id, SubjectDTO subjectDTO) {
            _dbContext.Subjects.Update(DTOToModel(subjectDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var subjectToDelete = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
            _dbContext.Subjects.Remove(subjectToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
