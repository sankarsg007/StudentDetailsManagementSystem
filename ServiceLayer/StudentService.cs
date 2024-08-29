using StudentModel;
using StudentRepository;

namespace ServiceLayer
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentRepository;

        public StudentService(IStudentRepo studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public List<Students> GetStudentList()
        {
            
            return _studentRepository.GetStudentList();

        }

        public void InsertARecord(Students students) 
        {
            _studentRepository.InsertStudents(students);

        }

        public void DeleteARecord(int rollno) 
        {
            _studentRepository.DeleteRecord(rollno);
        }

        public void UpdateARecord(Students students)
        {
            _studentRepository.UpdateRecord(students);
        }

        public List<Students> FetchARecordByRollno(int rollno) 
        {
            return _studentRepository.FetchByRollno(rollno);
        }

        public decimal? FetchAResult(int rollno) 
        {
            return _studentRepository.FetchResultByRollno(rollno);
        }
    }
}
