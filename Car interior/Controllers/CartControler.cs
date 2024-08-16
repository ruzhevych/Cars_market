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
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(cartService.GetProducts());
        }

        public IActionResult Add(int id)
        {
            cartService.AddItem(id);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }
    }
}
