using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ILeaveTypeRepository LeaveTypeRepository { get; }
        ILeaveRecordRepository LeaveRecordRepository { get; }
        void Save();
    }
}
