using AutoMapper;
using KoronaZakupy.Entities;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services
{
    public class CompleteUserInfo : ICompleteUserInfo
    {
        private readonly UserManager<Entities.UserDb.User> _userManager;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;

        public CompleteUserInfo(UserManager<Entities.UserDb.User> userManager,
            IMapper mapper,IRatingService ratingService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _ratingService = ratingService;
        }

        public async Task<IEnumerable<CompleteOrderDTO>> CompleteAsync(IEnumerable<OrderDTO> orders)
        {
            var results = new List<CompleteOrderDTO>();

            foreach (var order in orders)
            {
                var result = new CompleteOrderDTO()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    Products = order.Products,
                    OrderStatus = order.OrderStatus,
                    UsersInfo = new List<UserDTO>()
                };

                foreach (var userId in order.UsersId)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    user.UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    user.Rating = _ratingService.GetUserRating(user.Id).Result;
                    result.UsersInfo.Add(_mapper.Map<UserDTO>(user));
                }
                results.Add(result);
            }
            return results;
        }
    }
}
