using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Dtos;


namespace DAL.Interface
{
    public interface IManager
    {
        public List<ManagerDto> getAllManager();
        Task<bool> addManager(ManagerDto managerDto);
    }
}
