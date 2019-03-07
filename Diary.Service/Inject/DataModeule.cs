using Diary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.Inject
{
 public class DataModeule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();

            Bind<DiariesEntities>().ToSelf();
            Bind<IDiaryRepository>().To<DiaryRepository>();
            Bind<IDiCommentRepository>().To<DiCommentRepository>();
        }
    }
}
