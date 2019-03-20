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
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _service;
        private readonly IMapper _mapper;

        public BusinessController(IBusinessService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<BusinessViewModel>> GetAllAsync()
        {
            var businesses = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Business>, IReadOnlyCollection<BusinessViewModel>>(businesses)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessViewModel>> GetByIdAsync(int id)
        {
            var business = await _service.GetItemByIdAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Business, BusinessViewModel>(business);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IReadOnlyCollection<FamilyViewModel>>> GetChildrenAsync(int id)
        {
            var families = await _service.GetFamiliesAsync(id);

            if (families == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<IReadOnlyCollection<Family>, IReadOnlyCollection<FamilyViewModel>>(families)
                    .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<BusinessViewModel>> CreateAsync(BusinessViewModel businessView)
        {
            if (ModelState.IsValid)
            {
                var business = _mapper.Map<BusinessViewModel, Business>(businessView);

                var createdBusiness = await _service.CreateAsync(business);
                businessView.Id = createdBusiness.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = businessView.Id }, businessView);
            }

            return BadRequest(businessView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(BusinessViewModel businessView, int id)
        {
            if (id == businessView.Id)
            {
                if (ModelState.IsValid)
                {
                    var business = _mapper.Map<BusinessViewModel, Business>(businessView);
                    await _service.UpdateAsync(id, business);

                    return NoContent();
                }

                return BadRequest(businessView);
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