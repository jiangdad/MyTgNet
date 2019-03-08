using Diary.Service;
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
            var diaryservice = _DiaryManager.GetDiaryService(diaryid);
            var DiaryComment = diaryservice.DiaryComment;
            return View(DiaryComment.ToList());
        }
        public ActionResult Add(int? diarid=null)
        {
           int? _diaryid = diarid;
            return View();
        }

        //[HttpPost]
        //public ActionResult Add([Bind(Include = "CommentContent")] DiaryCommentModel diarycommentcontroller)
        //{

        //    return JsonString();
        //}
        [HttpPost]
        public ActionResult Add([Bind(Include ="CommentContent")] DiaryCommentModel diaryCommentModel)
        {
            Diary.Data.DiaryComment diaryComment = new Diary.Data.DiaryComment {  CContent=diaryCommentModel.CommentContent,
                  CreateTime=DateTime.Now,
                   DiaryId= _diaryid,
                    UserId=(int)User.ID

            };
            var _DiaryCommentService= _DiaryCommentManager.Add(diaryComment);
            return RedirectToAction("UserIndex", "Diary",new { userid=(int)User.ID});

        }
    }
}