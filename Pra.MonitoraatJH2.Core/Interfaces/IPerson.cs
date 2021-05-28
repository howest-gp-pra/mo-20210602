using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Entities;
namespace Pra.MonitoraatJH2.Core.Interfaces
{
    public interface IPerson
    {
        string Name { get; set; }
        DateTime DateOfBirth { get; set; }
        bool? Gender { get; set; }
        int AgeInYears { get;  }
        string NativeLanguage { get; set; }
        string Phone { get; set; }
        List<string> Emails { get;  }
        List<Address> Addresses { get; }

        string ShowDetails();
    }
}
