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
    public class OfferingController : ControllerBase
    {
        private readonly IOfferingService _service;
        private readonly IMapper _mapper;

        public OfferingController(IOfferingService offeringService, IMapper mapper)
        {
            _service = offeringService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<OfferingViewModel>> GetAllAsync()
        {
            var offerings = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Offering>, IReadOnlyCollection<OfferingViewModel>>(offerings)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferingViewModel>> GetByIdAsync(int id)
        {
            var offering = await _service.GetItemByIdAsync(id);

            if (offering == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Offering, OfferingViewModel>(offering);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IReadOnlyCollection<DepartmentViewModel>>> GetChildrenAsync(int id)
        {
            var departments = await _service.GetDepartmentsAsync(id);

            if (departments == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<IReadOnlyCollection<Department>, IReadOnlyCollection<DepartmentViewModel>>(departments)
                    .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<OfferingViewModel>> CreateAsync(OfferingViewModel offeringView)
        {
            if (ModelState.IsValid)
            {
                var offering = _mapper.Map<OfferingViewModel, Offering>(offeringView);

                var createdOffering = await _service.CreateAsync(offering);
                offeringView.Id = createdOffering.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = offeringView.Id }, offeringView);
            }

            return BadRequest(offeringView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(OfferingViewModel offeringView, int id)
        {
            if (id == offeringView.Id)
            {
                if (ModelState.IsValid)
                {
                    var family = _mapper.Map<OfferingViewModel, Offering>(offeringView);
                    await _service.UpdateAsync(id, family);

                    return NoContent();
                }

                return BadRequest(offeringView);
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