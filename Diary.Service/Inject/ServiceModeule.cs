using Diary.Service.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.Inject
{
   public class ServiceModeule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IUserManager>().To<UserManager>();
            Bind<IUserService>().To<UserService>();
            Bind<IDiaryManager>().To<DiaryManager>();
            Bind<IDiaryService>().To<DiaryService>();
        }
    }
}
