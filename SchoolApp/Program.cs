using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                // Add some data
                if (!context.Students.Any())
                {
                    var student = new Student
                    {
                        Name = "John Doe",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Enrollments = new[]
                        {
                            new Enrollment
                            {
                                Course = new Course
                                {
                                    Title = "Math 101",
                                    Credits = 3
                                }
                            }
                        }
                    };

                    context.Students.Add(student);
                    context.SaveChanges();
                }

                // Retrieve and display data
                var students = context.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .ToList();

                foreach (var s in students)
                {
                    Console.WriteLine($"Student: {s.Name}, DOB: {s.DateOfBirth.ToShortDateString()}");
                    foreach (var e in s.Enrollments)
                    {
                        Console.WriteLine($"  Enrolled in: {e.Course.Title}, Credits: {e.Course.Credits}");
                    }
                }
            }
        }
    }
}
