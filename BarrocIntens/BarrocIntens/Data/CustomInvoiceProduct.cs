﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class CustomInvoiceProduct
    {
        public int Id {  get; set; }
        public int ProductId { get; set; }
        public int CustomInvoiceId {  get; set; }
        public int Amount { get; set; }
        public decimal PricePerProduct {  get; set; }
    }
}
