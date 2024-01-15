using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.Models.ViewModels
{
    public class GetAllLeaveVM
    {

        public string? EmployeeCode {  get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        [ValidateNever]
        public List<LeaveRecord> LeaveList { get; set; }

    }
}
