using Diary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet;
using Tgnet.Api;

namespace Diary.Service
{
    class UserService:IUserService
    {
        public IUserRepository IUserRepository;
        private Lazy<Data.User> _LazyUser;
        private readonly int _UserId;
        IDiaryRepository _diaryRepository;
        public UserService(IUserRepository iuserRepository,int userid,IDiaryRepository diaryRepository)
        {
            //是否userid存在
            ExceptionHelper.ThrowIfNotId(userid, "userid");
            _UserId = userid;
            IUserRepository = iuserRepository;
            _diaryRepository = diaryRepository;
            _LazyUser = new Lazy<Data.User>(() =>
            {
                var user = IUserRepository.Entities.FirstOrDefault(u => u.UserId == userid);
                if (user == null)
                    throw new ExceptionWithErrorCode(ErrorCode.没有找到对应条目, "用户不存在");
                return user;
            });
        }
       public IQueryable<Data.Diary> MyDiaries {
            get
            {
                return _diaryRepository.NoTackingDiary.Where(p => p.UserId == _UserId);
            }
                }
        int IUserService.UserId
        {
            get
            {
                return _LazyUser.Value.UserId;
            }
        }      
    }
}
