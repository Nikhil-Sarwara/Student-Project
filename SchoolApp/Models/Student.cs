using System;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
