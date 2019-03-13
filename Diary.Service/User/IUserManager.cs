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
        IUserService GetService(int userid);
        // 核查登陆密码和用户名是否正确（即在UserRepository仓储类查找是否存在用户名和密码一致的实体）
        LoginUserModel Login(User user);
        //IDiaryManager GetDiaryManager(int userid);
      
    }
}
