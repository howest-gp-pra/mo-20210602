using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Entities;
using Pra.MonitoraatJH2.Core.Enums;

namespace Pra.MonitoraatJH2.Core.Interfaces
{
    interface IPersonService
    {
        List<Person> Persons { get; }
        List<Type> GetPersonTypes();
        List<Person> GetPersonsPerType(Type type);
    }
}
