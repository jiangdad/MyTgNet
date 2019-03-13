using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;
using Tgnet;
using Tgnet.Api;

namespace Diary.Service.DiaryComment
{
    class DiaryCommentService : IDiaryCommentService
    {
        private int _commentId;
        private IDiCommentRepository _DiCommentRepository;
        private Lazy<Data.DiaryComment> _LazyDiaryComment;
        public DiaryCommentService(int commentId, IDiCommentRepository DiCommentRepository)
        {
            ExceptionHelper.ThrowIfNotId(commentId, "messageCommentId");
            _commentId = commentId;
            _DiCommentRepository = DiCommentRepository;

            //懒加载的Diary获取
            _LazyDiaryComment = new Lazy<Data.DiaryComment>(() =>
            {
                var diarycomment = DiCommentRepository.EnableDiaryComment.Where(m => m.CommentId == commentId).FirstOrDefault();
                if (diarycomment == null)
                    throw new ExceptionWithErrorCode(ErrorCode.没有找到对应条目, "日志评论不存在");
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
                return _LazyDiaryComment.Value.UserId;
            }
        }

        string IDiaryCommentService.UserName
        {
            get
            {
                return _LazyDiaryComment.Value.User.UserName;
            }
        }

        string IDiaryCommentService.CContent
        {
            get
            {
                return _LazyDiaryComment.Value.CContent;
            }
        }

        DateTime IDiaryCommentService.CreateTime
        {
            get
            {
                return _LazyDiaryComment.Value.CreateTime;
            }
        }
        void IDiaryCommentService.Delete()
        {
            
           if(!_LazyDiaryComment.Value.IsDel)
            {
                _LazyDiaryComment.Value.IsDel = true;
                _DiCommentRepository.SaveChanges();
            }
        }
    }
}
