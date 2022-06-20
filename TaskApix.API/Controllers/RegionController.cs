using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApix.Data.Models;
using TaskApix.Dtos;
using TaskApix.Dtos.CountryDto;
using TaskApix.Dtos.Region;
using TaskApix.Services.IRepository;
using TaskApix.Services.Extensions;

namespace TaskApix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromForm] RegionCreation creation)
        {
            var region = _mapper.Map<Region>(creation);
            var country = await _unitOfWork.Countries.Get(p => p.Id == creation.CountryId);
            if (country is null)
                return NotFound();
            await _unitOfWork.Regions.Insert(region);
            await _unitOfWork.Save();

            return Created("Region", region);
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions([FromQuery] RequestParams requestParams)
        {
            if (requestParams.OrderBy is null)
                requestParams.OrderBy = "Id";

            var regions = await _unitOfWork.Regions.GetPagedList(requestParams,
                order => order.OrderBy(requestParams.OrderBy));
            

            var response = new ResponseDto
            {
                PageCount = regions.PageCount,
                Total = regions.TotalItemCount,
                Current = regions.PageNumber,
                PageSize = regions.PageSize,
                HasPreviousPage = regions.HasPreviousPage,
                HasNextPage = regions.HasNextPage,
                FirstItemOnPage = regions.FirstItemOnPage,
                LastItemOnPage = regions.LastItemOnPage,
                Data = _mapper.Map<IEnumerable<RegionDto>>(regions)
            };

            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetRegion(long Id)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            region.Country = await _unitOfWork.Countries.Get(p => p.Id == region.CountryId);
            if (region is null)
                return NotFound();
            return Ok(region);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateRegion(long Id, RegionCreation creation)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            if (region is null)
                return NotFound();
            _mapper.Map(creation, region);

            _unitOfWork.Regions.Update(region);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRegion(long Id)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            if (region is null)
                return NotFound();
            _unitOfWork.Regions.Delete(region);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
