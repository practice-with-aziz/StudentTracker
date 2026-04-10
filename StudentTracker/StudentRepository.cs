using Serilog;
using System.Text.Json;

namespace StudentTracker
{
    public class StudentRepository
    {
        private readonly List<Student> _students;
        private readonly HashSet<string> _courses;
        private readonly HashSet<string> _className;
        private readonly string _filePath;
        public StudentRepository()
        {
            string appFolder = AppContext.BaseDirectory;
            _filePath = Path.Combine(appFolder, "students.json");

            _students = LoadStudentsFromFile();

            _courses = new HashSet<string>() { "BSc IT", "CS", "BSc", "BCom", "BA" };
            _className = new HashSet<string>() { "1st year", "2nd year", "3rd year" };
        }

        private List<Student> LoadStudentsFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Log.Warning($"file not found at {_filePath}");
                return new List<Student>();
            }
            try
            {
                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    Log.Warning($"student file is empty {_filePath}");
                    return new List<Student>();
                }

                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            catch (JsonException ex)
            {
                Log.Error(ex, $"student file is corrupted at {_filePath}. could not be serialized");
                Console.WriteLine("Warning! student data is corrupted, starting wiht empty list");
                return new List<Student>();
            }
            catch(IOException ex)
            {
                Log.Error(ex, $"could not read the student file at {_filePath}");
                Console.WriteLine($"Warning! could not read student file, starting with empty list");
                return new List<Student>();
            }
        }
        private void SaveStudentsToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_students, options);

                File.WriteAllText(_filePath, json);
            }
            catch (IOException ex)
            {
                Log.Warning(ex, $"I/O error, disk is full/locked");
                Console.WriteLine($"Warning! cannot save data to file, file is full/locked");
            }
            
        }
        private bool IsRollNumberExists(int rollno)
        {
            return _students.Any(s => s.RollNumber == rollno);
        }
        public bool AddStudent(Student student)
        {
            if (!IsRollNumberExists(student.RollNumber)
                && _className.Contains(student.ClassName)
                && _courses.Contains(student.Course))
            {
                _students.Add(student);
                SaveStudentsToFile();

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
            return _students.FirstOrDefault(s => s.RollNumber == rollno);
        }
        public Student? UpdateCourse(int rollno,string course)
        {
            var student = _students.FirstOrDefault(s => s.RollNumber == rollno);
            if (student != null)
            {
                if (_courses.Contains(course))
                {
                    student.Course = course;
                    SaveStudentsToFile();
                    return student;
                }
                return null;
            }
            return null;
        }
        public bool DeleteStudent(int rollno)
        {
            var student = _students.FirstOrDefault(s => s.RollNumber == rollno);
            if (student != null)
            {
                _students.Remove(student);
                SaveStudentsToFile();
                return true;
            }
            return false;
        }
    }
}
