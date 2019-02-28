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
        DiariesEntities diaries;
        public UserRepository(DiariesEntities context):base(context)
        {

        }
        protected override DbSet<User> DbSet { get; }
    }
}
