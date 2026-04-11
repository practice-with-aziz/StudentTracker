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
    Console.WriteLine("4.Update student's details");
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
            Console.WriteLine("Invalid roll Number, student could not be added");
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
        Student student = studentRepo.SearchByRollNo(rollNo);

        if (student == null)
        {
            Console.WriteLine("student not found, cannot update student's details");
        }
        else
        {
            bool isUpdating = true;
            while (isUpdating)
            {
                Console.WriteLine("enter the exact number to update the corresponding detail");
                Console.WriteLine("1.First name");
                Console.WriteLine("2.Last name");
                Console.WriteLine("3.Age");
                Console.WriteLine("4.Marks");
                Console.WriteLine("5.Course");
                Console.WriteLine("6.Gender");
                Console.WriteLine("7.Class");
                Console.WriteLine("8.go to main menu");

                int choice = StudentUtility.UserInput(1, 8);

                bool isChangedSuccessful = false;
                switch (choice)
                {
                    case 1:
                        var firstName = utility.GetStudentName("Enter new first name");
                        student.ChangeFirstName(firstName);
                        isChangedSuccessful = true;
                        break;

                    case 2:
                        var lastName = utility.GetStudentName("Enter new last name");
                        student.ChangeLastName(lastName);
                        isChangedSuccessful = true;
                        break;

                    case 3:
                        var age = utility.GetStudentAge("Enter new age");
                        student.ChangeAge(age);
                        isChangedSuccessful = true;
                        break;

                    case 4:
                        var marks = utility.GetStudentMarks("Enter new marks");
                        student.ChangeMarks(marks);
                        isChangedSuccessful = true;
                        break;

                    case 5:
                        var newCourse = utility.GetStudentCourse("Enter new course");
                        student.ChangeCourse(newCourse);
                        isChangedSuccessful = true;
                        break;

                    case 6:
                        var gender = utility.GetStudentGender("Enter new gender");
                        student.ChangeGender(gender);
                        isChangedSuccessful = true;
                        break;

                    case 7:
                        var newClass = utility.GetStudentClass("Enter new class");
                        student.ChangeClassName(newClass);
                        isChangedSuccessful = true;
                        break;

                    case 8:
                        isUpdating = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                if (isChangedSuccessful)
                {
                    studentRepo.SaveChanges(student);
                    Console.WriteLine("Student's details updated successfully");
                    utility.PrintStudent(student);
                }
            }
        }
    }
    else if (userInput == 5)
    {
        int rollNo = utility.GetStudentRollNumber("Enter your roll no.");
        bool isDeleted = studentRepo.DeleteStudent(rollNo);

        if (isDeleted)
        {
            Console.WriteLine("Student removed successfully");
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