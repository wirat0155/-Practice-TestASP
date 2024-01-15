using TestASP.DataAccess.Data;
using TestASP.DataAccess.Repository.IRepository;
using TestASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestASP.DataAccess.Repository
{
    public class LeaveTypeRepository : Repository<LeaveType>, IRepository.ILeaveTypeRepository
    {
        private ApplicationDBContext _dbContext;
        public LeaveTypeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(LeaveType obj)
        {
            _dbContext.tbl_leave_type.Update(obj);
        }
    }
}
