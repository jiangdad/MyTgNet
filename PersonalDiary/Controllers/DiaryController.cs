using Diary.Data;
using Diary.Service;
using Diary.Service.Diary;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        //主页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(int? userid)
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
            var messagediary = _DiaryManager.NoTackingDiary.Where(d => d.UserId == userid);
            int pageCount,count, pageSize = 20;
            int page = 1;
            var userdiarymodel = messagediary.OrderByDescending(p => p.CreateTime).TakePage(out count, out pageCount, page, pageSize).Select(p=>new UserDiaryModel{UserDiaryId=p.DiaryId, Content=p.Content, CreateTime=p.CreateTime, DiaryCount=p.UserId, Title=p.Title, UserName=p.User.UserName, UserId=p.UserId }).ToList();
            PageModel<UserDiaryModel> userDiaryModels = new PageModel<UserDiaryModel>(userdiarymodel,page,pageCount);
            ViewBag.userDiaryModels = userDiaryModels;
            ViewBag.User = User;
            return View();
        }

        // GET: Default/Details/5
        //展示日志内容详情
        public ActionResult Details(int diaryid)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
