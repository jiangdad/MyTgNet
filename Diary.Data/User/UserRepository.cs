using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet.Data;
using Tgnet.Data.Entity;
namespace Diary.Data
{
    public interface IUserRepository:IRepository<User>
    {

    }
    public class UserRepository:EFRepository<DiariesEntities,User>,IUserRepository
    {
       public DiariesEntities diaries;
        public UserRepository(DiariesEntities context):base(context)
        {
            diaries = context;
        }
        protected override DbSet<User> DbSet {
            get
            {
                return diaries.Set<User>();
            }
             }

    }
}
