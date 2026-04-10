namespace StudentTracker
{
    public class StudentUtility
    {
        private readonly HashSet<string> _className;
        private readonly HashSet<string> _courses;
        public StudentUtility()
        {
            _className = new HashSet<string> { "1st year", "2nd year", "3rd year" };
            _courses = new HashSet<string> { "BSc IT", "CS", "BSc", "BCom", "BA" };
        }

        // input utility
        public static int UserInput(int min, int max)
        {
            while (true)
            {
                string userInput = (Console.ReadLine() ?? "");
                bool validInput = int.TryParse(userInput, out int result);

                if (validInput)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine($"invalid input given, please enter the number from {min} to {max}");
            }
        }
        public string GetStudentName(string prompt)
        {
            string name;
            while (true)
            {
                Console.WriteLine(prompt);
                name = (Console.ReadLine() ?? "").Trim();

                if(string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("name cannot be empty");
                    continue;
                }
                if (!IsNameValid(name))
                {
                    Console.WriteLine("invalid name, only letters, space, hyphens are allowed");
                    continue;
                }
                return name;
            }
        }
        public int GetStudentAge(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string inputAge = (Console.ReadLine() ?? "");
                bool validAge = int.TryParse(inputAge, out int age);

                if (!validAge || age < 5 || age > 30) 
                {
                    Console.WriteLine("invalid age entered, age must be between 5 to 30 years");
                    continue;
                }
                return age;
            }
        }
        public string GetStudentClass(string prompt)
        {
            string className;
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine(string.Join(", ", _className));

                className = (Console.ReadLine() ?? "").Trim();
                if (string.IsNullOrWhiteSpace(className))
                {
                    Console.WriteLine("class name cannot be empty");
                    continue;
                }
                if (!IsClassValid(className))
                {
                    Console.WriteLine("invalid class name entered, please enter the class from shown option ");
                    continue;
                }
                return className;
            }
        }
        public string GetStudentCourse(string prompt)   
        {
            string course;
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine(string.Join(", ",_courses));

                course = (Console.ReadLine() ?? "").Trim();
                if (string.IsNullOrWhiteSpace(course))
                {
                    Console.WriteLine("course cannot be empty");
                    continue;
                }
                if (!IsCourseValid(course))
                {
                    Console.WriteLine("invalid course entered, please enter the course from shown options");
                    continue;
                }
                return course;
            }
        }
        public int GetStudentRollNumber(string prompt)
        {
           
            while (true)
            {
                Console.WriteLine(prompt);
                string inputRollNo = (Console.ReadLine() ?? "");
                bool validInput = int.TryParse(inputRollNo, out int rollNo);

                if (!validInput)
                {
                    Console.WriteLine("invalid roll no. entered, please enter the roll no. as whole digit");
                    continue;
                }
                return rollNo;
            }
        }
        public double GetStudentMarks(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string inputMarks = (Console.ReadLine() ?? "");
                bool validMarks = double.TryParse(inputMarks, out double marks);

                if(!validMarks || marks<0 || marks > 100)
                {
                    Console.WriteLine("invalid markes entered, marks must be between 0 to 100");
                    continue;
                }
                return marks;
            }
        }
        public Gender GetStudentGender(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                foreach(Gender g in Enum.GetValues(typeof(Gender)))
                {
                    Console.WriteLine(g);
                }
                string inputGender = (Console.ReadLine() ?? "").ToLower();
                if (inputGender == "male")
                {
                    return Gender.Male;
                }
                else if (inputGender == "female")
                {
                    return Gender.Female;
                }
                else if (inputGender == "other")
                {
                    return Gender.Other;
                }
                else
                {
                    Console.WriteLine("invalid gender, please enter correct gender");
                    continue;
                }
            }
        }


        // private utility
        private bool IsNameValid(string name)
        {
            foreach(char c in name)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')
                    return false;
            }
            return true;
        }
        private bool IsClassValid(string className)
        {
            return _className.Contains(className);
        }
        private bool IsCourseValid(string course)
        {
            return _courses.Contains(course);
        }

        // utility
        public void PrintStudent(IReadOnlyList<Student> studentList)
        {
            Console.WriteLine($"{"Roll No",-10} {"FirstName",-15} {"LastName",-15} {"Age",-5} {"Marks",-7} {"Gender",-10} {"Course",-10} {"Class",-10} ");
            Console.WriteLine(new string('-', 85));
            foreach (Student s in studentList)
            {
                Console.WriteLine($"{s.RollNumber,-10} {s.FirstName,-15} {s.LastName,-15} {s.Age,-5} {s.Marks,-7} {s.Gender,-10} {s.Course,-10} {s.ClassName,-10}");
            }
        }
        public void PrintStudent(Student student)
        {
            Console.WriteLine($"{"Roll No",-10} {"FirstName",-15} {"LastName",-15} {"Age",-5} {"Marks",-7} {"Gender",-10} {"Course",-10} {"Class",-10} ");
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"{student.RollNumber,-10} {student.FirstName,-15} {student.LastName,-15} {student.Age,-5} {student.Marks,-7} {student.Gender,-10} {student.Course,-10} {student.ClassName,-10}");
        }
    }
}
