using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;
using Tgnet;
using Diary.Service.UserDiary;

namespace Diary.Service.DiaryComment
{
    class DiaryCommentManager : IDiaryCommentManager
    {
        IDiCommentRepository _DiCommentRepository;
        public DiaryCommentManager(IDiCommentRepository dicommentrepository)
        {
            _DiCommentRepository = dicommentrepository;
        }
            
        IQueryable<Data.DiaryComment> IDiaryCommentManager.MessageDiaryComment
        {
            get
            {
                return _DiCommentRepository.EnableDiaryComment;
            }
        }

        IQueryable<Data.DiaryComment> IDiaryCommentManager.NoTackingDiaryComment
        {
            get
            {
                return _DiCommentRepository.NoTackingDiaryComment;
            }
        }


           public IDiaryCommentService GetDiaryCommentService(int CommentId)
        {
            ExceptionHelper.ThrowIfNotId(CommentId, nameof(CommentId), "CommentId参数报错");
            return new DiaryCommentService(CommentId,_DiCommentRepository);
        }
        public IUserComment GetUserCommentService(int commentid,int userid)
        {
            return new UserComment(commentid, userid,this);
        }
    }
}
