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
        public int diaryId;
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
                    throw new ExceptionWithErrorCode(ErrorCode.没有找到对应条目, "日志不存在");
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
      

        void IDiaryService.Delete(int diaryId)
        {
            if (!_LazyDiary.Value.IsDel == true)
            {
            _LazyDiary.Value.IsDel = true;
            _diarepository.SaveChanges();
            }
          
        }

        void IDiaryService.UpdateDiary(int diaryId, string content,string title)
        {
            //  _LazyDiary.Value.Content = content;可以改变Diary仓储类里面的值
             if(content!=Content||title!=Title)
            {
                _LazyDiary.Value.Title = title;
                _LazyDiary.Value.Content = content;
                _diarepository.SaveChanges();
            }
        }
    }
}
