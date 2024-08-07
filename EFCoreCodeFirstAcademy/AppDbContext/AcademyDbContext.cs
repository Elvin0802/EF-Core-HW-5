using EFCoreCodeFirstAcademy.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstAcademy.AppDbContext;

public class AcademyDbContext : DbContext
{
	public DbSet<Department> Departments { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Faculty> Facultys { get; set; }
	public DbSet<Group> Groups { get; set; }

	public AcademyDbContext()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer
			(@"Server = (localdb)\MSSQLLocalDB;Integrated Security = SSPI; Database = AcademyTaskDB;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Group>(g =>
		{
			g.HasKey(x => x.GroupId);

			g.Property(x => x.GroupId)
					 .IsRequired()
					 .HasColumnName("Id")
					 .ValueGeneratedOnAdd();

			g.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(10);

			g.HasIndex(x => x.Name)
				.IsUnique();

			g.Property(x => x.Rating)
				.IsRequired();

			g.Property(x => x.Year)
				.IsRequired();

			g.HasOne(t => t.Teacher)
				.WithMany(h => h.Groups)
				.HasForeignKey(d => d.TeacherId);

			g.ToTable(x => x
					 .HasCheckConstraint("CK_Group_RatingRange", @"Rating >= 0 AND Rating <= 5"));

			g.ToTable(x => x
				 .HasCheckConstraint("CK_Group_YearRange", @"Year >= 1 AND Year <= 5"));

			g.ToTable(x => x
				 .HasCheckConstraint("CK_Group_IdEmpty", @"[Id] != ''"));

			g.ToTable(x => x
				 .HasCheckConstraint("CK_Group_NameEmpty", @"[Name] != ''"));

			g.ToTable(x => x
				 .HasCheckConstraint("CK_Group_YearEmpty", @"[Year] != ''"));

			g.ToTable(x => x
				 .HasCheckConstraint("CK_Group_RatingEmpty", @"[Rating] != ''"));
		});

		modelBuilder.Entity<Department>(d =>
		{
			d.HasKey(e => e.DepartmentId);

			d.Property(x => x.DepartmentId)
					 .IsRequired()
					 .HasColumnName("Id")
					 .ValueGeneratedOnAdd();

			d.Property(e => e.Financing)
				.HasColumnType("money")
				.IsRequired()
				.HasDefaultValue(0);

			d.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);

			d.HasIndex(e => e.Name)
				.IsUnique();

			d.ToTable(x => x
				 .HasCheckConstraint("CK_Department_IdEmpty", @"[Id] != ''"));

			d.ToTable(x => x
				 .HasCheckConstraint("CK_Department_NameEmpty", @"[Name] != ''"));

			d.ToTable(x => x
				 .HasCheckConstraint("CK_Department_FinancingEmpty", @"[Financing] != ''"));

			d.ToTable(x => x
				 .HasCheckConstraint("CK_Department_FinancingValue", @"[Financing] >= 0"));

		});

		modelBuilder.Entity<Faculty>(f =>
		{
			f.HasKey(x => x.FacultyId);

			f.Property(x => x.FacultyId)
					 .IsRequired()
					 .HasColumnName("Id")
					 .ValueGeneratedOnAdd();

			f.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			f.HasIndex(x => x.Name)
				.IsUnique();

			f.ToTable(x => x
				 .HasCheckConstraint("CK_Faculty_IdEmpty", @"[Id] != ''"));

			f.ToTable(x => x
				 .HasCheckConstraint("CK_Faculty_NameEmpty", @"[Name] != ''"));
		});

		modelBuilder.Entity<Teacher>(t =>
		{
			t.HasKey(x => x.TeacherId);

			t.Property(x => x.TeacherId)
				.IsRequired()
				.HasColumnName("Id")
				.ValueGeneratedOnAdd();

			t.Property(x => x.EmploymentDate)
				.IsRequired()
				.HasDefaultValue(DateTime.Now)
				.HasColumnType("date");

			t.Property(x => x.FirstName)
				.HasColumnName("Name")
				.IsRequired();

			t.Property(x => x.LastName)
				.HasColumnName("Surname")
				.IsRequired();

			t.Property(x => x.Salary)
				.HasColumnType("money")
				.IsRequired();

			t.Property(x => x.Premium)
				.HasColumnType("money")
				.IsRequired()
				.HasDefaultValue(0);

			t.HasOne(d => d.Department)
				.WithMany(p => p.Teachers)
				.HasForeignKey(d => d.DepartmentId);

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[Id] != ''"));

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[Name] != ''"));

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[Surname] != ''"));

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[Salary] != ''"));

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[Premium] != ''"));

			t.ToTable(x => x
				 .HasCheckConstraint("CK_Teacher_IdEmpty", @"[EmploymentDate] != ''"));
		});

		modelBuilder.Entity<Student>(s =>
		{
			s.HasKey(x => x.StudentId);

			s.Property(x => x.StudentId)
				.HasColumnName("Id")
				.IsRequired();

			s.Property(x => x.FirstName)
				.IsRequired();

			s.Property(x => x.LastName)
				.IsRequired();

			s.HasOne(d => d.Group)
				.WithMany(p => p.Students)
				.HasForeignKey(d => d.GroupId);

			s.HasOne(d => d.Faculty)
				.WithMany(p => p.Students)
				.HasForeignKey(d => d.FacultyId);

			s.Property(x => x.FirstName)
				.HasColumnName("Name")
				.IsRequired();

			s.Property(x => x.LastName)
				.HasColumnName("Surname")
				.IsRequired();

			s.ToTable(x => x
				 .HasCheckConstraint("CK_Student_IdEmpty", @"[Id] != ''"));

			s.ToTable(x => x
				 .HasCheckConstraint("CK_Student_NameEmpty", @"[Name] != ''"));

			s.ToTable(x => x
				 .HasCheckConstraint("CK_Student_SurnameEmpty", @"[Surname] != ''"));
		});
	}
}
