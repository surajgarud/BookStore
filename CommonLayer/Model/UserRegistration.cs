using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegistration
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public long MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
