using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
		public string BookName { get; set; }
		public string AuthorName { get; set; }
		public int Rating { get; set; }
		public int TotalView { get; set; }
		public int OriginalPrice { get; set; }
		public int DiscountedPrice { get; set; }
		public string BookDetails { get; set; }
		public string BookImage { get; set; }

	}
}
