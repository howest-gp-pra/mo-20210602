using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Enums;
namespace Pra.MonitoraatJH2.Core.Entities
{
    public class Student : Person
    {
        public Diploma Diploma { get; set; }

        public Student(string name, DateTime dateOfBirth, string phone, Diploma diploma, bool? gender = null)
            :base(name, dateOfBirth, phone, gender)
        {
            Diploma = diploma;
        }
        public Student(string name, DateTime dateOfBirth, string phone, Diploma diploma, string nativeLanguage, bool? gender = null)
            : base(name, dateOfBirth, phone,nativeLanguage, gender)
        {
            Diploma = diploma;
        }

        public override string ShowDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("STUDENT");
            sb.Append(base.ShowDetails());
            sb.AppendLine($"Diploma : {Diploma.ToString()}");
            return sb.ToString();
        }
    }
}
