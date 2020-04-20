using AutoMapper;
using KoronaZakupy.Entities;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;

namespace KoronaZakupy.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<PlaceOrderModel, Order>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
