﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Models
{
    public class Order
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public Guid IdMandant { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string TxtLarge { get; set; }
        public string TxtSmall { get; set; }
    }
}
