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
    public interface IDiCommentRepository:IRepository<DiaryComment>
    {
        IQueryable<DiaryComment> EnableDiaryComment { get; }
        IQueryable<DiaryComment> NoTackingDiaryComment { get; }
    }
   public class DiCommentRepository: EFRepository<DiariesEntities, DiaryComment>, IDiCommentRepository
    {

        private DiariesEntities _DbContext;
        public DiCommentRepository(DiariesEntities context):base(context)
        {
            _DbContext = context;
        }


        public IQueryable<DiaryComment> EnableDiaryComment
        {
            get
            {
                return DbSet.Where(l => !l.IsDel);
            }
        }
        public IQueryable<DiaryComment> NoTackingDiaryComment
        {
            get
            {
                return EnableDiaryComment.AsNoTracking();
            }
        }
        protected override DbSet<DiaryComment> DbSet
        {
            get
            {
                return _DbContext.Set<DiaryComment>();
            }
        }
    }
}

