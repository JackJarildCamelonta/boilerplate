using System.Collections.Generic;
using Boilerplate.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boilerplate.WebApi.Controllers
{
    /// <summary>
    /// Test API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/employee")]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of all employees</returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "getAllEmployees")]
        public IEnumerable<Employee> Get()
        {
            return GetEmployees();
        }

        /// <summary>
        /// Get an employee
        /// </summary>
        /// <returns>An employee</returns>
        [HttpGet("{id}", Name = "Get")]
        [SwaggerOperation(OperationId = "getEmployeeById")]
        public Employee Get(int id)
        {
            return GetEmployees().Find(e => e.Id == id);
        }

        /// <summary>
        /// Create an employee
        /// </summary>
        /// <returns>The created employee</returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "createEmployee")]

        public Employee Post([FromBody] Employee employee)
        {
            // Logic to create new Employee
            return new Employee();
        }

        /// <summary>
        /// Update an employee
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "updateEmployee")]

        public void Put(int id, [FromBody] Employee employee)
        {
            // Logic to update an Employee
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "deleteEmployee")]

        public void Delete(int id)
        {
        }

        private List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    FirstName= "John",
                    LastName = "Smith",
                    Email ="John.Smith@gmail.com"
                },
                new Employee()
                {
                    Id = 2,
                    FirstName= "Jane",
                    LastName = "Doe",
                    Email ="Jane.Doe@gmail.com"
                }
            };
        }
    }
}