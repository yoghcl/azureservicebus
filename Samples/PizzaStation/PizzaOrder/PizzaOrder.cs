﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStation.Models
{
    [DataContract]
    public class PizzaOrder
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Size { get; set; }
    }
}
