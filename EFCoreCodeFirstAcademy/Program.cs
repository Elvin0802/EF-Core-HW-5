using EFCoreCodeFirstAcademy.AppDbContext;
using EFCoreCodeFirstAcademy.EntityModels;

using AcademyDbContext db = new();

var d = new Department() { Financing = 19, Name="Department1", Teachers = new() };

var f = new Faculty() { Name="Faculty1", Students = new() };

var t = new Teacher();
t.FirstName = "RandomName";
t.LastName = "RandomSurname";
t.Salary = 2000;
t.EmploymentDate = DateTime.Now.AddMonths(-17);
t.Premium = 45;
t.Groups = new();

d.Teachers.Add(t);

List<Student> s = [
		new Student() { FirstName="Random1", LastName="Random2" },
		new Student() { FirstName="Random3", LastName="Random4" }];

var g = new Group() { Name = "Group1", Rating=3, Year=2, Students = s };

f.Students.AddRange(s);

t.Groups.Add(g);

db.Facultys.AddRange(f);
db.Departments.AddRange(d);
db.Students.AddRange(s);
db.Teachers.AddRange(t);
db.Groups.AddRange(g);

db.SaveChanges();

