namespace FizzBuzz2.SchoolManagement;

public class Subject
{
    public required string Name { get; set; }
    public HashSet<Teacher> QualifiedTeachers { get; } = [];
}
