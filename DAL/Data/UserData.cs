using AutoMapper;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Dtos;

namespace DAL.Data
{
    public class UserData:IUser
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        //אתחול ההזרקה
        public UserData(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> createUser(UserDto user)
        {
            var myUser = _mapper.Map<User>(user);
            _context.Users.Add(myUser);
            var isOk = _context.SaveChanges() >= 0;

            return isOk;
        }

        public async Task deleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new NotImplementedException();
                //return ("Not Found", $"Fitness machine with ID {id} not found.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<List<UserDto>> getAllUsers()
        {
            var myUser = _context.Users.ToList();
            var myUserDto = _mapper.Map<List<UserDto>>(myUser);
            return myUserDto;
        }

        public (string status, UserDto afterMapper) getByMail(string mail)
        {
            var mailFind = _context.Users.FirstOrDefault(b => b.mail == mail);
            var afterMapper = _mapper.Map<UserDto>(mailFind);
            if (afterMapper == null)
            {
                return ("Not Found", null);
            }
            return ("Found", afterMapper);
        }

        public async Task<bool> updateUser(UserDto user)
        {

            var useFind = _context.Users.Find(user.Id);
            if (useFind == null)
            {
                throw new NotImplementedException();

            }

            useFind.mail = user.mail;
            useFind.address = user.address;
            useFind.password = user.password;
            useFind.Name = user.Name;



            _context.Users.Update(useFind);
            return _context.SaveChanges() > 0;
        }   
    }

}
