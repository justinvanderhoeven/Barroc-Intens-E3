using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }

        public ICollection<Company> Companies { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
