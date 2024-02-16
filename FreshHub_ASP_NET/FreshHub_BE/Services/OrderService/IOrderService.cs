﻿using FreshHub_BE.Data.Entities;
using FreshHub_BE.Enums;
using FreshHub_BE.Models;

namespace FreshHub_BE.Services.OrderService
{
    public interface IOrderService
    {
        public Task<OrderResultModel> Create(OrderModel orderModel, int userId);
        public Task<OrderResultModel> Update(OrderModel orderModel);
        public Task<OrderResultModel> Rejected(int Id);
        public Task<OrderResultModel> Status(int Id, PaymentEnum status);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetAllByUser(int userId);
        public Task<OrderResultModel> GetOrderInfo(int orderId);

    }
}