using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountGrpc.Model
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
    }
}
