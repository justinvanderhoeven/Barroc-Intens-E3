﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone {  get; set; }
        public string Address {  get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string CountryCode {  get; set; }
        public DateTime BkrCheckedAt { get; set; }
        public int? ContactId { get; set; } = null;
        public User Contact { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Note> Notes { get; set; }

        [InverseProperty(nameof(Contract.Company))]
        public ICollection<Contract> CompanyContracts { get; set; }

    }
}
