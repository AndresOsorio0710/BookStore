using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Contexts;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories.Users
{
    public class UserRepository : IRepositoryUser<User, Guid>
    {
        private BookAppContext db;

        public UserRepository(BookAppContext db)
        {
            this.db = db;
        }

        private User Get(User user)
        {
            return this.db.Users.Where(u => u.Email == user.Email && u.PeopleId == user.PeopleId).FirstOrDefault();
        }

        private User GetById(Guid id)
        {
            return this.db.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        private string GetMD5(string contrasena)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder password = new StringBuilder();
            stream = md5.ComputeHash(aSCIIEncoding.GetBytes(contrasena));
            for (int i = 0; i < stream.Length; i++) password.AppendFormat("{0:x2}", stream[i]);
            return password.ToString();
        }

        public User Add(User entity)
        {
            if (this.Get(entity) == null)
            {
                User user = new User();
                user.Id = Guid.NewGuid();
                user.Name = entity.Name.Trim().ToUpper();
                user.Password = this.GetMD5(entity.Password.Trim());
                user.PeopleId= entity.PeopleId;
                this.db.Add(user);
                return user;
            }
            return null;
        }

        public void Delete(Guid entityID)
        {
            User user = this.GetById(entityID);
            if (user != null)
            {
                user.DeletedAt = DateTime.Now;
                this.db.Entry(user).State =  Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public void Edit(User entity)
        {
            User user = this.GetById(entity.Id);
            if (user != null)
            {
                user.Name = entity.Name.Trim().ToUpper();
                user.Email = entity.Email.Trim();
                user.Role = entity.Role.Trim().ToUpper();
                user.Password = this.GetMD5(entity.Password.Trim());
                user.UpdatedAt = DateTime.Now;
                this.db.Entry(user).State =  Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public User Get(Guid entityID)
        {
            return this.db.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = "",
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt,
                PeopleId = u.PeopleId
            }).Where(u => u.Id == entityID && u.CreatedAt == u.DeletedAt).FirstOrDefault();
        }

        public async Task<object> Get(int page, int rows)
        {
            int totalRows = await this.db.Users.Where(u => u.CreatedAt == u.DeletedAt).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var users = await this.db.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = "",
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt,
                PeopleId = u.PeopleId
            }).Where(u => u.CreatedAt == u.DeletedAt).OrderBy(u => u.Name).Skip((page - 1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, users = users };
            return result;
        }

        public async Task<object> Get(int page, int rows, string search)
        {
            int totalRows = await this.db.Users.Where(u => u.CreatedAt == u.DeletedAt && (u.Name.Contains(search) || u.Email.Contains(search) || u.Role.Contains(search))).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var users = await this.db.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = "",
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt,
                PeopleId = u.PeopleId
            }).Where(u => u.CreatedAt == u.DeletedAt  && (u.Name.Contains(search) || u.Email.Contains(search) || u.Role.Contains(search))).OrderBy(u => u.Name).Skip((page - 1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, users = users };
            return result;
        }

        public User Get(string user, string password)
        {
            return this.db.Users.Where(u => u.CreatedAt == u.DeletedAt &&  u.Password.Equals(this.GetMD5(password)) && (u.Name.Equals(user) || u.Email.Equals(user))).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return this.db.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = "",
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt,
                PeopleId = u.PeopleId
            }).Where(u => u.CreatedAt == u.DeletedAt).ToList();
        }

        public void SaveAllChanges()
        {
            this.db.SaveChanges();
        }
    }
}
