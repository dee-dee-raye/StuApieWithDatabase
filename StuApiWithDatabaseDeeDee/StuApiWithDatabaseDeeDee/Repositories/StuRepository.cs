using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Dapper;
using StuApiWithDatabaseDeeDee.Models;
using System.Collections.Generic;
using System.Linq;

namespace StuApiWithDatabaseDeeDee.Repositories
{
    public class StuRepository : IStuRepository
    {
        const string connectionString = "Server=localhost; Database=StudentDB; UID=deedee; Password=1@Abcdefg";

        public Student GetStudent(string id)
        {
            string sql = "SELECT * FROM Student WHERE StudentId = @StudentID;";
            Student stu;

            using (var connection = new MySqlConnection(connectionString))
            {
                stu = connection.QuerySingleOrDefault<Student>(sql, new { StudentID = id });
            }

            return stu;
        }

        public List<Student> GetAllStudents()
        {
            string sql = "SELECT * FROM Student;";
            List<Student> stuList;

            using (var connection = new MySqlConnection(connectionString))
            {
                stuList = connection.Query<Student>(sql).ToList();
            }

            return stuList;
        }

        public void CreateStudent(Student stu)
        {
            string sql = "INSERT INTO Student (StudentName, StudentId, StudentGpa) Values (@StudentName, @StudentId, @StudentGpa);";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, new {StudentName = stu.StudentName, StudentID = stu.StudentId, StudentGpa = stu.StudentGpa } );
            }
        }

        public void UpdateStudent(Student updatedStudent)
        {
            string sql = "UPDATE Student SET StudentName = @StudentName, StudentId = @StudentId, StudentGpa = @StudentGpa WHERE StudentId = @StudentId;";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, new { StudentName = updatedStudent.StudentName, StudentID = updatedStudent.StudentId, StudentGpa = updatedStudent.StudentGpa });
            }
        }

        public void DeleteStudent(Student deleteStudent)
        {
            string sql = "DELETE FROM Student WHERE StudentId = @StudentID";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, new { StudentID = deleteStudent.StudentId });
            }
        }

        public float[] GetRange()
        {
            string sql = "SELECT min(StudentGpa) as min, max(StudentGpa) as max from Student;";
            float[] range = new float[2];

            using (var connection = new MySqlConnection(connectionString))
            {
                var result = connection.Query(sql).Single();
                range[0] = result.min;
                range[1] = result.max; ;
            }

            return range;
        }
    }
}
