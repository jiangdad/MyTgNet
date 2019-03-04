using Diary.Data;
using Diary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            // 1. 从数据库中读取实体对象 (Diary)
            
            //var repository = new DiaryRepository(new DiariesEntities());
            //var diaries = repository.NoTackingDiary.ToList();
            _DiaryManager.NoTackingDiary()
            // 2. 将实体对象转换成 Model

            // 3. 将 Model 对象传递给视图 (View)

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
