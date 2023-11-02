using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class Contract
    {
        public int Id { get; set; }
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
        public int Active {  get; set; }
    }
}
