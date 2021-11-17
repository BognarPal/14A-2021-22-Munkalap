using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using munkalap.service;
using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace munkalap.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly EmployeeRepository employeeRepository;

        public EmployeeController(ApplicationDbContext dbContext)
        {
#if DEBUG
            dbContext.Database.EnsureCreated();
#endif
            employeeRepository = new EmployeeRepository(dbContext);
        }

        [HttpGet]
        public IEnumerable<Employee> Employees([FromQuery]bool? withDeleted)
        {
            //return employeeRepository.GetAll(withDeleted == null ? false : (bool)withDeleted);
            return employeeRepository.GetAll(withDeleted ?? false);
        }

        [HttpGet("{id}")]   // pl.: /api/employee/2  -> 2-est behelyettesíti
        public ActionResult GetEmployee(int id)
        {
            return this.Run(() =>
            {
                return Ok(employeeRepository.GetById(id));
            });          
        }     

        [HttpPut]
        public ActionResult Create(Employee employee)
        {
            try
            {
                return Ok(employeeRepository.Create(employee));
            }
            catch
            {
                return BadRequest(new
                {
                    ErrorMessage = "Váratlan hiba"
                });
            }
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            try
            {
                return Ok(employeeRepository.Update(employee));
            }
            catch
            {
                return BadRequest(new
                {
                    ErrorMessage = "Váratlan hiba"
                });
            }
        }

        [HttpDelete]
        public ActionResult Delete(Employee employee)
        {
            try
            {
                employeeRepository.Delete(employee);
                return Ok();
            }
            catch
            {
                return BadRequest(new
                {
                    ErrorMessage = "Váratlan hiba"
                });
            }
        }
    }
}
