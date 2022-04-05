using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IUserBL
   {
        public UserRegistration Register(UserRegistration user);
        public UserLogin Login(string EmailId, string Password);
   }
}
