namespace FizzBuzz2.SchoolManagement;

public class Classroom
{
    public required string Name { get; set; }
    public FormGroup? FormGroup { get; set; }
    public HashSet<Subject> AssociatedSubjects { get; } = [];
}
