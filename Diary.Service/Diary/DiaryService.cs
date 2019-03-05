using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;
using Tgnet;
using Tgnet.Api;

namespace Diary.Service.Diary
{
  public  class DiaryService : IDiaryService
    {
        private int diaryId;
        private IDiaryRepository _diarepository;
        private Lazy<Data.Diary> _LazyDiary;
        public DiaryService(int diaryId, IDiaryRepository diarepository)
        {
            ExceptionHelper.ThrowIfNotId(diaryId, "diaryId");//用来报错的；
            this.diaryId = diaryId;
            _diarepository = diarepository;
            //懒加载的Diary获取
            _LazyDiary = new Lazy<Data.Diary>(() =>
            {
                var diary = _diarepository.EnableDiary.Where(m => m.DiaryId == diaryId).FirstOrDefault();
                if (diary == null)
                    throw new ExceptionWithErrorCode(ErrorCode.没有找到对应条目, "日志不存在");//代码还会往下执行吗
                return diary;
            });
            }

        public DateTime CreateTime
        {
            get
            {
                return _LazyDiary.Value.CreateTime.GetValueOrDefault();
            }
        }
        public int DiaryId
        {
            get
            {
                return diaryId;
            }
        }
        public int UserId
        {
            get
            {
                return _LazyDiary.Value.UserId;
            }
        }
        public string UserName
        {
            get
            {
                return _LazyDiary.Value.User.UserName;
            }
        }
        public string Content
        {
            get
            {
                return _LazyDiary.Value.Content;
            }
        }
        public string Title
        {
            get
            {
                return _LazyDiary.Value.Title;
            }
        }
      

        void IDiaryService.Delete(int userId)
        {
            throw new NotImplementedException();
        }

        void IDiaryService.UpdateDiary(int userId, string Content)
        {
           
        }
    }
}
