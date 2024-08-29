using System.Data.SqlClient;
using Dapper;
using StudentModel;


namespace StudentRepository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly string _connectionString;

        public StudentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Students> GetStudentList()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Students;";
                return connection.Query<Students>(query).ToList();
            }
        }

        public void InsertStudents(Students students)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Students (StudName, Degree, DateOfBirth, Gender, City, GPAinPercent) " +
                                       "VALUES (@StudName, @Degree, @DateOfBirth, @Gender, @City, @GPAinPercent);";

                connection.Execute(query, students);
            }

        }

        public void DeleteRecord(int rollno)      //delete based on id
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Students WHERE Rollno = @Rollno;";
                int rowsAffected = connection.Execute(query, new { Rollno = rollno });

                if (rowsAffected > 0)
                {
                    Console.WriteLine("\n###########***Delete operation completed successfully. Rows affected: " + rowsAffected + "***###########\n");
                }
                else
                {
                    Console.WriteLine("\n************No rows were deleted.************\n");
                }

            }

        }

        public List<Students> FetchByRollno(int rollno)      //fetch based on rollno
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Students WHERE Rollno = @Rollno;";
                return connection.Query<Students>(query, new { Rollno = rollno }).ToList() ;

            }

        }

        public void UpdateRecord(Students students)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Students SET " +
                               "StudName = @StudName, " +
                               "Degree = @Degree, " +
                               "DateOfBirth = @DateOfBirth, " +
                               "Gender = @Gender, " +
                               "City = @City, " +
                               "GPAinPercent = @GPAinPercent " +
                               "WHERE Rollno = @Rollno;";
                connection.Execute(query, students);
            }

        }

        public decimal? FetchResultByRollno(int rollno) // Nullable decimal to handle null results
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT GPAinPercent FROM Students WHERE Rollno = @Rollno;";

                // Execute the query and retrieve the GPAinPercent as a decimal
                decimal? gpa = connection.QuerySingleOrDefault<decimal?>(query, new { Rollno = rollno });

                return gpa;
            }
        }



    }
}
