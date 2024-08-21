using AutoMapper;
using Car_market.Extensions;
using Core.Dtos;
using Data.Data;
using Car_market.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace Car_market.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(cartService.GetProducts());
        }

        public IActionResult Add(int id, string returnUrl)
        {
            cartService.AddItem(id);
            return Redirect(returnUrl ?? "/");
        }

        public IActionResult Remove(int id)
        {
            cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }
    }
}
