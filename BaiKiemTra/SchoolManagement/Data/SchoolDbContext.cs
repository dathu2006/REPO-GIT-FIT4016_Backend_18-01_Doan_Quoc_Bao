using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập Unique Index cho các trường dữ liệu quan trọng
            modelBuilder.Entity<School>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.StudentId).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();

            // Cấu hình tên cột cho giống với database bạn đang dùng
            modelBuilder.Entity<School>(entity => {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Principal).HasColumnName("principal");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<Student>(entity => {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SchoolId).HasColumnName("school_id");
                entity.Property(e => e.FullName).HasColumnName("full_name");
                entity.Property(e => e.StudentId).HasColumnName("student_id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // --- SEED DATA: 10 SCHOOLS ---
            modelBuilder.Entity<School>().HasData(
                new School { Id = 1, Name = "Green Valley High", Principal = "Dr. Jonathan Smith", Address = "123 Education St, CA", CreatedAt = DateTime.Now },
                new School { Id = 2, Name = "West River Academy", Principal = "Mrs. Sarah Johnson", Address = "456 River Rd, NY", CreatedAt = DateTime.Now },
                new School { Id = 3, Name = "North Star Institute", Principal = "Mr. Robert Brown", Address = "789 North Way, WA", CreatedAt = DateTime.Now },
                new School { Id = 4, Name = "Elite Preparatory", Principal = "Ms. Emily Davis", Address = "101 Scholar Blvd, MA", CreatedAt = DateTime.Now },
                new School { Id = 5, Name = "Unity International", Principal = "Dr. Michael Wilson", Address = "202 Global Ave, TX", CreatedAt = DateTime.Now },
                new School { Id = 6, Name = "Blue Ridge High", Principal = "Mrs. Linda Garcia", Address = "303 Mountain Dr, CO", CreatedAt = DateTime.Now },
                new School { Id = 7, Name = "Horizon Science", Principal = "Mr. David Martinez", Address = "404 Future Ln, NV", CreatedAt = DateTime.Now },
                new School { Id = 8, Name = "Maple Leaf School", Principal = "Ms. Karen Anderson", Address = "505 Forest Pkwy, VT", CreatedAt = DateTime.Now },
                new School { Id = 9, Name = "Ocean View Academy", Principal = "Mr. James Taylor", Address = "606 Coastline Hwy, FL", CreatedAt = DateTime.Now },
                new School { Id = 10, Name = "Summit Peak School", Principal = "Mrs. Susan Thomas", Address = "707 Peak Rd, UT", CreatedAt = DateTime.Now }
            );

            // --- SEED DATA: 20 STUDENTS ---
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, SchoolId = 1, FullName = "Alice Thompson", StudentId = "STU001", Email = "alice.t@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 2, SchoolId = 1, FullName = "Bob Miller", StudentId = "STU002", Email = "bob.m@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 3, SchoolId = 2, FullName = "Charlie Davis", StudentId = "STU003", Email = "charlie.d@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 4, SchoolId = 2, FullName = "Diana Prince", StudentId = "STU004", Email = "diana.p@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 5, SchoolId = 3, FullName = "Edward Norton", StudentId = "STU005", Email = "edward.n@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 6, SchoolId = 3, FullName = "Fiona Gallagher", StudentId = "STU006", Email = "fiona.g@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 7, SchoolId = 4, FullName = "George Clooney", StudentId = "STU007", Email = "george.c@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 8, SchoolId = 4, FullName = "Hannah Montana", StudentId = "STU008", Email = "hannah.m@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 9, SchoolId = 5, FullName = "Ian Wright", StudentId = "STU009", Email = "ian.w@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 10, SchoolId = 5, FullName = "Julia Roberts", StudentId = "STU010", Email = "julia.r@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 11, SchoolId = 6, FullName = "Kevin Hart", StudentId = "STU011", Email = "kevin.h@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 12, SchoolId = 6, FullName = "Laura Palmer", StudentId = "STU012", Email = "laura.p@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 13, SchoolId = 7, FullName = "Mike Tyson", StudentId = "STU013", Email = "mike.t@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 14, SchoolId = 7, FullName = "Nina Simone", StudentId = "STU014", Email = "nina.s@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 15, SchoolId = 8, FullName = "Oscar Isaac", StudentId = "STU015", Email = "oscar.i@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 16, SchoolId = 8, FullName = "Peter Parker", StudentId = "STU016", Email = "peter.p@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 17, SchoolId = 9, FullName = "Quinn Fabray", StudentId = "STU017", Email = "quinn.f@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 18, SchoolId = 9, FullName = "Riley Reid", StudentId = "STU018", Email = "riley.r@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 19, SchoolId = 10, FullName = "Steve Rogers", StudentId = "STU019", Email = "steve.r@school.edu", CreatedAt = DateTime.Now },
                new Student { Id = 20, SchoolId = 10, FullName = "Tony Stark", StudentId = "STU020", Email = "tony.s@school.edu", CreatedAt = DateTime.Now }
            );
        }
    }
}