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
    public interface IDiaryRepository : IRepository<Diary>
    {
        IQueryable<Diary> EnableDiary { get; }

        IQueryable<Diary> NoTackingDiary { get; }
    }
    public class DiaryRepository : EFRepository<DiariesEntities, Diary>, IDiaryRepository
    {
        private DiariesEntities _DbContext;
        public DiaryRepository(DiariesEntities context):base(context)
        {
            _DbContext = context;
        }
        public IQueryable<Diary> EnableDiary
        {
            get
            {
                return DbSet.Where(l => !l.isDel);
            }
        }
       public IQueryable<Diary> NoTackingDiary
        {
            get
            {
                return EnableDiary.AsNoTracking();
            }
        }
        protected override DbSet<Diary> DbSet
        {
            get
            {
                return _DbContext.Set<Diary>();
            }
        }
    }
}

