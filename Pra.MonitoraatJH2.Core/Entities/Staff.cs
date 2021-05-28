using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Enums;
using Pra.MonitoraatJH2.Core.Interfaces;
namespace Pra.MonitoraatJH2.Core.Entities
{
    public class Staff:Person, IStaff
    {
        public Diploma Diploma { get; set; }
        public Function Function { get; set; }

        public Staff(string name, DateTime dateOfBirth, string phone, Function function, Diploma diploma, bool? gender = null)
            : base(name, dateOfBirth, phone, gender)
        {
            Diploma = diploma;
            Function = function;
        }
        public Staff(string name, DateTime dateOfBirth, string phone, Function function, Diploma diploma, string nativeLanguage, bool? gender = null)
            : base(name, dateOfBirth, phone, nativeLanguage, gender)
        {
            Diploma = diploma;
            Function = function;
        }
        public override string ShowDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("STAFF");
            sb.Append(base.ShowDetails());
            sb.AppendLine($"Function : {Function.ToString()}");
            sb.AppendLine($"Diploma : {Diploma.ToString()}");
            return sb.ToString();
        }
    }
}
