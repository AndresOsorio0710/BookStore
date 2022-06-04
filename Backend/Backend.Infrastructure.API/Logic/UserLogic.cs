using Backend.Aplications.Services.Users;
using Backend.Domain.Models;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.Users;
using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.API.Logic
{
    public class UserLogic
    {
        private static SesionLogic sesionLogic = new SesionLogic();

        private UserService CreateUserService()
        {
            BookAppContext db = new BookAppContext();
            UserRepository userRepository = new UserRepository(db);
            UserService userService = new UserService(userRepository);
            return userService;
        }

        public List<User> Get(Guid sesionId)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                {
                    var service = this.CreateUserService();
                    return service.GetAll();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User Get(Guid sesionId, Guid userId)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN") || sesionLogic.ValidateOwner(sesionId, userId))
                {
                    var service = this.CreateUserService();
                    return service.Get(userId);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
