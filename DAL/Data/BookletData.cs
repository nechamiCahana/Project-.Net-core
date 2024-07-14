using System;
using DAL.Interface;
using DAL.Dtos;
using MODELS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class BookletData:IBooklet
    {
        private readonly BookletContext _context;
        private readonly IMapper _mapper;
        public BookletData(BookletContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateBooklet(BookletDto booklet)
        {
            var myBooklet= _mapper.Map<Booklet>(booklet);
            _context.Booklets.Add(myBooklet);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<List<BookletDto>> GetAllBooklet()
        {
            var booklet = _context.Booklets.ToList();
            var mybookletsDto = _mapper.Map<List<BookletDto>>(booklet);
            return mybookletsDto;
        }

        public async Task<BookletDto> GetBookletByName(string name)
        {
            var myBooklet=_context.Booklets.FirstOrDefault(book => book.Name == name);
            var mybookletDto = _mapper.Map<BookletDto>(myBooklet);
            return mybookletDto;
        }

        public async Task UpdatePrice(double price, int id)
        {
            var Booklet = _context.Booklets.Find(id);

            if (Booklet == null)
            {
                throw new NotImplementedException();
            }

            Booklet.Price = price;

            _context.Update(Booklet);
            _context.SaveChangesAsync();
        }

        public async Task DeleteBooklet(int id)
        {
            var myBooklet= await _context.Booklets.Where(b => b.ID==id).FirstOrDefaultAsync();
            if (myBooklet != null)
            {
                _context.Booklets.Remove(myBooklet);
                _context.SaveChanges();
            }
        }
    }
}
