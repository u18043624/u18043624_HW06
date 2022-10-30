using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u18043624_HW06.Models
{
	public class ProductsCRUDClass
	{
		public int product_id;
		public string product_name;
		public int brand_id;
		public int category_id;
		public int model_year;
		public decimal list_price;
		public virtual CategoryCRUDClass category { get; set; }
		public virtual BrandCRUDClass brand { get; set; }
	}
}