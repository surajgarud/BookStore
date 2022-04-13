using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminLogin Adminlogin(string emailid, string password)
        {
            try
            {
                return this.adminRL.Adminlogin(emailid, password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
