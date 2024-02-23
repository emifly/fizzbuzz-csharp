namespace FizzBuzz2.SchoolManagement;

public class Student : Person
{
    public required int GraduationYear { get; set; }
    public required FormGroup FormGroup { get; set; }
    public HashSet<Subject> CurrentSubjects { get; } = [];
    public HashSet<Grade> Grades { get; } = [];
    public HashSet<Parent> Parents { get; } = [];
}
