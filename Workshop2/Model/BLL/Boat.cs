using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Workshop2.Model.BLL
{
    class Boat
    {
        public enum BoatTypes { Sailboat, Motorsailer, Canoe, Other };

        [Range(0, int.MaxValue, ErrorMessage = "Boat id is out of range.")]
        public int BoatId { get; set; }

        [Required(ErrorMessage = "Member id is required.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Boat must have a length.")]
        public decimal Length { get; set; }

        [Required(ErrorMessage = "Boat must have a type.")]
        public BoatTypes BoatType { get; set; }
    }
}
