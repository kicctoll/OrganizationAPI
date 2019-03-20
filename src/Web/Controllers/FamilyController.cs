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
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _service;
        private readonly IMapper _mapper;

        public FamilyController(IFamilyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<FamilyViewModel>> GetAllAsync()
        {
            var families = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Family>, IReadOnlyCollection<FamilyViewModel>>(families)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyViewModel>> GetByIdAsync(int id)
        {
            var families = await _service.GetItemByIdAsync(id);

            if (families == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Family, FamilyViewModel>(families);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IReadOnlyCollection<OfferingViewModel>>> GetChildrenAsync(int id)
        {
            var offerings = await _service.GetOfferingsAsync(id);

            if (offerings == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<IReadOnlyCollection<Offering>, IReadOnlyCollection<OfferingViewModel>>(offerings)
                    .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<FamilyViewModel>> CreateAsync(FamilyViewModel familyView)
        {
            if (ModelState.IsValid)
            {
                var family = _mapper.Map<FamilyViewModel, Family>(familyView);

                var createdFamily = await _service.CreateAsync(family);
                familyView.Id = createdFamily.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = familyView.Id }, familyView);
            }

            return BadRequest(familyView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(FamilyViewModel familyView, int id)
        {
            if (id == familyView.Id)
            {
                if (ModelState.IsValid)
                {
                    var family = _mapper.Map<FamilyViewModel, Family>(familyView);
                    await _service.UpdateAsync(id, family);

                    return NoContent();
                }

                return BadRequest(familyView);
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