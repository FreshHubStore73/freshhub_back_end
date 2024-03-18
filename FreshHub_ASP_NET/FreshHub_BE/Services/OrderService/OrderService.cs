using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Enums;
using FreshHub_BE.Models;
using System;
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
                CreatedDate = orderModel.CreateDate,
                OrderStatusId = 1,               
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

            //var cart = await dbContext.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

            cart.CartItems.RemoveRange(0, cart.CartItems.Count);
            await dbContext.SaveChangesAsync();

            return new OrderResultModel { Id = order.Id, Summ = order.OrderDatails.Sum(x => x.Price) };
        }

        public async Task<OrderResultModel> CreateWithOutCart(OrderWithOutCartModel orderWithOutCartModel, int userId)
        {
            Order order = new Order
            {
                CashSum = orderWithOutCartModel.CashSum,
                Call = orderWithOutCartModel.Call,
                Comment = orderWithOutCartModel.Comment,
                DeliveryAddress = new DeliveryAddress
                {
                    StreetHouse = orderWithOutCartModel.StreetHouse,
                    Flat = orderWithOutCartModel.Flat,
                    Floor = orderWithOutCartModel.Floor
                },
                NumberPerson = orderWithOutCartModel.NumberPerson,
                CreatedDate = orderWithOutCartModel.CreateDate,
                OrderStatusId = 1,
                Payment = orderWithOutCartModel.Payment,
                PaymentStatus = orderWithOutCartModel.PaymentStatus,
                PhoneNumber = orderWithOutCartModel.PhoneNumber,
                Recipient = orderWithOutCartModel.Recipient,
                UserId = userId
            };

            order.OrderDatails = orderWithOutCartModel.Items.Select(ci => new OrderDatail
            {
                Order = order,
                Quantity = ci.Quantity,
                ProductId = ci.ProductId,
                Price = ci.Price
            }).ToList();

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return new OrderResultModel
            {
                Id = order.Id,
                Summ = order.OrderDatails.Sum(x => x.Price)
            };
        }

        public async Task<List<Order>> GetAll() // for admin
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

        public async Task<OrderResultModel> GetOrderInfo(int orderId)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDatails)
                    .ThenInclude(o => o.Product)
                        .ThenInclude(o => o.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId);
            return new OrderResultModel
            {
                Id = orderId,
                Summ = order.OrderDatails.Sum(x => x.Product.Price * x.Quantity)
            };
        }

        public async Task Rejected(int Id)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.Id == Id);
            if (order == null)
            {
                throw new System.Exception("Order not excist.");
            }

            order.OrderStatus = dbContext.OrderStatus.First(o => o.Id == 3);

           
        }

        public async Task Status(int Id, PaymentEnum status)
        {
            var result = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            result.PaymentStatus = status == PaymentEnum.Cash;
            await dbContext.SaveChangesAsync();
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
            order.CreatedDate = orderModel.CreateDate;
            order.OrderStatusId = 1;            
            order.Payment = orderModel.Payment;
            order.PaymentStatus = orderModel.PaymentStatus;
            order.PhoneNumber = orderModel.PhoneNumber;
            order.Recipient = orderModel.Recipient;
            order.UserId = userId;
            await dbContext.SaveChangesAsync();
        }

    }
}
