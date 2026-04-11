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
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public int Age
        {
            get { return _age; }
            private set
            {
                if (value < 5 || value > 30)
                    throw new ArgumentOutOfRangeException("age must be between 5 - 30");
                _age = value;
            }
        }
        public double Marks
        {
            get { return _marks; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("marks must be between 0 - 100");
                _marks = value;
            }
        }
        public int RollNumber { get; init; }
        public string Course { get; private set; } = string.Empty;
        public Gender Gender { get; private set; } 
        public string ClassName { get; private set; } = string.Empty;

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
        internal void ChangeFirstName(string newName) => FirstName = ValidateName(newName);
        internal void ChangeLastName(string newName) => LastName = ValidateName(newName);
        internal void ChangeAge(int newAge) => Age = newAge;
        internal void ChangeMarks(double newMarks) => Marks = newMarks;
        internal void ChangeCourse(string newCourse) => Course = newCourse;
        internal void ChangeGender(Gender newGender) => Gender = newGender;
        internal void ChangeClassName(string newClassName) => ClassName = newClassName;
        
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
