using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.Models.ViewModels
{
    public class LeaveVM
    {
        public LeaveRecord LeaveRecord { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? Leave_Type_List { get; set; }

    }
}
