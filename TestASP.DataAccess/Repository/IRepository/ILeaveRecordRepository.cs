using TestASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.DataAccess.Repository.IRepository
{
    public interface ILeaveRecordRepository : IRepository<LeaveRecord>
    {
        void Update(LeaveRecord obj);
        List<LeaveRecord> Filter(string? employeeCode, string? departmentName, DateTime? from, DateTime? to);
    }
}
