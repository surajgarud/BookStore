using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistration Register(UserRegistration user);
        public UserLogin Login(string EmailId, string Password);
        public string ForgotPassword(string EmailId);
        public bool ResetPassword(string EmailId, string NewPassword, string ConfirmPassword);
    }
}
