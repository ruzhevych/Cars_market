using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdersService
    {
        List<OrderDto> GetOrders(string userId);
        void Create(string userId);
    }
}
