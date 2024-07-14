using System;
using DAL.Dtos;
using DAL.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS.Models;

namespace DAL.Interface
{
    public interface IBooklet
    {
        Task<bool> CreateBooklet(BookletDto booklet);
        Task<List<BookletDto>> GetAllBooklet();
        Task<BookletDto> GetBookletByName(string name);
        Task UpdatePrice(double price,int id);
        Task DeleteBooklet(int id);
    }
}
