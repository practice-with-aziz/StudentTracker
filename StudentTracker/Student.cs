namespace StudentTracker
{
    public class Student
    {
        private int _age;
        private double _marks;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int RollNumber { get; set; }
        public double Marks
        {
            get { return _marks; }
            set
            {
                if (value < 0 && value > 100)
                    throw new ArgumentOutOfRangeException("marks must be between 0 - 100");
                _marks = value; 
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 5 && value > 30)
                    throw new ArgumentOutOfRangeException("age must be between 5 - 30");
                _age = value;
            }
        }
        public string Course { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}
