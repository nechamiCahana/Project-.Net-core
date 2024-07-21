using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public string OrderingName { get; set; }
        public Dictionary<string, int>  MyAllBooklet { get; set; }
        public DateTime DateOrder { get; set; }

    }
}
