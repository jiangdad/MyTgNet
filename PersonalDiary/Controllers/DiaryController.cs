using Diary.Data;
using Diary.Service;
using Diary.Service.Diary;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tgnet;
using Tgnet.Linq;
using Tgnet.Web.Mvc;

namespace PersonalDiary.Controllers
{
    public class DiaryController : BaseController
    {
        IDiaryManager _DiaryManager;
        public DiaryController(IDiaryManager DiaryManager)
        {
            _DiaryManager = DiaryManager;
        }
        // GET: Default
        //所有用户日志列表页面
        public ActionResult AllIndex()
        {
            return View();
        }
        //当前用户日志列表
        public ActionResult UserIndex(int userid,string? value)
        {
            // 1. 从数据库中读取实体对象 (Diary)

            //var repository = new DiaryRepository(new DiariesEntities());
            //var diaries = repository.NoTackingDiary.ToList();
            //var Diary=_DiaryManager.NoTackingDiary;
            //// 2. 将实体对象转换成 Model
            //var diaries = Diary.Where(diary => diary.UserId == userid);
            //// 3. 将 Model 对象传递给视图 (View)
            //return View(diaries);
            //1.从Diary仓储类中找到UserId==userid的实体集 （IQueryable类型）
            //2.
            //int A;
            //int.TryParse(userid, out A);
            var messagediary = _DiaryManager.NoTackingDiary.Where(d => d.UserId ==userid);
            int pageSize = 20;int count; int pageCount;
            int page = 1;
            var userdiarymodel = messagediary.OrderByDescending(p => p.CreateTime)
                .TakePage(out count, out pageCount, page, pageSize) .ToList()
                .Select(p=>new UserDiaryModel{UserDiaryId=p.DiaryId, Content=p.Content, CreateTime=p.CreateTime, DiaryCount=p.UserId, Title=p.Title, UserName=p.User.UserName, UserId=p.UserId })
               ;
            //PageModel<UserDiaryModel> userDiaryModels = null;
            PageModel<UserDiaryModel> userDiaryModels = new PageModel<UserDiaryModel>(userdiarymodel, page, pageCount);
            //ViewBag.userDiaryModels = userDiaryModels;
            ViewBag.User = User;

            //var str = "";//排序的值
            //if (str == "1")
            //    messagediary = messagediary.OrderBy(a => a.DiaryId);
            //else if()

            return View(userDiaryModels);
        }
//        严重性 代码  说明 项目  文件 行   类别 禁止显示状态
//警告 未能找到引用的组件“Microsoft.QualityTools.Testing.Fakes”。	PersonalDiary




        // GET: Default/Details/5
        //展示日志内容详情
        //[AllowHtml]
        //[ValidateInput]
        public ActionResult Details(int diaryid)
        {
            var messagediary = _DiaryManager.GetDiaryService(diaryid);
            UserDiaryModel userdiarymodel = new UserDiaryModel
            {
                Content = messagediary.Content,
                CreateTime = messagediary.CreateTime ,
                Title = messagediary.Title,
                UserName = messagediary.UserName
            };
            return View(userdiarymodel);
        }

        // GET: Default/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Content,Title")]UserDiaryModel userdiarymodel)
        {
            //if (User == null)
            //    throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);
            
            Diary.Data.Diary diary = new Diary.Data.Diary { Content = userdiarymodel.Content,
                CreateTime = DateTime.Now,
                Title = userdiarymodel.Title,
                UserId =(int) User.ID, 
              
                IsDel = false, IsPrivate = false };
               var DiaryService= _DiaryManager.Add(diary);
            return JsonString(new BaseReponseModel { Msg = "创建成功", Status = "ok",
                Url = Url.RouteUrl(new { controller = "Diary", action = "Index",
                    userid = diary.UserId
                }) });
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int diaryid)
        {
            var messagediary = _DiaryManager.GetDiaryService(diaryid);
            UserDiaryModel userdiarymodel = new UserDiaryModel
            {
                Content = messagediary.Content,
                CreateTime = messagediary.CreateTime,
                Title = messagediary.Title,
                UserName = messagediary.UserName,
                 UserDiaryId=diaryid, UserId=messagediary.UserId
            };
            return View(userdiarymodel);

        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Content,Title,UserDiaryId")]UserDiaryModel userdiarymodel)
        {
            //1.调用GetDiaryService方法
            var messagediary = _DiaryManager.GetDiaryService(userdiarymodel.UserDiaryId);
            //2.调用updateDiary方法
            messagediary.UpdateDiary(userdiarymodel.UserDiaryId, userdiarymodel.Content,userdiarymodel.Title);

            return JsonString(new BaseReponseModel { Msg = "修改成功", Status = "ok", Url = Url.RouteUrl(new { controller = "Diary",
                action = "Index",
                userid = messagediary.UserId }) });
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int diaryid)
        {
            var messagediary = _DiaryManager.GetDiaryService(diaryid);
            messagediary.Delete(messagediary.DiaryId);
          //int   PamUserId = messagediary.UserId;
            return RedirectToAction("Index", new
            {
                userid = User.ID
            });
        }

   
    }
}
