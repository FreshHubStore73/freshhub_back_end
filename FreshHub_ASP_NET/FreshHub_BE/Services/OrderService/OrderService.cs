﻿using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Enums;
using FreshHub_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext dbContext;

        public OrderService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<OrderResultModel> Create(OrderModel orderModel, int userId)
        {
            Order order = new Order
            {
                CashSum = orderModel.CashSum,
                Call = orderModel.Call,
                Comment = orderModel.Comment,
                DeliveryAddress = new DeliveryAddress
                {
                    StreetHouse = orderModel.StreetHouse,
                    Flat = orderModel.Flat,
                    Floor = orderModel.Floor
                },
                NumberPerson = orderModel.NumberPerson,
                OrderDateOnly = orderModel.OrderDateOnly,
                OrderStatusId = 1,
                OrderTimeOnly = orderModel.OrderTimeOnly,
                Payment = orderModel.Payment,
                PaymentStatus = orderModel.PaymentStatus,
                PhoneNumber = orderModel.PhoneNumber,
                Recipient = orderModel.Recipient,
                UserId = userId
            };

            var user = await dbContext.Users.Include(u => u.Carts).ThenInclude(c => c.CartItems).FirstOrDefaultAsync(x => x.Id == userId);

            var cart = user.Carts.First();

            order.OrderDatails = cart.CartItems.Select(ci => new OrderDatail
            {
                Order = order,
                Quantity = ci.Quantity,
                ProductId = ci.ProductId,
                Price = ci.Price
            }).ToList();

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return new OrderResultModel {Id = order.Id, Summ = order.OrderDatails.Sum(x=>x.Quantity *x.Price)};
        }

        public async Task<List<Order>> GetAll()
        {
            return await dbContext.Orders
                .Include(o => o.OrderStatus)                
                .Include(o => o.DeliveryAddress)
                .Include(o => o.OrderDatails)
                    .ThenInclude(o => o.Product)
                .Include(o => o.User)
                .Where(o => o.OrderStatus.Name == "In progress")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllByUser(int userId)
        {
            return await dbContext.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDatails)
                    .ThenInclude(o => o.Product)    
                        .ThenInclude(o => o.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> GetOrderInfo(int orderId)
        {
            return await dbContext.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDatails)
                    .ThenInclude(o => o.Product)
                        .ThenInclude(o => o.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> Rejected(int Id)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.Id == Id);
            if (order == null)
            {
                return null;
            }

            order.OrderStatus = dbContext.OrderStatus.First(o => o.Id == 3);

            return order;
        }

        public Task<OrderResultModel> Status(int Id, PaymentEnum status)
        {
            throw new NotImplementedException();
        }

        public async Task Update(OrderModel orderModel, int userId)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderModel.Id);


            order.CashSum = orderModel.CashSum;
            order.Call = orderModel.Call;
            order.Comment = orderModel.Comment;
            order.DeliveryAddress.StreetHouse = orderModel.StreetHouse;
            order.DeliveryAddress.Flat = orderModel.Flat;
            order.DeliveryAddress.Floor = orderModel.Floor;
           
            order.NumberPerson = orderModel.NumberPerson;
            order.OrderDateOnly = orderModel.OrderDateOnly;
            order.OrderStatusId = 1;
            order.OrderTimeOnly = orderModel.OrderTimeOnly;
            order.Payment = orderModel.Payment;
            order.PaymentStatus = orderModel.PaymentStatus;
            order.PhoneNumber = orderModel.PhoneNumber;
            order.Recipient = orderModel.Recipient;
            order.UserId = userId;
            await dbContext.SaveChangesAsync();
        }

        
    }
}