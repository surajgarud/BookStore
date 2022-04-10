using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishListBL : IWishListBL
    {
            private readonly IWishListRL wishListRL;
            public WishListBL(IWishListRL wishListRL)
            {
                this.wishListRL = wishListRL;
            }

        public string AddToWishlist(int bookId, int userId)
        {
            try
            {
                return this.wishListRL.AddToWishlist(bookId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
