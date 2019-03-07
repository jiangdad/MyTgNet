using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.DiaryComment
{
    class DiaryCommentService : IDiaryCommentService
    {
        int IDiaryCommentService.DiaryCommentId => throw new NotImplementedException();

        int IDiaryCommentService.UserId => throw new NotImplementedException();

        string IDiaryCommentService.UserName => throw new NotImplementedException();

        string IDiaryCommentService.CContent => throw new NotImplementedException();

        DateTime IDiaryCommentService.CreateTime => throw new NotImplementedException();

        void IDiaryCommentService.Delete(int diarycommentid)
        {
            throw new NotImplementedException();
        }
    }
}
