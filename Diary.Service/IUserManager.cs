using Diary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service
{
    public interface IUserManager
    {
       IUserService add(User user);
        bool CheckUserName(string user);
    }
}
