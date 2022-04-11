using BusinessLayer.Interface;
using CommonLayer.Model;
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

        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                return this.wishListRL.DeleteFromWishlist(userId, wishlistId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WishListModel> GetAllFromWishlist(int userId)
        {
            try
            {
                return this.wishListRL.GetAllFromWishlist(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
