namespace EFCoreCodeFirstAcademy.EntityModels;

public class Group
{
	public int GroupId { get; set; }
	public string Name { get; set; }
	public int Rating { get; set; }
	public int Year { get; set; }

	public List<Student> Students { get; set; }

	public int TeacherId { get; set; }
	public Teacher Teacher { get; set; }

	public Group()
	{
		Students = new();
	}
}
