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
        [Range(0, int.MaxValue, ErrorMessage = "Boat id is out of range.")]
        public int BoatId { get; set; }

        [Required(ErrorMessage = "Member id is required.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Boat must have a length.")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Boat length must be in the following format: NN.N")]
        public decimal BoatLength { get; set; }

        [Required(ErrorMessage = "Boat must have a type.")]
        public string BoatType { get; set; }
    }
}
