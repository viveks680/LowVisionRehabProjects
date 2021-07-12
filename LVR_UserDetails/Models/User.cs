using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVR_UserDetails.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public int BCVA { get; set; }

        public User()
        {

        }
    }
}
