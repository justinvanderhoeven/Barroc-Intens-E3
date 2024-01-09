using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class MaintenanceAppointment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? UserId { get; set; } = null;
        public User User { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
