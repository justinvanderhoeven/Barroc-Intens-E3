using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Contract
    {
        public int Id { get; set; }
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
        public DateOnly ActiveUntil {  get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ContractProduct> ContractProducts { get; set; }
    }
}
