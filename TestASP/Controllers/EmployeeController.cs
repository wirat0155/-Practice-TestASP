using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestASP.DataAccess.Repository.IRepository;
using TestASP.Models;
using TestASP.Models.ViewModels;
using TestASP.Utility;

namespace TestASP.Controllers
{
    [Authorize(Roles = SD.Role_HR)]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Employee> Employee_List = _unitOfWork.EmployeeRepository.GetAll(includeProperties: "Department").ToList();
            return View(Employee_List);
        }

        public IActionResult Insert()
        {
            EmployeeVM add = new EmployeeVM();
            add.Department_List = _unitOfWork.DepartmentRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(add);
        }

        [HttpPost]
        public IActionResult Insert(EmployeeVM add)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(add.Employee);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            add.Department_List = _unitOfWork.DepartmentRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(add);
        }

        public IActionResult Update(Guid Id)
        {
            EmployeeVM edit = new EmployeeVM();
            edit.Employee = _unitOfWork.EmployeeRepository.Get(u => u.Id == Id);
            if(edit.Employee == null)
            {
                return NotFound();
            }
            edit.Department_List = _unitOfWork.DepartmentRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(edit);
        }

        [HttpPost]
        public IActionResult Update(EmployeeVM edit)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Update(edit.Employee);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            edit.Department_List = _unitOfWork.DepartmentRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(edit);
        }

        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            Employee employee = _unitOfWork.EmployeeRepository.Get(U => U.Id == Id);
            if (employee == null)
            {
                return NotFound();
            }
            _unitOfWork.EmployeeRepository.Remove(employee);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
