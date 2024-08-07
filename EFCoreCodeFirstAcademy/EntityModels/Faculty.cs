namespace EFCoreCodeFirstAcademy.EntityModels;

public class Faculty
{
	public int FacultyId { get; set; }
	public string Name { get; set; }

	public List<Student> Students { get; set; }

	public Faculty()
	{
		Students = new();
	}
}
