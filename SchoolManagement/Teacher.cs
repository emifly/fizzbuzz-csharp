namespace FizzBuzz2.SchoolManagement;

public class Teacher : Person
{
    public FormGroup? FormGroup { get; set; }
    public HashSet<Subject> QualifiedSubjects { get; } = [];
}
