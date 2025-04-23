// MappingProfiles/OrderProfile.cs
using AutoMapper;
using OrderManagement.Models;
using OrderManagement.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderManagement.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.TotalPrice));
        }
    }
}