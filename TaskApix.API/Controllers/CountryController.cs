using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApix.Data.Models;
using TaskApix.Dtos;
using TaskApix.Services.IRepository;
using TaskApix.Services.Extensions;
using TaskApix.Dtos.CountryDto;

namespace TaskApix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromForm] CountryCreation creation)
        {
            var counrty = _mapper.Map<Country>(creation);

            await _unitOfWork.Countries.Insert(counrty);
            await _unitOfWork.Save();

            return Created("Country", counrty);
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetCountry(long Id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == Id);
            var regions = await _unitOfWork.Regions.GetAll(p => p.CountryId == country.Id);
            country.Regions = regions.ToList();
            if (country is null)
                return NotFound();
            return Ok(country);
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            if (requestParams.OrderBy is null)
                requestParams.OrderBy = "Id";

            var countries = await _unitOfWork.Countries.GetPagedList(requestParams,
                order => order.OrderBy(requestParams.OrderBy));

            var response = new ResponseDto
            {
                PageCount = countries.PageCount,
                Total = countries.TotalItemCount,
                Current = countries.PageNumber,
                PageSize = countries.PageSize,
                HasPreviousPage = countries.HasPreviousPage,
                HasNextPage = countries.HasNextPage,
                FirstItemOnPage = countries.FirstItemOnPage,
                LastItemOnPage = countries.LastItemOnPage,
                Data = _mapper.Map<IEnumerable<CountryDto>>(countries)
            };

            return Ok(response);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateCountry(long id, [FromForm] CountryCreation creation)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == id);
            if (country is null)
                return NotFound();
            _mapper.Map(creation, country);
            country.IsSchengen = true;

            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCountry([FromBody] long Id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == Id);
            if (country is null)
                return NotFound();
            _unitOfWork.Countries.Delete(country);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
