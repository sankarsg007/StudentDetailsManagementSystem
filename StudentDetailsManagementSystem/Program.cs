using ServiceLayer;
using StudentRepository;
using StudentModel;
using System.ComponentModel;


namespace StudentDetailsManagementSystem
{
    public class Program
    {

        static void Main(string[] args)
        {
            string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=interns;password=Wel#123@Team;TrustServerCertificate=True;";

            IStudentRepo studentRepository = new StudentRepo(connectionString);
            IStudentService studentService = new StudentService(studentRepository);


            Console.WriteLine("-------------------------Welcome-------------------------\n");
            Console.WriteLine("********************************************************");
            Console.WriteLine("********** Student Database Management System **********");
            Console.WriteLine("********************************************************\n");

            bool flag = true;

            do
            {
                //Menu
                Console.Write("1.Register a Student\n2.Display all records\n3.Delete a record\n4.Update a record\n5.Fetch by Roll number\n6.View Student Result\n7.Sort the Records\nEnter 0 to save and exit...\n\nChoice : ");


                int choice = -1;

                //Exception handling incase if user enter characters
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n***********You are only allowed to enter numbers*********** \n");
                }



                switch (choice)
                {
                    case 1:
                        {
                            //1.Insert record
                            InsertStudentRecord(studentService);
                            break;
                        }
                    case 2:
                        {
                            //2.display all records
                            DisplayStudentsList(studentService);
                            Console.WriteLine("\n");
                            break;
                        }
                    case 3:
                        {
                            //3.delete a record based on student roll number
                            DeleteStudentRecord(studentService);
                            break;
                        }
                    case 4:
                        {
                            //4.update a record based on student roll number
                            UpdateStudent(studentService);
                            break;
                        }
                    case 5:
                        {
                            //5.Fetch a record based on student roll number
                            FetchByRollno(studentService);
                            break;
                        }
                    case 6:
                        {
                            //6.Fetch result by roll number
                            FetchResultByRollno(studentService);
                            break;
                        }
                    case 7:
                        {
                            //7.sort the records
                            SortBy(studentService);
                            break;
                        }

                    case 0:
                        {
                            //exit case
                            flag = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("*******************Enter a valid choice********************\n");
                            break;
                        }
                }
            } while (flag);
        }



        /// <summary>
        /// this method Displays all the records of the Students
        /// </summary>
        /// <param name="studentService"></param>
        static void DisplayStudentsList(IStudentService studentService)
        {
            List<Students> students = studentService.GetStudentList();
            foreach (var student in students)
            {
                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                    $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n");
            }
        }

        /// <summary>
        /// This method is used to register a new student details
        /// </summary>
        /// <param name="studentService"></param>
        static void InsertStudentRecord(IStudentService studentService)
        {

            Students students = new Students();

            Console.Write("Enter Name: ");
            students.StudName = Console.ReadLine();

            Console.Write("Enter Degree: ");
            students.Degree = Console.ReadLine();


            int count1 = 0;
            bool localFlag = true;
            do
            {
                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    students.DateOfBirth = dateOfBirth;
                    localFlag = false;
                }
                else
                {
                    Console.WriteLine("\n***********Invalid date format.***********\n");
                    count1++;
                    if (count1 == 3) return;  //If an invalid entry is made three times, the registration process stops.
                }
            } while (localFlag);

            Console.Write("Enter Gender: ");
            students.Gender = Console.ReadLine();

            Console.Write("Enter City: ");
            students.City = Console.ReadLine();


            bool flag = true;
            int count2 = 0;
            do
            {
                Console.Write("Enter GPA in Percent: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal gpaInPercent) && (gpaInPercent > 0 && gpaInPercent <= 100))
                {
                    students.GPAinPercent = gpaInPercent;
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\n***********Invalid GPA format.***********\n");
                    count2++;
                    if (count2 == 3) return; //If an invalid entry is made three times, the registration process stops. 
                }

            } while (flag);

            studentService.InsertARecord(students);
            Console.WriteLine("\n###########***Student registered successfully.***###########\n");

        }

