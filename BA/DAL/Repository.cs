using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BA.DAL
{
    public class Repository : IRepository
    {
        #region System
        private BAContext _db;
        public BAContext db
        {
            get
            {
                if (_db == null)
                    _db = new BAContext();
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        private bool _disposed = false;
        public Repository(BAContext db)
        {
            if (db == null) this.db = new BAContext();
        }
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //           if (db != null) Dispose(true);
        //        }
        //        db = null;
        //        _disposed = true;
        //    }
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
        #endregion
        #region User
        public IQueryable<User> GetUsers()
        {
            var res = db.User;
            return res;
        }
        public async Task<User> GetUser(int ID)
        {
            var res = await db.User.FirstOrDefaultAsync(x => x.Id == ID);
            return res;
        }
        public async Task<int> SaveUser(User element, bool withSave = true)
        {
            if (element.Id == 0)
            {
                db.User.Add(element);
                if (withSave) await Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) await Save();
            }
            return element.Id;
        }
        public async Task<bool> DeleteUser(int ID)
        {
            bool res = false;
            var item = await db.User.SingleOrDefaultAsync(x => x.Id == ID);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                await Save();
                res = true;
            }
            return res;
        }
        #endregion
        #region Department
        public IQueryable<Department> GetDepartments()
        {
            var res = db.Department;
            return res;
        }
        public async Task<Department> GetDepartment(int ID)
        {
            var res = await db.Department.FirstOrDefaultAsync(x => x.Id == ID);
            return res;
        }
        public async Task<int> SaveDepartment(Department element, bool withSave = true)
        {
            if (element.Id == 0)
            {
                db.Department.Add(element);
                if (withSave) await Save();
            }
            else
            {
                db.Entry(element).State = EntityState.Modified;
                if (withSave) await Save();
            }
            return element.Id;
        }
        public async Task<bool> DeleteDepartment(int ID)
        {
            bool res = false;
            var item = db.Department.SingleOrDefault(x => x.Id == ID);
            if (item != null)
            {
                db.Entry(item).State = EntityState.Deleted;
                await Save();
                res = true;
            }
            return res;
        }
        #endregion
       
    }
}