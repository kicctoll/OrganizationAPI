using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Web.ViewModels;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<DepartmentViewModel>> GetAllAsync()
        {
            var departments = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Department>, IReadOnlyCollection<DepartmentViewModel>>(departments)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentViewModel>> GetByIdAsync(int id)
        {
            var department = await _service.GetItemByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Department, DepartmentViewModel>(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentViewModel>> CreateAsync(DepartmentViewModel departmentView)
        {
            if (ModelState.IsValid)
            {
                var offering = _mapper.Map<DepartmentViewModel, Department>(departmentView);

                var createdDepartment = await _service.CreateAsync(offering);
                departmentView.Id = createdDepartment.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = departmentView.Id }, departmentView);
            }

            return BadRequest(departmentView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(DepartmentViewModel departmentView, int id)
        {
            if (id == departmentView.Id)
            {
                if (ModelState.IsValid)
                {
                    var family = _mapper.Map<DepartmentViewModel, Department>(departmentView);
                    await _service.UpdateAsync(id, family);

                    return NoContent();
                }

                return BadRequest(departmentView);
            }

            return BadRequest(
                new
                {
                    title = "The Id of passed entity is not equal to id passed through a query string"
                });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}