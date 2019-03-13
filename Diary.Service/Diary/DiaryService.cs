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
        public int _diaryId;
        private IDiCommentRepository _dicommentRepository;
        private IDiaryRepository _diarepository;
        private Lazy<Data.Diary> _LazyDiary;
        public DiaryService(int diaryId, IDiaryRepository diarepository, IDiCommentRepository dicommentRepository)
        {
            ExceptionHelper.ThrowIfNotId(diaryId, "diaryId");//用来报错的；
            _diaryId = diaryId;
            _dicommentRepository = dicommentRepository;
            _diarepository = diarepository;
            //懒加载的Diary获取
            //设置懒加载，设置Diary类diary对象的搜索
            //我们创建某一个对象需要很大的消耗，而这个对象在运行过程中又不一定用到，
            //为了避免每次运行都创建该对象，这时候延迟初始化（也叫延迟实例化）就出场了。
            _LazyDiary = new Lazy<Data.Diary>(() =>
            {
                var diary = _diarepository.EnableDiary.Where(m => m.DiaryId == _diaryId).FirstOrDefault();
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
                return _diaryId;
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
        public bool IsPrivate
        {
            get
            {
                return _LazyDiary.Value.IsPrivate;
            }
        }
        //用来查询当前日志的所有评论
        public IQueryable<Data.DiaryComment> DiaryComment
        {
            get
            {
                return _dicommentRepository.EnableDiaryComment.Where(c => c.DiaryId == _diaryId);
            }
        }
        void IDiaryService.Delete()
        {
            if (!_LazyDiary.Value.IsDel)
            {
            _LazyDiary.Value.IsDel = true;
                //删除日志就删除所有评论
            foreach(var item in DiaryComment.AsEnumerable())
                {
                    item.IsDel = true;
                }
            _diarepository.SaveChanges();
            _dicommentRepository.SaveChanges();
            }
          
        }
        void IDiaryService.Publish()
        {

            if (_LazyDiary.Value.IsPrivate)
            {
                _LazyDiary.Value.IsPrivate = false;
                _diarepository.SaveChanges();
                _dicommentRepository.SaveChanges();
            }

        }




        void IDiaryService.UpdateDiary(string content, string title, bool isPrivate)
        {
            ExceptionHelper.ThrowIfNullOrEmpty(content, "内容", "内容为空");
            ExceptionHelper.ThrowIfNullOrEmpty(title, "标题", "标题为空");
             _LazyDiary.Value.Title = title;
              _LazyDiary.Value.Content = content;
              _LazyDiary.Value.IsPrivate = isPrivate;
            _diarepository.SaveChanges();
        }
        void IDiaryService.AddComment(string diaryComment,int uid)
        {
            ExceptionHelper.ThrowIfNull(diaryComment, "评论", "评论无效");
            _dicommentRepository.Add(new Data.DiaryComment() {
                CContent=diaryComment,
                CreateTime=DateTime.Now,
                DiaryId=this.DiaryId,
                IsDel=false,
                UserId=uid,
            });
            _dicommentRepository.SaveChanges();
        }
    }
}
