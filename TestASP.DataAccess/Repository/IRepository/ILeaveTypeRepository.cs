using TestASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.DataAccess.Repository.IRepository
{
    public interface ILeaveTypeRepository : IRepository<LeaveType>
    {
        void Update(LeaveType obj);
    }
}
