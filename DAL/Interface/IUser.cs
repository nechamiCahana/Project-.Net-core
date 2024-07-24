using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Dtos;

namespace DAL.Interface
{
    public interface IUser
    {
        Task<bool> createUser(UserDto user);
        Task deleteUser(int id);
        Task<List<UserDto>> getAllUsers();
        public (string status, UserDto afterMapper) getByMail(string mail);
        Task<bool> updateUser(UserDto user);
    }
}
