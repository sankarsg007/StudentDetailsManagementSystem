using StudentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepository
{
    public interface IStudentRepo
    {
        List<Students> GetStudentList();

        void InsertStudents(Students students);

        void DeleteRecord(int rollno);

        void UpdateRecord(Students students);

        List<Students> FetchByRollno(int rollno);

        decimal? FetchResultByRollno(int rollno);
    }
}
