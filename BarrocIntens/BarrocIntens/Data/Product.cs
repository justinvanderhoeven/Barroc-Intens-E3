using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public int? MaintenanceAppointmentId { get; set; } = null;
        public MaintenanceAppointment MaintenanceAppointment { get; set; }
        public ICollection<CustomInvoice> CustomInvoices { get; set; }
        public ICollection<CustomInvoiceProduct> CustomInvoiceProducts { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<ContractProduct> ContractProducts { get; set; }
        public int ProductsCategoryId { get; set; }
        public ProductsCategory ProductsCategory { get; set; }
    }
}
