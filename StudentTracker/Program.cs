using Serilog;
using StudentTracker;

// serilog setup
Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                 .CreateLogger();


Console.WriteLine("Welcome to Student tracker app");
Console.WriteLine("here you can do");

StudentRepository studentRepo = new();
StudentUtility utility = new();

while (true)
{
    Console.WriteLine("1.Add Student");
    Console.WriteLine("2.View all students");
    Console.WriteLine("3.Search student by roll number");
    Console.WriteLine("4.Update student's course");
    Console.WriteLine("5.Remove student from the the database");
    Console.WriteLine("6.Exit the program");
    Console.WriteLine("Enter the appropriate number for the corresponding operation");

    int userInput = StudentUtility.UserInput(1, 6);

    if (userInput == 1)
    {
        string firstName = utility.GetStudentName("Enter your First Name");
        string lastName = utility.GetStudentName("Enter your Last Name");
        int age = utility.GetStudentAge("Enter your age");
        Gender gender = utility.GetStudentGender("Enter your Gender");
        string className = utility.GetStudentClass("Enter your Class");
        int rollNubmer = utility.GetStudentRollNumber("Enter your Roll No.");
        double marks = utility.GetStudentMarks("Enter your Marks");
        string course = utility.GetStudentCourse("Enter your course");

        Student student = new Student(firstName, lastName, gender,
                                      course, className, age,
                                      rollNubmer, marks);

        var IsAdded = studentRepo.AddStudent(student);
        if (!IsAdded)
        {
            Console.WriteLine("something went wrong, student could not be added");
            continue;
        }
        Console.WriteLine("Student added successfully");
    }
    else if (userInput == 2)
    {
        var studentList = studentRepo.GetAllStudents();
        if (studentList != null && studentList.Any())
        {
            utility.PrintStudent(studentList);
        }
        else
        {
            Console.WriteLine("there are no students in the database\n");
        }
    }
    else if (userInput == 3)
    {
        int rollNo = utility.GetStudentRollNumber("Enter your roll no.");
        var searchStudent = studentRepo.SearchByRollNo(rollNo);
        if (searchStudent != null)
        {
            utility.PrintStudent(searchStudent);
        }
        else
        {
            Console.WriteLine("Student does not exists");
        }
    }
    else if (userInput == 4)
    {
        int rollNo = utility.GetStudentRollNumber("Enter your roll no.");
        

        string course = utility.GetStudentCourse("Enter your Course name");
        var updateStudentCourse = studentRepo.UpdateCourse(rollNo, course);

        if (updateStudentCourse != null)
        {
            utility.PrintStudent(updateStudentCourse);
        }
        else
        {
            Console.WriteLine("Student does not exists, course cannot be updated");
        }
    }
    else if (userInput == 5)
    {
        int rollNo = utility.GetStudentRollNumber("Enter your roll no.");
        var student = studentRepo.SearchByRollNo(rollNo);

        if(student != null)
        {
            if (studentRepo.DeleteStudent(rollNo))
            {
                Console.WriteLine("Student removed successfully");
            }
        }
        else
        {
            Console.WriteLine("student does not exists, cannot remove");
        }
    }
    else if(userInput == 6)
    {
        Console.WriteLine("see you again");
        break;
    }

}
Log.CloseAndFlush();