using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControlSteps
{
    public enum Branch
    {
        None,
        [Description("Computer Science")]
        ComputerScience,

        [Description("Mechanical Engineering")]
        MechanicalEngineering,

        [Description("Electronics and Communication")]
        ElectronicsCommunication,

        [Description("Civil Engineering")]
        CivilEngineering
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class Student
    {
        public string Name { get; set; }
        public Branch StudentBranch { get; set; }
        public Gender StudentGender { get; set; }

        public override string ToString()
        {
            return $"{Name} {StudentBranch} {StudentGender}";
        }

        public Student(string name, Branch branch, Gender gender)
        {
            Name = name ;
            StudentBranch = branch ;
            StudentGender = gender ;
        }

        public Student()
        {
            
        }

        //public List<Student> GetStudentList()
        //{
        //    return new List<Student>()
        //    {
        //        new Student(){Name="Subin", StudentBranch = Branch.Computer_Science, Gender=Gender.Male,},
        //        new Student(){Name="Umer", StudentBranch = Branch.Computer_Science, Gender=Gender.Male,},
        //        new Student(){Name="Kashyap", StudentBranch = Branch.Computer_Science, Gender=Gender.Male,},
        //        new Student(){Name="Rohit", StudentBranch = Branch.Electronics, Gender=Gender.Male,},
        //        new Student(){Name="David", StudentBranch = Branch.Electronics, Gender=Gender.Male,},
        //    };
        //}
    }
}
