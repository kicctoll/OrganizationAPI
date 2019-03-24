using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly IMapper _mapper;

        public OrganizationController(IOrganizationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationViewModel>>> GetAllAsync()
        {
            var organizations = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Organization>, IReadOnlyCollection<OrganizationViewModel>>(organizations)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationViewModel>> GetByIdAsync(int id)
        {
            var organization = await _service.GetItemByIdAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Organization, OrganizationViewModel>(organization);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IReadOnlyCollection<CountryViewModel>>> GetChildrenByIdAsync(int id)
        {
            var countries = await _service.GetCountriesAsync(id);

            if (countries == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<IReadOnlyCollection<Country>, IReadOnlyCollection<CountryViewModel>>(countries)
                    .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> CreateAsync(OrganizationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<OrganizationViewModel, Organization>(viewModel);

                var createdEntity = await _service.CreateAsync(model);
                viewModel.Id = createdEntity.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = viewModel.Id }, viewModel);
            }

            return BadRequest(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(OrganizationViewModel viewModel, int id)
        {
            if (viewModel.Id == id)
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<OrganizationViewModel, Organization>(viewModel);
                    await _service.UpdateAsync(id, model);

                    return NoContent();
                }

                return BadRequest(viewModel);
            }

            return BadRequest(
                new
                {
                    title = "The Id of passed entity is not equal to id passed through query string",
                }
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}