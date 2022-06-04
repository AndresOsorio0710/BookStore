using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Models;
using System;

namespace Backend.Infrastructure.API.Logic
{
    public class Connectlogic
    {
        private static SesionLogic sesionLogic = new SesionLogic();
        public string Get(Guid id)
        {
            if (sesionLogic.inSession(id))
            {
                return "Ok.";
            }
            return "No connect.";
        }

        public ActionResult<Sesion> LogIn(Connect connect)
        {
            if (connect == null)
            {
                return null;
            }
            var sesion = sesionLogic.LogIn(connect.user, connect.password);
            return sesion;
        }

        public String LogOut(Guid id)
        {
            sesionLogic = new SesionLogic();
            sesionLogic.LogOut(id);
            return "Successful transaction.";
        }
    }
}
