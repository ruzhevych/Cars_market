using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Car_interior.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly CarsDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public OrdersController(CarsDbContext context, IMapper mapper, ICartService cartService)
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            var orders = context.Orders.Include(x => x.User)
                                        .Where(x => x.UserId == UserId)
                                        .ToList();
            return View(mapper.Map<List<OrderDto>>(orders));
        }

        public IActionResult Create()
        {
            // create order
            var newOrder = new Order()
            {
                CreatedAt = DateTime.Now,
                Cars = cartService.GetProductsEntity(),
                UserId = this.UserId
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

            cartService.Clear();

            return RedirectToAction(nameof(Index));
        }
    }
}
