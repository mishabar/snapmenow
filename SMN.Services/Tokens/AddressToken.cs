using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data;

namespace SMN.Services.Tokens
{
    public class AddressToken
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [RegularExpression(@"\d{4}", ErrorMessage = "Please provide a 4 digit code")]
        [Display(Name = "Postcode")]
        public string ZipCode { get; set; }
        public string Phone { get; set; }
    }

    public static class AddressTokenExtensions
    {
        public static AddressToken AsToken(this Address address)
        {
            return new AddressToken
            {
                Name = address.Name,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                State = address.State,
                ZipCode = address.ZipCode,
                Phone = address.Phone
            };
        }
    }
}
