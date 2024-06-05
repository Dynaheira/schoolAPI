using Microsoft.EntityFrameworkCore;

namespace SchoolWebApp.Models {
	public class ApplicationDbContext : DbContext {
		public ApplicationDbContext(DbContextOptions options) : base(options) {
		}

		public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
