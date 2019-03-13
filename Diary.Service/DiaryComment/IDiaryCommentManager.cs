using Diary.Service.UserDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.DiaryComment
{
    public interface IDiaryCommentManager
    {

        IQueryable<Data.DiaryComment> MessageDiaryComment { get; }

        IQueryable<Data.DiaryComment> NoTackingDiaryComment { get; }

        IDiaryCommentService GetDiaryCommentService(int CommentId);
        IUserComment GetUserCommentService(int commentid, int userid);
    }
}
