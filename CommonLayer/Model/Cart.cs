﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
        public int userId { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
