using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class MaintenanceAppointment
    {
        public int Id { get; set; }
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
        public string Remark {  get; set; }
        public DateTime DateAdded { get; set; }
    }
}
