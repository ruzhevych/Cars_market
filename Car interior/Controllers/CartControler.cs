using AutoMapper;
using Car_market.Extensions;
using Core.Dtos;
using Data.Data;
using Car_market.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_market.Controllers
{
    public class CartController : Controller
    {
        private readonly IMapper mapper;
        private readonly CarsDbContext context;

        public CartController(IMapper mapper, CarsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items") ?? new();

            var cars = context.Cars.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return View(mapper.Map<List<CarsDto>>(cars));
        }

        public IActionResult Add(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) ids = new();

            ids.Add(id);

            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items");

            if (ids == null || !ids.Contains(id)) return NotFound();

            ids.Remove(id);

            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index");
        }
    }
}
