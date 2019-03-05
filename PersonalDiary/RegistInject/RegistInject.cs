using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diary.Service.Inject;
using Tgnet.Web.Mvc.Inject;

namespace PersonalDiary.RegistInject
{
    public class RegistInject
    {
        public static void registInject()
        {
            var modules = new Ninject.Modules.NinjectModule[] {
                new Diary.Service.Inject.DataModeule(),
            new Diary.Service.Inject.ServiceModeule()
            
            };

            var ninjectDependencyResolver = new NinjectDependencyResolver(modules);
            //DependencyResolver.SetResolver(ninjectDependencyResolver);
            System.Web.Mvc.DependencyResolver.SetResolver(ninjectDependencyResolver);//

        }
    }
}