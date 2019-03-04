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
    public interface IDiaryRepository:IRepository<Diary>
    {
        IQueryable<Diary> Diaries { get; set; }
    }
    class DiaryRepository: EFRepository<DiariesEntities, Diary>,IRepository<Diary>
    {
        public DiariesEntities diaries;

        public DiaryRepository(DiariesEntities context) : base(context)
        {
            diaries = context;
        }
        IQueryable<Diary> Diaries
        {
            get
            {
                return DbSet.Where(p => p.UserId > 2);
            }
        }
        protected override DbSet<Diary> DbSet
        {
            get
            {
                return diaries.Set<Diary>();
            }
        }

    }
}
