using FreshHub_BE.Data.Entities;
using FreshHub_BE.Enums;
using FreshHub_BE.Models;

namespace FreshHub_BE.Services.OrderService
{
    public interface IOrderService
    {
        public Task<OrderResultModel> Create(OrderModel orderModel, int userId);
        public Task<OrderResultModel> CreateWithOutCart(OrderWithOutCartModel orderWithOutCartModel, int userId);
        public Task Update(OrderModel orderModel, int userId);
        public Task Rejected(int Id);
        public Task Status(int Id, PaymentEnum status);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetAllByUser(int userId);
        public Task<OrderResultModel> GetOrderInfo(int orderId);


    }
}
