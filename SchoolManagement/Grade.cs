namespace FizzBuzz2.SchoolManagement;

public class Grade
{
    public required Student Student { get; init; }
    public required Subject Subject { get; init; }
    public required Teacher AssigningTeacher { get; set; }
    public int? Score { get; set; }
}
