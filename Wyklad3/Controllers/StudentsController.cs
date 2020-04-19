using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wyklad4.Services;

namespace Wyklad4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        string conString = "Data Source=db-mssql;Initial Catalog=s17571;Integrated Security=True";
        
        IStudentsDal _dbService;

        public StudentsController(IStudentsDal dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents([FromServices] IStudentsDal dbService)
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester FROM Student s " +
                    "JOIN Enrollment e ON e.IdEnrollment = s.IdEnrollment JOIN Studies st ON st.IdStudy = e.IdStudy";
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new StudentInfoDto()
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Name = dr["Name"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        Semester = dr["Semester"].ToString()
                    };
                    
                    list.Add(st);
                }
            }
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @indexNo";
                com.Parameters.AddWithValue("indexNo", indexNumber);
                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var st = new Student()
                    {
                        IndexNumber = dr["IndexNumber"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString()
                    };
                    return Ok(st);
                }
            }
            return NotFound();
        }

        [HttpGet("ex2")]
        public IActionResult GetStudents2()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Test";
                com.Parameters.AddWithValue("LastName", "Nowak");
                com.CommandType = System.Data.CommandType.StoredProcedure;
                var dr = com.ExecuteReader();
            }
            return NotFound();
        }
    }
}