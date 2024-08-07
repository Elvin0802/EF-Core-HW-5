namespace EFCoreCodeFirstAcademy.EntityModels;

public class Teacher
{
	public int TeacherId { get; set; }
	public DateTime EmploymentDate { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public decimal Salary { get; set; }
	public decimal Premium { get; set; }

	public List<Group> Groups { get; set; }

	public int DepartmentId { get; set; }
	public Department Department { get; set; }

	public Teacher()
	{
		Groups = new();
	}
}
