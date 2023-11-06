using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class ContractProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int PricePerProduct { get; set; }
        public int ContractId { get; set; }
        public int ProductId { get; set; }
    }
}
