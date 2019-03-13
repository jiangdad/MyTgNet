using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.DiaryComment
{
   public interface IDiaryCommentService
    {
        int DiaryCommentId { get; }
        int UserId { get; }
        string UserName { get; }
        string CContent { get; }
        DateTime CreateTime { get; }
        void Delete();
    }
}
