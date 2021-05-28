using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Enums;
namespace Pra.MonitoraatJH2.Core.Interfaces
{
    interface IStaff
    {
        Diploma Diploma { get; set; }
        Function Function { get; set; }
    }
}
