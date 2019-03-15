using Diary.Data;
using Diary.Service.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service
{
   public class DiaryManager:IDiaryManager
    {
        //
        public IDiaryRepository _diarepository;
        public IDiCommentRepository _dicommentrepository;
        public IUserManager _userManager;
        public DiaryManager(IDiaryRepository diaryRepository,IDiCommentRepository dicommentRepository,IUserManager userManager)
        {
            _diarepository = diaryRepository;
            _dicommentrepository = dicommentRepository;
            _userManager = userManager;

        }
        public IQueryable<Data.Diary> MessageDiary
        {
            get
            {
                return _diarepository.EnableDiary;
            }
        }

        public IQueryable<Data.Diary> NoTackingDiary
        {
            get
            {
                return _diarepository.NoTackingDiary;
            }
        }

        public IDiaryService Add(int userid, string title, bool isPrivate, string content)
        {
            Data.Diary diary = new Data.Diary
            {
                CreateTime = DateTime.Now,
                Content = content,
                IsPrivate = isPrivate,
                Title = title,
                IsDel = false,
                UserId = userid
            };
            _diarepository.Add(diary);
            _diarepository.SaveChanges();
            return GetDiaryService(diary.DiaryId);
        }

        public IDiaryService GetDiaryService(int DiaryId)
        {
            return new DiaryService(DiaryId, _diarepository, _dicommentrepository);
        }
        public IUserDiaryService GetUserDiaryService(int DiaryId,int UserId)
        {
            return new UserDiaryService(UserId, DiaryId, this,_userManager);
        }

    }
}
