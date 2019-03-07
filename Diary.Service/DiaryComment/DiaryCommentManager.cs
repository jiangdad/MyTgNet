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

        IDiaryCommentService IDiaryCommentManager.Add(Data.DiaryComment diarycomment)
        {
            _DiCommentRepository.Add(diarycomment);
            _DiCommentRepository.SaveChanges();
            return GetDiaryCommentService(diarycomment.CommentId);
        }

           public IDiaryCommentService GetDiaryCommentService(int CommentId)
        {
            return new DiaryCommentService(CommentId,_DiCommentRepository);
        }
    }
}
