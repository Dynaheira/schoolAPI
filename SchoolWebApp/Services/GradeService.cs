using SchoolWebApp.Models;

namespace SchoolWebApp.Services {
    public class GradeService {
        private ApplicationDbContext _dbContext;

        public GradeService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

    }
}
