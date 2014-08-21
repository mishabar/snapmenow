using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMN.Services.Tokens;

namespace SMN.Web.Models
{
    public class AddressModel
    {
        public AddressToken Address { get; set; }
        public int Step { get; set; }
        public string ID { get; set; }

        public static IEnumerable<SelectListItem> States() 
        {
            return new string[] 
            {
                "Australian Capital Territory", 
                "New South Wales",
                "Northern Territory",
                "Queensland",
                "South Australia",
                "Tasmania", 
                "Victoria",
                "Western Australia"
            }.Select(v => new SelectListItem { Text = v, Value = v });
        }
    }
}