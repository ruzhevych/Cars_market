﻿﻿using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car_market.Extensions;

namespace Car_market.Services
{
    //public interface ICartService
    //{
    //    int GetCount();
    //    void AddItem(int id);
    //    void RemoveItem(int id);
    //    List<ProductDto> GetProducts();
    //}

    public class CartService
    {
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;
        private readonly CarsDbContext context;
        public CartService(IHttpContextAccessor contextAccessor, IMapper mapper, CarsDbContext context)
        {
            httpContext = contextAccessor.HttpContext!;
            this.mapper = mapper;
            this.context = context;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) return 0;

            return ids.Distinct().Count();
        }

        public List<CarsDto> GetProducts() 
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            var products = context.Cars.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return mapper.Map<List<CarsDto>>(products);
        }

        public void AddItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) ids = new();

            ids.Add(id);

            httpContext.Session.Set("cart_items", ids);
        }

        public void RemoveItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null || !ids.Contains(id)) return; // throw exception

            ids.Remove(id);

            httpContext.Session.Set("cart_items", ids);
        }
    }
}
