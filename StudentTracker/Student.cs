using System.Text.Json.Serialization;

namespace StudentTracker
{
    public class Student
    {
        [JsonConstructor]
        public Student(string firstName,
                       string lastName,
                       Gender gender,
                       string course,
                       string className,
                       int age,
                       int rollNumber,
                       double marks)
        {
            FirstName = ValidateName(firstName);
            LastName = ValidateName(lastName);
            Gender = gender;
            Course = course;
            ClassName = className;
            Age = age;
            RollNumber = rollNumber;
            Marks = marks;
        }
        private int _age;
        private double _marks;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string ClassName { get; init; } = string.Empty;
        public int RollNumber { get; init; }
        public double Marks
        {
            get { return _marks; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("marks must be between 0 - 100");
                _marks = value; 
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 5 || value > 30)
                    throw new ArgumentOutOfRangeException("age must be between 5 - 30");
                _age = value;
            }
        }
        public string Course { get; set; } = string.Empty;
        public Gender Gender { get; set; } 

        private string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name cannot be empty");
            foreach(char c in name)
            {
                if (!(char.IsLetter(c) || char.IsWhiteSpace(c)))
                    throw new ArgumentException("invalid name input");
            }
            return name;
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
