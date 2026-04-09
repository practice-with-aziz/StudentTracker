namespace StudentTracker
{
    public class StudentRepository
    {
        private readonly List<Student> _students;
        private readonly HashSet<string> _courses;
        private readonly HashSet<string> _className;
        public StudentRepository()
        {
            _students = new List<Student>();
            _courses = new HashSet<string>() { "BSc It", "CS", "BSc", "BCom", "BA" };
            _className = new HashSet<string>() { "1st year", "2nd year", "3rd year" };
        }
        private bool IsValidRollNo(int rollno)
        {
            return _students.Any(s => s.RollNumber == rollno);
        }
        public bool AddStudent(Student student)
        {
            if (IsValidRollNo(student.RollNumber))
            {
                _students.Add(student);
                return true;
            }
            return false;
        }
        
        public IReadOnlyList<Student> GetAllStudents()
        {
            if (_students.Any())
            {
                return _students.AsReadOnly();
            }
            return new List<Student>().AsReadOnly();
        }
        public Student? SearchByRollNo(int rollno)
        {
            if (_students.Any(s => s.RollNumber == rollno))
            {
                return _students.FirstOrDefault(s => s.RollNumber == rollno);
            }
            return null;
        }
        public Student? UpdateCourse(int rollno,string course)
        {

            if(_students.Any(s=>s.RollNumber == rollno))
            {
                var student = _students.FirstOrDefault(s => s.RollNumber == rollno);
                if (student != null)
                {
                    student.Course = course;
                    return student;
                }

            }
            return null;
        }
        public bool DeleteStudent(int rollno)
        {
            if(_students.Any(s=>s.RollNumber == rollno))
            {
                var removeStudent = _students.FirstOrDefault(s => s.RollNumber == rollno);
                if(removeStudent != null)
                {
                    _students.Remove(removeStudent);
                    return true;
                }
            }
            return false;
        }
    }
}
