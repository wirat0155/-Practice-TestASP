using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TestASP.DataAccess.Repository.IRepository;
using TestASP.Models;
using TestASP.Models.ViewModels;
using TestASP.Utility;

namespace TestASP.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LeaveController(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (User.IsInRole(SD.Role_HR))
            {
                return RedirectToAction("GetAllLeave");
            }
            else
            {
                return RedirectToAction("GetMyLeave");
            }
        }

        [Authorize(Roles = SD.Role_HR)]
        public IActionResult GetAllLeave(GetAllLeaveVM? vm)
        {
            vm.LeaveList = _unitOfWork.LeaveRecordRepository.Filter(vm.EmployeeCode, vm.DepartmentName, vm.From, vm.To);

            foreach (var leave in vm.LeaveList)
            {
                leave.Employee = _unitOfWork.EmployeeRepository.Get(u => u.Id == leave.EmployeeId);
            }

            foreach (var leave in vm.LeaveList)
            {
                leave.Employee.Department = _unitOfWork.DepartmentRepository.Get(u => u.Id == leave.Employee.DepartmentId);
            }
            foreach (var leave in vm.LeaveList)
            {
                leave.LeaveType = _unitOfWork.LeaveTypeRepository.Get(u => u.Id == leave.LeaveTypeId);
            }
            return View(vm);
        }

        public IActionResult GetMyLeave()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            Employee employee = _unitOfWork.EmployeeRepository.Get(u => u.UserId == userId);

            List<LeaveRecord> leaveList = _unitOfWork.LeaveRecordRepository.GetAll(u => u.EmployeeId == employee.Id, includeProperties: "LeaveType").ToList();
            return View(leaveList);
        }

        public IActionResult AddLeave()
        {
            LeaveVM add = new LeaveVM();
            add.Leave_Type_List = _unitOfWork.LeaveTypeRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(add);
        }

        [HttpPost]
        public IActionResult AddLeave(LeaveVM add)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                string userId = claim.Value;

                Employee employee = _unitOfWork.EmployeeRepository.Get(u => u.UserId == userId);
                add.LeaveRecord.EmployeeId = employee.Id;
                _unitOfWork.LeaveRecordRepository.Add(add.LeaveRecord);
                _unitOfWork.Save();
                return RedirectToAction("GetMyLeave");
            }
            add.Leave_Type_List = _unitOfWork.LeaveTypeRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(add);
        }
    }
}
