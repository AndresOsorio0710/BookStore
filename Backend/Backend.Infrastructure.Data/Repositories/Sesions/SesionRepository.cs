using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Contexts;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Interfaces;

namespace Backend.Infrastructure.Data.Repositories.Sesions
{
    public class SesionRepository : IRepositoryConnect<Sesion, Guid>
    {
        private BookAppContext db;

        public SesionRepository(BookAppContext db)
        {
            this.db = db;
        }

        private Sesion Get(Sesion sesion)
        {
            return this.db.Sesions.Where(s => s.UserId == sesion.UserId).FirstOrDefault();
        }

        private Sesion GetById(Guid id)
        {
            return this.db.Sesions.Where(s => s.Id == id).FirstOrDefault();
        }

        public Sesion Get(Guid entityID)
        {
            return this.db.Sesions.Where(s => s.Id == entityID).FirstOrDefault();
        }

        public Sesion LogIn(Sesion entity)
        {
            Sesion sesion = this.Get(entity);
            if (sesion != null)
            {
                this.db.Remove(sesion);
            }
            Sesion newSesion = new Sesion();
            newSesion.Id = Guid.NewGuid();
            newSesion.Role = entity.Role;
            newSesion.UserId = entity.UserId;
            this.db.Add(newSesion);
            return newSesion;
        }

        public void LogOut(Guid entityID)
        {
            Sesion sesion = this.GetById(entityID);
            if (sesion != null)
            {
                this.db.Remove(sesion);
            }
        }

        public void SaveAllChanges()
        {
            this.db.SaveChanges();
        }
    }
}
