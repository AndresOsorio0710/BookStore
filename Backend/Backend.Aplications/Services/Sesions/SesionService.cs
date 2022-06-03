using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Aplications.Interfaces;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;

namespace Backend.Aplications.Services.Sesions
{
    public class SesionService : IServiceConnect<Sesion, Guid>
    {
        private readonly IRepositoryConnect<Sesion, Guid> repositoryConnect;

        public SesionService(IRepositoryConnect<Sesion, Guid> repositoryConnect)
        {
            this.repositoryConnect = repositoryConnect;
        }

        public Sesion Get(Guid entityID)
        {
            return this.repositoryConnect.Get(entityID);
        }

        public Sesion LogIn(Sesion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("The Sesion is required.");
            }
            Sesion sesion = this.repositoryConnect.LogIn(entity);
            if (sesion != null)
            {
                this.repositoryConnect.SaveAllChanges();
            }
            return sesion;

        }

        public void LogOut(Guid entityID)
        {
            this.repositoryConnect.LogOut(entityID);
            this.repositoryConnect.SaveAllChanges();
        }
    }
}
