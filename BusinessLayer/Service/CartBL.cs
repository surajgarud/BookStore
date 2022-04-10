﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
       
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }


        public CartModel AddCart(CartModel cart, int userId)
        {
            try
            {
                return this.cartRL.AddCart(cart,userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCart(int cartId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Cart> GetCartByUserId(int userId)
        {
            try
            {
                return this.cartRL.GetCartByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel UpdateCart(CartModel cart, int userId)
        {
            try
            {
                return this.cartRL.UpdateCart(cart, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
