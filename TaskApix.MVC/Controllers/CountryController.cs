using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApix.Data.Models;
using TaskApix.Dtos.CountryDto;
using TaskApix.Services.IRepository;

namespace TaskApix.MVC.Controllers
{
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        
        public async Task<IActionResult> Index()
        {
            var countries = await _unitOfWork.Countries.GetAll();
            return View(countries);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Add(CountryCreation countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            await _unitOfWork.Countries.Insert(country);
            await _unitOfWork.Save();
            return Redirect("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == id);
            return View(country);
        }
        public async Task<IActionResult> Save(Country country)
        {
            country.IsSchengen = true;
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();
            return Redirect("Index");
        }
        public async Task<IActionResult> Details(long id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == id);
            return View(country);
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == Id);
            return View(country);
        }

        public async Task<IActionResult> Remove(Country country)
        {
            _unitOfWork.Countries.Delete(country);
            await _unitOfWork.Save();
            return Redirect("Index");
        }
    }
}
