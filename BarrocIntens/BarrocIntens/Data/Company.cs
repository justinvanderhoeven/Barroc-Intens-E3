using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone {  get; set; }
        public string Street {  get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string CountryCode {  get; set; }
        public DateTime BkrCheckedAt { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Note> Notes { get; set; }

        //public int UserId {  get; set; }
        //public User User { get; set; }
    }
}
