using StudentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IStudentService
    {
        List<Students> GetStudentList();

        void InsertARecord(Students students);

        void DeleteARecord(int rollno);

        void UpdateARecord(Students students);

        List<Students> FetchARecordByRollno(int rollno);

        decimal? FetchAResult(int rollno);
    }
}
