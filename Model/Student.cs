using ControllerExamples.CustomValidator;
using ControllerExamples.Helpers;

namespace ControllerExamples.Model;

public class Student
{
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender StudentGender { get; set; }
    [AmountValidation(50, "Fee",ErrorMessage = "Amount must be multiple of Rs. 50/-")]
    public int Fee { get; set; }
}



