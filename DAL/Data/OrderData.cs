using DAL.Interface;
using DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class OrderData:IOrder
    {
        private readonly BookletContext _context;
        private readonly IMapper _mapper;
        public OrderData(BookletContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrder(OrderDto order)
        {
            var myOrder = _mapper.Map<Orders>(order);
            _context.Orders.Add(myOrder);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<List<OrderDto>> GetAllOrder()
        {
            var orders = _context.Orders.ToList();
            var myOrederDto = _mapper.Map<List<OrderDto>>(orders);
            return myOrederDto;
        }
        public async Task<OrderDto> GetOrderByName(string name)
        {
            var myOrder = _context.Orders.FirstOrDefault(book => book.OrderingName == name);
            var myOrderDto = _mapper.Map<OrderDto>(myOrder);
            return myOrderDto;
        }
        public async Task DeleteOrder(int id)
        {
            var myOrder = await _context.Orders.Where(b => b.ID == id).FirstOrDefaultAsync();
            if (myOrder != null)
            {
                _context.Orders.Remove(myOrder);
                _context.SaveChanges();
            }
        }
    }
}
