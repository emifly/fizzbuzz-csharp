namespace FizzBuzz2.SchoolManagement;

public class FormGroup
{
    public required string Name { get; set; }
    public Teacher? Teacher { get; set; }
    public Classroom? Classroom { get; set; }
    public HashSet<Student> Students { get; } = [];
}
