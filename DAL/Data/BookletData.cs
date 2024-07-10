using System;
using DAL.Interface;
using DAL.Dtos;
using MODELS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

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
        //public async Task<List<BookletDto>> GetAllBooklet()
        //{
        //    return;
        //}
        //public async Task<BookletDto> GetBookletByName(string name)
        //{
        //    return BookletDto();
        //}
    }
}
