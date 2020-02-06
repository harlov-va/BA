using System;
using System.Collections.Generic;

namespace BA.DAL
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int DepartmentId { get; set; }
    }
}
