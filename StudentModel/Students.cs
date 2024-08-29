namespace StudentModel
{
    public class Students
    {
        /// <summary>
        /// Student roll number, in database IDENTITY(1,1) constraint is used, so the is no need to specify it
        ///  A unique identifier for each student, typically used as the primary key in the database.
        /// </summary>
        public int Rollno { get; set; }

        /// <summary>
        /// The full name of the student. This property is required (NOT NULL).
        /// </summary>
        public string StudName { get; set; }

        /// <summary>
        /// The academic degree or program the student is pursuing. This property is required (NOT NULL). 
        /// ex:B.E CS, B.Tech IT, etc
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// The date of birth of the student, representing the student’s birthdate without a time component. 
        /// format -- new DateOnly(2000, 1, 1)
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Student's Gender - Male,Female,others,can also be NULL
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// City where the student resides, can be left NULL 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// he student's grade point average expressed as a percentage. This property is required (NOT NULL).
        /// ex: 87.33
        /// </summary>
        public decimal GPAinPercent { get; set; }

    }
}
