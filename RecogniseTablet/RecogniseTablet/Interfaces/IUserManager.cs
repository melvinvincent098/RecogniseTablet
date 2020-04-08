using RecogniseTablet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface IUserManager
    {
        Task<UserModel> CheckUser(string username, string password);

        Task<int> CheckUserIDPersonGroupID(int UserID);

        Task InsertUserIDPersonGroupID(int UserID, int PersonGroupID);
    }
}
