using Diary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet.Api;

namespace Diary.Service.Diary
{
    class UserDiaryService : IUserDiaryService
    {

        long _userid;
        int _diaryid;
        IDiaryManager _diaryManager;
        IUserManager _usermanager;
        Lazy<IUserService> user;
        Lazy<IDiaryService> _Dairy;
        public UserDiaryService(int userid, int diaryId, IDiaryManager diaryManager, IUserManager usermanager)
        {
            //userid是当前用户登陆ID
            //获得这条diaryid的用户id
            _userid = userid;
            _diaryid = diaryId;
            _diaryManager = diaryManager;
            _usermanager = usermanager;
            _Dairy = new Lazy<IDiaryService>(() =>
            {
                var diaryService = diaryManager.GetDiaryService(_diaryid);
                if (diaryService.UserId != userid)
                {
                    throw new ExceptionWithErrorCode(ErrorCode.没有操作权限, "没有权限操作该留言");
                }
                return diaryService;
            });
            user = new Lazy<IUserService>(() =>
              {
                  var userService = _usermanager.GetService(userid);
                  return userService;
              });


        }

        //IDiaryService IUserDiaryService.diary
        //{
        //    get
        //    {
        //        return _diaryManager.GetDiaryService(_diaryid);
        //    }
        //}



        //IUserService IUserDiaryService.user
        //{
        //    get
        //    {
        //        return _usermanager.GetService((int)_userid);
        //    }
        //}

        void IUserDiaryService.DeleteDiary()
        {
            _Dairy.Value.Delete();
        }

        void IUserDiaryService.Publish()
        {
            _Dairy.Value.Publish();
        }

        //void IUserDiaryService.UpdateDiary(string content, string title, bool isPrivate)
        //{
        //    _Dairy.Value.UpdateDiary(_Dairy.Value.Content, _Dairy.Value.Title, _Dairy.Value.IsPrivate);
        //}
    }
}
