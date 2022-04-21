 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CumulativeProject1_AkshayaDupati.Models
{
    /// <summary>
    /// Blueprint of the Teacher datatype
    /// </summary>
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public decimal TeacherSalary{ get; set; }

    }
}