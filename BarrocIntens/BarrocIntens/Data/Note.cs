using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class Note
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Content {  get; set; }
        public DateTime Date { get; set; }
        public int CompanyId {  get; set; }
        public int UserId {  get; set; }
    }
}
