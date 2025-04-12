using RentVilla.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Users
{
    public class CompanyProfile:CommonEntity<int>
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string EconomicCode { get; set; }  
        public string NationalId { get; set; } 
        public string LogoUrl { get; set; }
        public string Website { get; set; }

        public User User { get; set; }

    }
}
