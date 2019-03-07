using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;
using Tgnet.Api;

namespace Diary.Service.DiaryComment
{
    class DiaryCommentService : IDiaryCommentService
    {
        private int _commentId;
        private IDiCommentRepository _DiCommentRepository;
        private Lazy<Data.DiaryComment> _LazyDiary;
        public DiaryCommentService(int commentId, IDiCommentRepository DiCommentRepository)
        {
            _commentId = commentId;
            _DiCommentRepository = DiCommentRepository;
           
            //懒加载的Diary获取
            _LazyDiary = new Lazy<Data.DiaryComment>(() =>
            {
                var diarycomment = DiCommentRepository.EnableDiaryComment.Where(m => m.CommentId == commentId).FirstOrDefault();
                if (diarycomment == null)
                    throw new ExceptionWithErrorCode(ErrorCode.没有找到对应条目, "日志不存在");
                return diarycomment;
            });
        }



        int IDiaryCommentService.DiaryCommentId
        {
            get
            {
                return _commentId;
            }
        }
        int IDiaryCommentService.UserId
        {
            get
            {
                return _LazyDiary.Value.UserId;
            }
        }

        string IDiaryCommentService.UserName
        {
            get
            {
                return _LazyDiary.Value.User.UserName;
            }
        }

        string IDiaryCommentService.CContent
        {
            get
            {
                return _LazyDiary.Value.CContent;
            }
        }

        DateTime IDiaryCommentService.CreateTime
        {
            get
            {
                return _LazyDiary.Value.CreateTime;
            }
        }
        void IDiaryCommentService.Delete(int diarycommentid)
        {
            throw new NotImplementedException();
        }
    }
}
