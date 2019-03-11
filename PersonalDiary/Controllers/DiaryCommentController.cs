using Diary.Service;
using Diary.Service.Diary;
using Diary.Service.DiaryComment;
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
            //获取该条日志的服务
            var diaryservice = _DiaryManager.GetDiaryService(diaryid);
            model.Diaryid = diaryid;
            model.content = diaryservice.Content;
            //日志的用户ID diaryservice.UserId;
            //受到评论的日志的用户名
            model.UserName = diaryservice.UserName;
            //判断当前登陆User的ID和该条评论的用户ID是否一致,一样的话可以进行删除该条评论操作
            model.Title = diaryservice.Title;
         
            model.commentList = diaryservice.DiaryComment.ToList().Select(p => new DiaryCommentModel
            {
                CommentContent = p.CContent,
                DiaryCommentId = p.CommentId,
                DiaryId = p.DiaryId,
                //UserID代表该条评论的用户ID
                UserId = p.UserId,
                //
                CanDel =User!=null? (int)User.ID == p.UserId:false,
            }).ToArray();
           
            var DiaryComment = diaryservice.DiaryComment; 
           
            return View(model);
           
        }
        [HttpPost]
        public PartialViewResult _Index(int diaryid,int page=1)
        {
            int pageSize = 10; int count; int pageCount;

            var diaryservice = _DiaryManager.GetDiaryService(diaryid);
            var model= diaryservice.DiaryComment.TakePage(out count, out pageCount, page, pageSize).ToList().Select(p => new DiaryCommentModel
            {
                CommentContent = p.CContent,
                DiaryCommentId = p.CommentId,
                DiaryId = p.DiaryId,
                //UserID代表该条评论的用户ID
                UserId = p.UserId,
                //
                CanDel = User != null ? (int)User.ID == p.UserId : false,
            });
            PageModel<DiaryCommentModel> commentmodel = new PageModel<DiaryCommentModel>(model, page, pageCount);
            return PartialView(commentmodel);
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

        public ActionResult Delete(int diarycommentid)
        {
            var messagecomment = _DiaryCommentManager.GetDiaryCommentService(diarycommentid);
            messagecomment.Delete((int)User.ID);
            //int   PamUserId = messagediary.UserId;
            return RedirectToAction("UserIndex","Diary", new
            {
                userid = User.ID
            });
        }
    }
}