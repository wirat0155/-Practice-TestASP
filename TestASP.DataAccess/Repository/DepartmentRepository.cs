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
    public class DepartmentRepository : Repository<Department>, IRepository.IDepartmentRepository
    {
        private ApplicationDBContext _dbContext;
        public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Department obj)
        {
            _dbContext.tbl_department.Update(obj);
        }
    }
}
