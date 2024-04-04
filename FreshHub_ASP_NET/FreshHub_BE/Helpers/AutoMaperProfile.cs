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

            CreateMap<User, UserWithRoleModels>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));

            CreateMap<Product, ProductResultModel>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => ImagePathCreator.CreatePath(src.PhotoUrl)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductCreateModel, Product>();

            CreateMap<UserRegistrationModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.PhoneNumber));


            CreateMap<CartItemModel, CartItem>();
            CreateMap<CartItem, CartItemResultModel>();
            CreateMap<User, UserInfoModel>();

        }
    }
}
