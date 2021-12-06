using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApix.Data.Models;
using TaskApix.Dtos.Region;
using TaskApix.Services.IRepository;

namespace TaskApix.MVC.Controllers
{
    public class RegionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<IActionResult> Index()
        {
            var regions = await _unitOfWork.Regions.GetAll();
            return View(regions);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.CountryId = await _unitOfWork.Countries.GetAll();
             
            return View();
        }
        public async Task<IActionResult> Add(RegionCreation regionDto)
        {
            var region = _mapper.Map<Region>(regionDto);
            await _unitOfWork.Regions.Insert(region);
            await _unitOfWork.Save();
            return Redirect("Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            region.Country = await _unitOfWork.Countries.Get(p => p.Id == region.CountryId);
            ViewBag.Countries = await _unitOfWork.Countries.GetAll();
            return View(region);
        }
        public async Task<IActionResult> Save(Region region)
        {
            _unitOfWork.Regions.Update(region);
            await _unitOfWork.Save();
            return Redirect("Index");
        }
        public async Task<IActionResult> Details(long Id)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            region.Country = await _unitOfWork.Countries.Get(p => p.Id == region.CountryId);
            return View(region);
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var region = await _unitOfWork.Regions.Get(p => p.Id == Id);
            region.Country = await _unitOfWork.Countries.Get(p => p.Id == region.CountryId);
            return View(region);
        }
        public async Task<IActionResult> Remove(Region region)
        {
            _unitOfWork.Regions.Delete(region);
            await _unitOfWork.Save();
            return Redirect("Index");
        }
    }
}
