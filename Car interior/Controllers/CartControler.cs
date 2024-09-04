using AutoMapper;
using Car_market.Extensions;
using Core.Dtos;
using Data.Data;
using Car_market.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Car_market.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly RoleManager<IdentityRole> userManager;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
             this.userManager = userManager;
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
