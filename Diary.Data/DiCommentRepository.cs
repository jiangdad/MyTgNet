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
//        却总能只提供你所需要的数据.大大减少了数据的传输
//IQueryable的优势是它有表达式树，所有对于IQueryable的过滤，排序等操作，都
//            会先缓存到表达式树中，只有当真正遍历发生的时候，才会将表达式树由
//            IQueryProvider执行获取数据操作。
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

