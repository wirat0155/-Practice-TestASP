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
    public class EmployeeRepository : Repository<Employee>, IRepository.IEmployeeRepository
    {
        private ApplicationDBContext _dbContext;
        public EmployeeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Employee obj)
        {
            _dbContext.tbl_employee.Update(obj);
        }
    }
}
