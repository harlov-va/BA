using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.DAL
{
    public interface IRepository
    {
        #region System
        Task Save();
        #endregion
        #region User
        IQueryable<User> GetUsers();
        Task<User> GetUser(int ID);
        Task<int> SaveUser(User element, bool withSave = true);
        Task<bool> DeleteUser(int ID);
        #endregion
        #region Department
        IQueryable<Department> GetDepartments();
        Task<Department> GetDepartment(int ID);
        Task<int> SaveDepartment(Department element, bool withSave = true);
        Task<bool> DeleteDepartment(int ID);
        #endregion
       
    }
}
