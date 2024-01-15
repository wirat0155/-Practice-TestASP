using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.Models.ViewModels
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; } = new Employee();
        [ValidateNever]
        public IEnumerable<SelectListItem>? Department_List { get; set; }
    }
}
