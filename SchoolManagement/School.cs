namespace FizzBuzz2.SchoolManagement;

public class School
{
    public required string Name { get; set; }
    public HashSet<Student> Students { get; } = [];
    public HashSet<Teacher> Teachers { get; } = [];
    public HashSet<FormGroup> FormGroups { get; } = [];
    public HashSet<Subject> Subjects { get; } = [];
    public HashSet<Classroom> Classrooms { get; } = [];

    public FormGroup RegisterFormGroup(string formGroupName)
    {
        var newFormGroup = new FormGroup
        {
            Name = formGroupName,
        };
        FormGroups.Add(newFormGroup);
        return newFormGroup;
    }

    public Student RegisterStudent(
        string studentName,
        IEnumerable<string> parentNames,
        int projectedGraduationYear)
    {
        var newStudent = new Student
        {
            Name = studentName,
            GraduationYear = projectedGraduationYear,
            FormGroup = FormGroups.ElementAt(new Random().Next(FormGroups.Count)),
        };
        foreach (var parentName in parentNames)
        {
            var newStudentParent = new Parent
            {
                Name = parentName,
            };
            newStudentParent.Children.Add(newStudent);
            newStudent.Parents.Add(newStudentParent);
        }
        Students.Add(newStudent);
        return newStudent;
    }
}
