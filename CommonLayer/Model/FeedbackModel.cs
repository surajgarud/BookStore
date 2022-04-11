﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }

        public int Rating { get; set; }

        public int BookId { get; set; }

        public UserRegistration User { get; set; }
    }
}
