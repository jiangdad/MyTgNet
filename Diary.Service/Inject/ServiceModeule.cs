using Diary.Service.Diary;
using Diary.Service.DiaryComment;
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
            Bind<IDiaryManager>().To<DiaryManager>();
            Bind<IDiaryCommentManager>().To<DiaryCommentManager>();
        }
    }
}
