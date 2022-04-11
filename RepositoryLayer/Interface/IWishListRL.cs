using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool DeleteFromWishlist(int userId, int wishlistId);
        public List<WishListModel> GetAllFromWishlist(int userId);
    }
}
