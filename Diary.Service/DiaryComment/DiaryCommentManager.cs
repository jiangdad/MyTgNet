using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;

namespace Diary.Service.DiaryComment
{
    class DiaryCommentManager : IDiaryCommentManager
    {
        IQueryable<Data.DiaryComment> IDiaryCommentManager.MessageDiaryComment => throw new NotImplementedException();

        IQueryable<Data.DiaryComment> IDiaryCommentManager.NoTackingDiaryComment => throw new NotImplementedException();

        IDiaryCommentService IDiaryCommentManager.Add(Data.DiaryComment diarycomment)
        {
            throw new NotImplementedException();
        }

        IDiaryCommentService IDiaryCommentManager.GetDiaryCommentService(int CommentId)
        {
            throw new NotImplementedException();
        }
    }
}
