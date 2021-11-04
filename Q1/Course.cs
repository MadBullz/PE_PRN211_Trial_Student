using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public delegate void UpdateNumHandler(int a, int b);

    class Course
    {
        public event UpdateNumHandler OnNumberOfStudentChange;

        public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public Dictionary<Student, double> Students;

        public Course(int courseID, string courseTitle)
        {
            CourseID = courseID;
            CourseTitle = courseTitle;
            Students = new Dictionary<Student, double>();
        }

        public void AddStudent(Student p, double g)
        {
            int oldNum = Students.Count;
            Students.Add(p, g);
            int newNum = Students.Count;
            if (OnNumberOfStudentChange != null)
            {
                OnNumberOfStudentChange(oldNum, newNum);
            }
        }

        public void RemoveStudent(int StudentID)
        {
            int oldNum = Students.Count;
            var key = (from s in Students.Keys
                       where s.StudentID == StudentID
                       select s).FirstOrDefault();
            Students.Remove(key);
            int newNum = Students.Count;
            if (OnNumberOfStudentChange != null)
            {
                OnNumberOfStudentChange(oldNum, newNum);
            }
        }

        public override string ToString()
        {
            string result = $"Course: {CourseID} - {CourseTitle} \n";
            foreach(var x in Students)
            {
                result += $"Student: {x.Key.StudentID} - {x.Key.StudentName} - {x.Value.ToString().Replace(".",",")} \n";
            }
            return result;
        }
    }
}