        /// <summary>
        /// This method will delete a student's record based on roll number
        /// </summary>
        /// <param name="studentService"></param>
        static void DeleteStudentRecord(IStudentService studentService)
        {
            Console.Write("Enter Roll Number of the student to delete: ");
            if (int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                studentService.DeleteARecord(rollNumber);

            }
            else
            {
                Console.WriteLine("\n***********Invalid roll number.***********\n");
            }

        }
        /// <summary>
        /// This method will Display a student's record based on roll number
        /// </summary>
        /// <param name="studentService"></param>
        static void FetchByRollno(IStudentService studentService)
        {
            Console.Write("Enter Roll Number of the student : ");
            if (int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                List<Students> studentsList = studentService.FetchARecordByRollno(rollNumber);
                if (studentsList.Count > 0)
                {
                    foreach (var student in studentsList)
                    {
                        Console.WriteLine($"Rollno    : {student.Rollno}\nNmae      : {student.StudName}\nDegree    : {student.Degree}\n" +
                            $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n***********Record doesn't exist***********\n");
                }
            }
            else
            {
                Console.WriteLine("\n***********Invalid roll number.***********\n");
            }
        }

        /// <summary>
        /// This method will update a student's record based on roll number
        /// </summary>
        /// <param name="studentService"></param>
        static void UpdateStudent(IStudentService studentService)
        {
            while (true)
            {
                Console.Write("Enter Roll Number of the student to update: ");
                if (int.TryParse(Console.ReadLine(), out int rollNumber))
                {
                    //to print the old details
                    Console.WriteLine("\nExisting Details : \n");
                    List<Students> studentsList = studentService.FetchARecordByRollno(rollNumber);
                    if (studentsList.Count > 0)
                    {
                        foreach (var students in studentsList)
                        {
                            Console.WriteLine($"Rollno    : {students.Rollno}\nName      : {students.StudName}\nDegree    : {students.Degree}\n" +
                                $"DOB       : {DateOnly.FromDateTime(students.DateOfBirth)}\nGender    : {students.Gender}\nCity      : {students.City}\nGPA(in %) : {students.GPAinPercent}\n");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\n***********Record doesn't exist***********\n");
                    }

                    //update new details
                    var student = studentService.FetchARecordByRollno(rollNumber);
                    if (student != null)
                    {
                        // Prompt for each field and update if input is provided
                        Console.Write("Enter New Name (leave empty to keep current): ");
                        var newName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newName)) student[0].StudName = newName;

                        Console.Write("Enter New Degree (leave empty to keep current): ");
                        var newDegree = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newDegree)) student[0].Degree = newDegree;

                        Console.Write("Enter New Date of Birth (yyyy-mm-dd) (leave empty to keep current): ");
                        var newDateOfBirth = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newDateOfBirth))
                        {
                            if (DateTime.TryParse(newDateOfBirth, out DateTime dob))
                            {
                                student[0].DateOfBirth = dob;
                            }
                            else
                            {
                                Console.WriteLine("\n***********Invalid date format. Keeping the current date of birth.***********\n");
                            }
                        }

                        Console.Write("Enter New Gender (leave empty to keep current): ");
                        var newGender = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newGender)) student[0].Gender = newGender;

                        Console.Write("Enter New City (leave empty to keep current): ");
                        var newCity = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCity)) student[0].City = newCity;

                        Console.Write("Enter New GPA in Percent (leave empty to keep current): ");
                        var newGPAinPercent = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newGPAinPercent))
                        {
                            if (decimal.TryParse(newGPAinPercent, out decimal gpa))
                            {
                                student[0].GPAinPercent = gpa;
                            }
                            else
                            {
                                Console.WriteLine("\n***********Invalid GPA format. Keeping the current GPA.***********\n");
                            }
                        }

                        studentService.UpdateARecord(student[0]);
                        Console.WriteLine("\n##########***Student updated successfully.***###########\n");

                    }
                    else
                    {
                        Console.WriteLine("\n***********Student not found.***********\n");
                    }
                }
                else
                {
                    Console.WriteLine("***********Invalid roll number.***********\n");
                }

                Console.Write("\nDo you want to update another student? (Y/N): ");
                var continueChoice = Console.ReadLine();
                if (continueChoice == null || !continueChoice.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n");
                    break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// This method will fetch the total marks of the student and displays wheather he/she is pass or fail
        /// </summary>
        /// <param name="studentService"></param>
        static void FetchResultByRollno(IStudentService studentService)
        {
            Console.Write("Enter Roll Number of the student: ");
            if (int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                List<Students> students = studentService.GetStudentList();
                Students student = students.FirstOrDefault(s => s.Rollno == rollNumber);

                if (student != null)
                {
                    decimal? result = studentService.FetchAResult(rollNumber);
                    Console.Write($"\nStudent name : {student.StudName}\nDegree : {student.Degree}");
                    Console.Write($"\nMark Scored (out of 500) : {result * 5}\n");

                    if (result >= 45)
                    {
                        Console.WriteLine("***********Pass***********\n");
                    }
                    else
                    {
                        Console.WriteLine("***********Fail***********\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n***********Roll number not found.***********\n");
                }
            }
            else
            {
                Console.WriteLine("\n***********Invalid roll number.***********\n");
            }
        }


        /// <summary>
        /// This method will sort and display the students records based on given attributes
        /// </summary>
        /// <param name="studentService"></param>
        static void SortBy(IStudentService studentService)
        {
            List<Students> students = studentService.GetStudentList();

            bool flag = true;

            do
            {
                Console.Write("\n1.Sort By Name\n2.Sort By City\n3.Sort By GPA\n4.Highest marks\n5.Sort By Name Descending\n6.Sort By Date\n7.Sort By Date Descending\nEnter 0 to exit...\n\nEnter your Choice : ");
                int choice = -1;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n***********You are only allowed to enter numbers*********** \n");
                }
                switch (choice)
                {
                    case 0:
                        {
                            flag = false;
                            break;
                        }
                    case 1:
                        {
                            var a = students.OrderBy(x => x.StudName).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 2:
                        {
                            var a = students.OrderBy(x => x.City).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 3:
                        {
                            var a = students.OrderBy(x => x.GPAinPercent).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 4:
                        {
                            var a = students.OrderByDescending(x => x.GPAinPercent).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 5:
                        {
                            var a = students.OrderByDescending(x => x.StudName).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 6:
                        {
                            var a = students.OrderBy(x => x.DateOfBirth).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    case 7:
                        {
                            var a = students.OrderByDescending(x => x.DateOfBirth).ToList();
                            foreach (var student in a)
                            {
                                Console.WriteLine($"Rollno    : {student.Rollno}\nName      : {student.StudName}\nDegree    : {student.Degree}\n" +
                                                                $"DOB       : {DateOnly.FromDateTime(student.DateOfBirth)}\nGender    : {student.Gender}\nCity      : {student.City}\nGPA(in %) : {student.GPAinPercent}\n\n");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\n*******************Enter a valid choice********************\n");
                            break;
                        }

                }

            } while (flag);


        }

    }
}
