using Diary.Service;
using Diary.Service.Diary;
using Diary.Service.DiaryComment;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tgnet.Web.Mvc;
namespace PersonalDiary.Controllers
{
    public class DiaryCommentController : BaseController
    {
        //一个用户有多条评论，一篇日志有多条评论
        // GET: DiaryComment
        //评论
        //主页显示发布过的视图
        //用户是否登陆，登陆的用户可以评论
        //
        int _diaryid;
       public IDiaryCommentManager _DiaryCommentManager;
        public IDiaryManager _DiaryManager;
       public DiaryCommentController(IDiaryCommentManager diarycommentmanager, IDiaryManager diarymanager)
        {
            _DiaryManager = diarymanager;
            _DiaryCommentManager = diarycommentmanager;
        }
        public ActionResult Index(int diaryid)
        {

            //判断用户是否登陆、、
            //if(User==null)
            //{
            //    throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);
            //}
            Comment model = new Comment();
            var diaryservice = _DiaryManager.GetDiaryService(diaryid);
            model.Diaryid = diaryid;
            model.content = diaryservice.Content;
            model.UserName = diaryservice.UserName;
            model.Title = diaryservice.Title;
            model.commentList = diaryservice.DiaryComment.Select(p=>new DiaryCommentModel
            {
                CommentContent =p.CContent,
                DiaryCommentId =p.CommentId,
                DiaryId =p.DiaryId,UserId=p.UserId
            }).ToArray();
            var DiaryComment = diaryservice.DiaryComment;
            return View(model);
        }
        public ActionResult Add(int diaryid)
        {
            IDiaryService diaryService= _DiaryManager.GetDiaryService(diaryid);
            UserDiaryModel userDiaryModel = new UserDiaryModel
            {
                Content = diaryService.Content,
                CreateTime = diaryService.CreateTime,
                Title = diaryService.Title,
                UserDiaryId = diaryService.DiaryId,
                UserName = diaryService.UserName,
                UserId = diaryService.UserId
            };
            ViewBag.DiaryId = diaryid;
            return View(userDiaryModel);
        }

        //[HttpPost]
        //public ActionResult Add([Bind(Include = "CommentContent")] DiaryCommentModel diarycommentcontroller)
        //{

        //    return JsonString();
        //}
        [HttpPost]
        public ActionResult Add([Bind(Include = "CommentContent,DiaryId")] DiaryCommentModel diaryCommentModel)
        {
            if (User == null)
                throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);
            Diary.Data.DiaryComment diaryComment = new Diary.Data.DiaryComment {  CContent=diaryCommentModel.CommentContent,
                  CreateTime=DateTime.Now,
                   DiaryId= diaryCommentModel.DiaryId,
                    UserId=(int)User.ID
            };
            var _DiaryCommentService= _DiaryCommentManager.Add(diaryComment);
            return RedirectToAction("UserIndex", "Diary",new { userid=(int)User.ID});
        }
    }
}