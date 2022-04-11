﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel order, int userId);
        public List<OrderModel> GetAllOrder(int userId);
    }
}
