using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u18043624_HW06.Models.ViewModels
{
	public class DetailsVM
	{
        public string product_name { get; set; }
        public short model_year { get; set; }
        public decimal list_price { get; set; }
        public string brand_name { get; set; }
        public string category_name { get; set; }
        public List<QuantityVM> Quantities { get; set; }
    }
}