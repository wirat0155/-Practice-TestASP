using TestASP.DataAccess.Data;
using TestASP.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _dbContext;
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public ILeaveTypeRepository LeaveTypeRepository { get; private set; }
        public ILeaveRecordRepository LeaveRecordRepository { get; private set; }

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
            LeaveTypeRepository = new LeaveTypeRepository(_dbContext);
            LeaveRecordRepository = new LeaveRecordRepository(_dbContext);
        }
        
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
