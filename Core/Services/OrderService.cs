using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly CarsDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;

        public OrdersService(CarsDbContext context, IMapper mapper, ICartService cartService)
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
        }
        public void Create(string userId)
        {
            // create order
            var newOrder = new Order()
            {
                CreatedAt = DateTime.Now,
                Cars = cartService.GetProductsEntity(),
                UserId = userId
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

            cartService.Clear();
        }

        public List<OrderDto> GetOrders(string userId)
        {
            var orders = context.Orders.Include(x => x.User)
                                        .Where(x => x.UserId == userId)
                                        .ToList();
            return mapper.Map<List<OrderDto>>(orders);
        }
    }
}
