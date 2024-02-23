using FizzBuzz2.SchoolManagement;

var school = new School
{
    Name = "London Park School",
};
school.RegisterFormGroup("7T");
school.RegisterStudent("Emily", ["Elaine", "Peter"], 2015);
Console.WriteLine(
    string.Join(", ",
    school.Students.Select(student =>
        $"Name: {student.Name}, form group: {student.FormGroup.Name}, parents: {string.Join(", ", student.Parents.Select(parent => parent.Name))}")));

// using FizzBuzz2.ShopManagement;

// var shop = new Shop();

// shop.AddProduct("broccoli", 0.6m, "Sheepy Farms");
// shop.AddProduct("strawberry and banana smoothie", 3m, "Innocent Smoothies");
// shop.AddProduct("olive oil", 3m, "Filippo Berio");

// var userInterface = new UserInterface
// {
//     Shop = shop,
// };

// userInterface.Run();
