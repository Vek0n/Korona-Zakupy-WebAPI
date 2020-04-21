using AutoMapper;
using KoronaZakupy.Entities;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoronaZakupy.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<PlaceOrderModel, Order>()
                .ForMember(dest => dest.OrderDate,
                opt => opt.MapFrom(src => DateTime.Now) );
                     
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.UsersId,
                opt => opt.MapFrom(src => src.Users.Select(uo => uo.UserId).ToList()));

            CreateMap<CompleteOrderDTO, OrderModel>();
           
        }
    }


    
}
