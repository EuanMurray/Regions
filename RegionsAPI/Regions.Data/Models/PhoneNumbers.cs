using System;
using System.Collections.Generic;

namespace Region.Data.Models
{
    public partial class PhoneNumbers
    {
        public string PhoneNumber { get; set; }
        public int RegionId { get; set; }
        public string LineNumber { get; set; }
    }
}
