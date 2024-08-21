using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Core.Services;
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
        private readonly IOrdersService ordersService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public IActionResult Index()
        {
            return View(ordersService.GetOrders(UserId));
        }

        public IActionResult Create()
        {
            ordersService.Create(UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
