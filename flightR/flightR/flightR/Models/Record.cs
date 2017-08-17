using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Models
{
    public class Record
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UserId { get; set; } = 1;
    }
}
