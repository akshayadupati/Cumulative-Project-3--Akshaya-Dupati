using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using CumulativeProject1_AkshayaDupati.Models;
using System.Diagnostics;

namespace CumulativeProject1_AkshayaDupati.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// The ListTeachers function returns list of teachers
        /// </summary>
        /// <param name="SearchKey">Search key that defines teacher name . Optional parameter.</param>
        /// <example>GET : /api/TeacherData/ListTeachers</example>
        /// <example>GET : /api/TeacherData/ListTeachers/Akshaya</example>
        /// <returns>
        /// A list of teachers with teacherid, teacherfname and teacherlname
        /// </returns>


        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
        {

            if (SearchKey != null)
            {
                Debug.WriteLine("Hey there! Search key is present! ");
            }

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "SELECT * FROM TEACHERS";

            if (SearchKey != null)
            {
                query = query + " where lower(teacherfname) = lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();
            }

            cmd.CommandText = query;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {

                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();

                Teachers.Add(NewTeacher);
            }

            Conn.Close();

            return Teachers;
        }


        /// <summary>
        /// The FindTeacher function returns data of that particular teacher id.
        /// </summary>
        /// <example>GET : api/TeacherData/FindTeacher/{3}</example>
        /// <returns>
        /// Returns teacherid, teacherfname and teacherlname of 3rd teacher in the database.
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{teacherId}")]

        public Teacher FindTeacher(int teacherId)
        {

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "select * from teachers where teacherid=@id";

            cmd.Parameters.AddWithValue("@id", teacherId);

            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Teacher selectedTeacher = new Teacher();

            while (ResultSet.Read())
            {
                selectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                selectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                selectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                selectedTeacher.TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);
            }

            Conn.Close();

            return selectedTeacher;
        }


        /// <summary>
        /// Adds new teacher into the database
        /// <paramref name="NewTeacher"/>Teacher info embedded in the new teacher object
        /// </summary>
        public void AddTeacher(Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, salary, hiredate) values (@teacherfname, @teacherlname, @teachersalary, CURRENT_DATE())";

            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@teachersalary", NewTeacher.TeacherSalary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }


        /// <summary>
        /// Delete a teacher based on the teacher id from the database
        /// </summary>
        /// <param name="TeacherId">primary key that identifies a particular teacher</param>
        public void DeleteTeacher(int TeacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "delete from teachers where teacherid = @id";

            cmd.Parameters.AddWithValue("@id", TeacherId);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        [Route("api/TeacherData/UpdateTeacher/{TeacherInfo}")]

        /// <summary>
        /// Updates teacher information in the teacher database
        /// </summary>
        /// <param name="TeacherId">Primary key of the teacher table</param>
        /// <param name="TeacherInfo">Teacher object that consists of Teacher's first name, last name and salary</param>
            
        public void UpdateTeacher(Teacher TeacherInfo)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, salary=@teachersalary where teacherid=@teacherid";

            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@teachersalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@teacherid", TeacherInfo.TeacherId);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
