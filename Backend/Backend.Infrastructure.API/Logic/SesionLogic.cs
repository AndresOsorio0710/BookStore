using Microsoft.AspNetCore.Mvc;
using Backend.Aplications.Services.Sesions;
using Backend.Aplications.Services.Users;
using Backend.Domain.Models;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.Sesions;
using Backend.Infrastructure.Data.Repositories.Users;
using System;

namespace Backend.Infrastructure.API.Logic
{
    public class SesionLogic
    {
        private SesionService CreateSesionService()
        {
            BookAppContext db = new BookAppContext();
            SesionRepository sesionRepository = new SesionRepository(db);
            SesionService sesionService = new SesionService(sesionRepository);
            return sesionService;
        }
        private UserService CreateUserService()
        {
            BookAppContext db = new BookAppContext();
            UserRepository userRepository = new UserRepository(db);
            UserService userService = new UserService(userRepository);
            return userService;
        }

        public bool inSession(Guid id)
        {
            var sesionService = CreateSesionService();
            if (sesionService.Get(id)==null)
            {
                return false;
            }
            return true;
        }

        public ActionResult<Sesion> LogIn(string user, string password)
        {
            var userService = CreateUserService();
            User userSelect = userService.Get(user, password);
            if (userSelect != null)
            {
                var sesionService = CreateSesionService();
                Sesion sesion = new Sesion();
                sesion.Role = userSelect.Role;
                sesion.UserId = userSelect.Id;
                var newSesion = sesionService.LogIn(sesion);
                if (newSesion != null)
                {
                    return newSesion;
                }
            }
            return null;
        }

        public void LogOut(Guid id)
        {
            var sesionService = CreateSesionService();
            sesionService.LogOut(id);
        }

        public bool ValidatePermission(Guid sesionId, string permission)
        {
            var sesionService = CreateSesionService();
            Sesion sesion = sesionService.Get(sesionId);
            if (sesion != null && sesion.Role.Equals(permission))
            {
                return true;
            }
            return false;
        }

        public bool ValidateOwner(Guid sesionId, Guid id)
        {
            var sesionService = CreateSesionService();
            Sesion sesion = sesionService.Get(sesionId);
            if (sesion != null && sesion.UserId==id)
            {
                return true;
            }
            return false;
        }
    }
}
