using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Dtos;

namespace DAL.Interface
{
    public interface IOrder
    {
        Task<bool> CreateOrder(OrderDto order);
        Task<List<OrderDto>> GetAllOrder();
        Task<OrderDto> GetOrderByName(string name);
        Task DeleteOrder(int id);
    }
}
