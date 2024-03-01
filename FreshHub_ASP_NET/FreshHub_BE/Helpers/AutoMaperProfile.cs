using AutoMapper;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;

namespace FreshHub_BE.Helpers
{
    public class AutoMaperProfile : Profile
    {
        public AutoMaperProfile()
        {
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItemModel, CartItem>();
            CreateMap<CartItem, CartItemResultModel>();               

        }
    }
}
