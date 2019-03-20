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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;
        private readonly IMapper _mapper;

        public CountryController(ICountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<CountryViewModel>> GetAllAsync()
        {
            var countries = await _service.GetAllAsync();

            return _mapper
                    .Map<IReadOnlyCollection<Country>, IReadOnlyCollection<CountryViewModel>>(countries)
                    .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryViewModel>> GetByIdAsync(int id)
        {
            var country = await _service.GetItemByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<Country, CountryViewModel>(country);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IReadOnlyCollection<BusinessViewModel>>> GetChildrenAsync(int id)
        {
            var businesses = await _service.GetBusinessesAsync(id);

            if (businesses == null)
            {
                return NotFound();
            }

            return _mapper
                    .Map<IReadOnlyCollection<Business>, IReadOnlyCollection<BusinessViewModel>>(businesses)
                    .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<CountryViewModel>> CreateAsync(CountryViewModel countryView)
        {
            if (ModelState.IsValid)
            {
                var country = _mapper.Map<CountryViewModel, Country>(countryView);

                var createdCountry = await _service.CreateAsync(country);
                countryView.Id = createdCountry.Id;

                return CreatedAtAction(nameof(GetByIdAsync), new { id = countryView.Id }, countryView);
            }

            return BadRequest(countryView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(CountryViewModel countryView, int id)
        {
            if (id == countryView.Id)
            {
                if (ModelState.IsValid)
                {
                    var country = _mapper.Map<CountryViewModel, Country>(countryView);
                    await _service.UpdateAsync(id, country);

                    return NoContent();
                }

                return BadRequest(countryView);
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