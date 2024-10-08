﻿﻿using AutoMapper;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car_market.Extensions;
using Core.Interfaces;

namespace Car_market.Services
{
    public class CartService : ICartService
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

        public List<Cars> GetProductsEntity()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            return context.Cars.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();
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

        public void Clear()
        {
            httpContext.Session.Remove("cart_items");
        }
    }
}
