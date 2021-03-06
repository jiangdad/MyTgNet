﻿using Diary.Data;
using Diary.Service;
using Diary.Service.Diary;
using PersonalDiary.Filter;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tgnet;
using Tgnet.Api;
using Tgnet.Linq;
using Tgnet.Web.Mvc;

namespace PersonalDiary.Controllers
{
    [CustomHandletErrorAttribute]
    [CheckLogin]
    public class DiaryController : BaseController
    {
        IDiaryManager _DiaryManager;
        IUserManager _userManager;
        public DiaryController(IDiaryManager DiaryManager, IUserManager userManager)
        {
            _DiaryManager = DiaryManager;
            _userManager = userManager;
        }
        // GET: Default
        //1.所有用户日志列表页面
        //2.当前用户日志列表
        [AllowAnonymous]
        public ActionResult UserIndex(int? userid=null , int value = 1, int page = 1, string searchInfo = null)
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
            
            var messagediary = _DiaryManager.NoTackingDiary;
            ViewBag.Select = value;
            ViewBag.searchInfo = searchInfo;
            //找到所有公开的日志
            if (User == null)
            {
                //公开的
                //ViewBag.userid=0代表用户未登录
                messagediary = messagediary.Where(d => d.IsPrivate == false);
                ViewBag.userid = 0;
            }
            //用户未登录的情况下ViewBag.userid=0
            //用户登录后
            //显示的是自己的所有日志和别人的公开日志
            if (User != null)
            {
                messagediary = messagediary.Where(d => (d.UserId == User.ID && d.IsPrivate == true) || d.IsPrivate == false);
                //把用户登录ID赋值ViewBag.userid
                ViewBag.userid = User.ID;
            }
            int pageSize = 10; int count; int pageCount;
            PageModel<UserDiaryModel> userDiaryModels = null;
            //根据value排序
            if (value == 1)
            {
                ViewBag.Select = "标题升序";
                messagediary = messagediary.OrderBy(a => a.Title);
            }

            else if (value == 2)
            {
                ViewBag.Select = "标题降序";
                messagediary = messagediary.OrderByDescending(a => a.Title);
            }
            else if (value == 3)
            {
                ViewBag.Select = "时间升序";
                messagediary = messagediary.OrderBy(a => a.CreateTime);
            }
            else if (value == 4)
            {
                ViewBag.Select = "时间降序";
                messagediary = messagediary.OrderByDescending(a => a.CreateTime);
            }
            else
            {
                messagediary = messagediary.OrderBy(a => a.Title);
            }
            if (!string.IsNullOrEmpty(searchInfo))
            {
                messagediary = messagediary.Where(d => d.User.UserName.Contains(searchInfo));
            }
            var model = messagediary.
            TakePage(out count, out pageCount, page, pageSize).ToList().Select(p => new UserDiaryModel
            {
                UserDiaryId = p.DiaryId,
                Content = p.Content,
                CreateTime = p.CreateTime,
                DiaryCount = p.UserId,
                Title = p.Title,
                UserName = p.User.UserName,
                UserId = p.UserId
            });
            if (page > pageCount || page < 1)
            {
                if (pageCount != 0)
                    throw new Tgnet.Exception("页码超出范围");
            }
            userDiaryModels = new PageModel<UserDiaryModel>(model, page, pageCount);
            ViewBag.page = page;
            return View(userDiaryModels);
        }

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
                CreateTime = messagediary.CreateTime,
                Title = messagediary.Title,
                UserName = messagediary.UserName
            };
            return View(userdiarymodel);
        }

        // GET: Default/Create

        public ActionResult Create()
        {
            //if (User == null)
            //    throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Content,Title,IsPrivate")]UserDiaryModel userdiarymodel)
        {
            //if (User == null)
            //    throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);

            //Diary.Data.Diary diary = new Diary.Data.Diary { Content = userdiarymodel.Content,
            //    CreateTime = DateTime.Now,
            //    Title = userdiarymodel.Title,
            //    UserId =(int) User.ID, 
            //    IsDel = false,
            //    IsPrivate = !userdiarymodel.IsPrivate };


            var DiaryService = _DiaryManager.Add((int)User.ID, userdiarymodel.Title,
                !userdiarymodel.IsPrivate, userdiarymodel.Content);
            //return JsonString(new BaseReponseModel { Msg = "创建成功", Status = "ok",
            //    Url = Url.RouteUrl(new { controller = "Diary", action = "UserIndex",
            //        userid = diary.UserId
            //    }) });
            return RedirectToAction("UserIndex", new { userid = DiaryService.UserId });
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
                UserDiaryId = diaryid,
                UserId = (int)User.ID
            };
            return View(userdiarymodel);

        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Content,Title,UserDiaryId,IsPrivate,UserId")]UserDiaryModel userdiarymodel)
        {
            //1.调用GetDiaryService方法
            var messagediary = _DiaryManager.GetUserDiaryService(userdiarymodel.UserDiaryId, (int)User.ID);
            //2.调用updateDiary方法
            //messagediary.UpdateDiary(userdiarymodel.UserDiaryId,userdiarymodel.UserId, userdiarymodel.Content,userdiarymodel.Title);
            messagediary.Update(userdiarymodel.Content, userdiarymodel.Title, userdiarymodel.IsPrivate);
            return JsonString(new BaseReponseModel
            {
                Msg = "修改成功",
                Status = "ok",
                Url = Url.RouteUrl(new
                {
                    controller = "Diary",
                    action = "UserIndex",
                    userid = User.ID
                })
            });
        }
        // GET: Default/Delete/5
        public ActionResult Delete(int diaryid)
        {


            var messagediary = _DiaryManager.GetUserDiaryService(diaryid, (int)User.ID);
            messagediary.DeleteDiary();
            //int   PamUserId = messagediary.UserId;
            return RedirectToAction("UserIndex", new
            {
                userid = User.ID
            });
        }

        public ActionResult Publish(int diaryid)
        {
            var messagediary = _DiaryManager.GetUserDiaryService(diaryid, (int)User.ID);
            messagediary.Publish();
            return RedirectToAction("UserIndex", new
            {
                userid = User.ID
            });
        }

    }
    }

