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

        public CompleteUserInfo(UserManager<Entities.UserDb.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrderWithUsersInfo>> CompleteAsync(IEnumerable<OrderWithUsersId> orders)
        {
            var results = new List<OrderWithUsersInfo>();

            foreach (var order in orders)
            {
                var result = new OrderWithUsersInfo()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    Products = order.Products,
                    IsActive = order.IsActive,
                    IsFinished = order.IsFinished,
                    UsersInfo = new List<UserInfo>()
                };

                foreach (var userId in order.UsersId)
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    result.UsersInfo.Add(new UserInfo()
                    {
                        UserId = userId,
                        Name = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Rating = user.Rating,
                        PhotoDirectory = user.PhotoDirectory,
                        Address = user.Address
                    });
                }
                results.Add(result);
            }
           
            return results;
        }
    }
}
