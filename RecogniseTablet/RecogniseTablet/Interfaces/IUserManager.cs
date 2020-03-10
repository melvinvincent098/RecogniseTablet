using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface IUserManager
    {
        Task<bool> CheckUser(string username, string password);
    }
}
