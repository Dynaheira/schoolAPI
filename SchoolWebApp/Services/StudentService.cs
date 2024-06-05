using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using System.Runtime.ExceptionServices;

namespace SchoolWebApp.Services {
	public class StudentService {
		private ApplicationDbContext _dbContext;

		public StudentService(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IEnumerable<StudentDTO> GetStudents() {
			var allStudents = _dbContext.Students;
			var studentsDTOs = new List<StudentDTO>();
			foreach (var student in allStudents) {
                studentsDTOs.Add(modelToDTO(student));
            }
            return studentsDTOs;
		}

        private static StudentDTO modelToDTO(Student student) {
            return new StudentDTO {
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Id = student.Id
            };
        }

        public async Task AddStudentAsync(StudentDTO studentDTO) {
            await _dbContext.Students.AddAsync(DTOToModel(studentDTO));
            await _dbContext.SaveChangesAsync();
        }

        private static Student DTOToModel(StudentDTO studentDTO) {
            return new Student {
                DateOfBirth = studentDTO.DateOfBirth,
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Id = studentDTO.Id
            };
        }

        private async Task<Student> VerifyIfExist(int id) {
            var student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
            if (student == null) {
                return null;
            }
            return student;
        }

        internal async Task<StudentDTO> GetStudentByIdAsync(int id) {
			var student = await VerifyIfExist(id);
                return modelToDTO(student);
        }

        internal async Task UpdateAsync(int id, StudentDTO studentDTO) {
			_dbContext.Students.Update(DTOToModel(studentDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var studentToDelete = await _dbContext.Students.FirstOrDefaultAsync(student =>student.Id == id);
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
