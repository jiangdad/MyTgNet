using Diary.Service.DiaryComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet.Api;

namespace Diary.Service.UserDiary
{
    class UserComment : IUserComment
    {
           IDiaryCommentManager _IDiaryCommentManager;
            Lazy<IDiaryCommentService> _lazyIDiaryCommentService;
        int _diarycommentid;
        public UserComment(int diarycommentid,int userid, IDiaryCommentManager IDiaryCommentManager)
        {
         
            _IDiaryCommentManager = IDiaryCommentManager;
            _diarycommentid = diarycommentid;
              _lazyIDiaryCommentService = new Lazy<IDiaryCommentService>(() =>
              {
                  var m = _IDiaryCommentManager.GetDiaryCommentService(diarycommentid);
                  if (m.UserId != userid)
                  {
                      throw new ExceptionWithErrorCode(ErrorCode.没有操作权限, "没有权限操作该留言");
                  }
                  return m;
              }
            );
        }
        void IUserComment.DeleteComment()
        {
            _lazyIDiaryCommentService.Value.Delete();
        }
    }
}
