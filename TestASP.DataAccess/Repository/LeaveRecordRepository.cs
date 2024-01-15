using TestASP.DataAccess.Data;
using TestASP.DataAccess.Repository.IRepository;
using TestASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestASP.DataAccess.Repository
{
    public class LeaveRecordRepository : Repository<LeaveRecord>, IRepository.ILeaveRecordRepository
    {
        private ApplicationDBContext _dbContext;
        public LeaveRecordRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LeaveRecord> Filter(string? employeeCode, string? departmentName, DateTime? from, DateTime? to)
        {
            var query = _dbContext.tbl_leave_record
                .FromSqlRaw($"SELECT [tbl_leave_record].[Id],[EmployeeId],[LeaveTypeId],[From] ,[To] " +
                             $"FROM [TestASP].[dbo].[tbl_leave_record] " +
                             $"LEFT JOIN [TestASP].[dbo].[tbl_employee] ON [tbl_leave_record].[EmployeeId] = [tbl_employee].[Id] " +
                             $"LEFT JOIN [TestASP].[dbo].[tbl_department] ON [tbl_employee].[DepartmentId] = [tbl_department].[Id] ");

            if (!string.IsNullOrWhiteSpace(employeeCode))
            {
                query = query.Where(record => record.Employee.EmployeeCode == employeeCode);
            }
            if (!string.IsNullOrWhiteSpace(departmentName))
            {
                query = query.Where(record => record.Employee.Department.Name == departmentName);
            }
            if (from != null) 
            {
                query = query.Where(record => record.From >= from);
            }
            if (to != null)
            {
                query = query.Where(record => record.To <= to);
            }

            return query.ToList();
        }


        public void Update(LeaveRecord obj)
        {
            _dbContext.tbl_leave_record.Update(obj);
        }
    }
}
