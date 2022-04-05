using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegistration Register(UserRegistration user)
        {
            try
            {
                return this.userRL.Register(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
