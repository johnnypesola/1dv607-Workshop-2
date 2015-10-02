using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2.Model
{
    class Boat
    {
        [Range(0, int.MaxValue, ErrorMessage = "Boat id is out of range.")]
        public int Id { get; set; }
    }
}
