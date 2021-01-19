using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Permission
    {
        ManagementPermission,
        WithoutManagementPermission
    }
    public class UserBo
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int PasswordOnlyForNumbers { get; set; }

        public Permission Permission1 { get; set; }

        public bool ManagementPermission { get; set; }
        public bool ChackDelete { get; set; }
        //public override string ToString()
        //{
        //    return ToStringProperty();
        //}

        //private string ToStringProperty()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
