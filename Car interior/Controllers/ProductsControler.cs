﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using Car_market.Models;
using Core.Validations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Dtos;

namespace Car_market.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CarsDbContext ctx;
        private readonly IMapper mapper;

        public ProductsController(IMapper mapper, CarsDbContext context)
        {
            this.mapper = mapper;
            this.ctx = context;
        }

        public IActionResult Index()
        {
            // .. load data from database ..
            var cars = ctx.Cars
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => !x.Archived)
                .ToList();

            return View(mapper.Map<List<CarsDto>>(cars));
        }

        public IActionResult Details(int id)
        {
            var product = ctx.Cars.Find(id);

            if (product == null) return NotFound();

            return View(mapper.Map<CarsDto>(product));
        }

        // GET: 
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            ViewBag.CreateMode = true;
            return View("Upsert");
        }

        // POST
        [HttpPost]
        public IActionResult Create(CarsDto model)
        {
            //// data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                LoadCategories();
                return View("Upsert", model);
            }

            // 1 - manual mapping
            //var entity = new Product
            //{
            //    Name = model.Name,
            //    Archived = model.Archived,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    Discount = model.Discount,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    Quantity = model.Quantity
            //};
            // 2 - using Auto Mapper
            var entity = mapper.Map<Cars>(model);

            ctx.Cars.Add(entity);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cars = ctx.Cars.Find(id);

            if (cars == null) return NotFound();

            LoadCategories();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<CarsDto>(cars));
        }

        [HttpPost]
        public IActionResult Edit(CarsDto model)
        {
           // data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                LoadCategories();
                return View("Upsert", model);
            }

            ctx.Cars.Update(mapper.Map<Cars>(model));
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cars = ctx.Cars.Find(id);

            if (cars == null) return NotFound();

            ctx.Cars.Remove(cars);
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }

        public IActionResult Archive()
        {
            // .. load data from database ..
            var cars = ctx.Cars
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => x.Archived)
                .ToList();

            return View(mapper.Map<List<CarsDto>>(cars));
        }

        public IActionResult ArchiveItem(int id)
        {
            var cars = ctx.Cars.Find(id);

            if (cars == null) return NotFound();

            cars.Archived = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RestoreItem(int id)
        {
            var cars = ctx.Cars.Find(id);

            if (cars == null) return NotFound();

            cars.Archived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }

        private void LoadCategories()
        {
            // ViewBag.PropertyName = value;
            ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
        }
    }
}
