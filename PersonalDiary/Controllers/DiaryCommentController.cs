using Diary.Service;
using Diary.Service.Diary;
using Diary.Service.DiaryComment;
using PersonalDiary.Filter;
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
    [Autho]
    [CheckLogin]
    public class DiaryCommentController : BaseController
    {
        //一个用户有多条评论，一篇日志有多条评论
        // GET: DiaryComment
        //评论
        //主页显示发布过的视图
        //用户是否登陆，登陆的用户可以评论
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
            //Comment是单条日志下的所有评论
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
                //UserID代表该条评论的用户ID
                UserId = p.UserId, DiaryId= diaryid,
                // 
                CanDel =User!=null? (int)User.ID == p.UserId:false,
            }).ToArray();
            return View(model);
           
        }
        [AllowAnonymous]
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
        [HttpPost]
        public ActionResult Add([Bind(Include = "CommentContent,DiaryId")] DiaryCommentModel diaryCommentModel)
        {
            //获取DiaryService服务，构造函数内部验证是否有这条日志，
            var DiaryManager = _DiaryManager.GetDiaryService(diaryCommentModel.DiaryId);
            //调用AddComment方法,该条评论的userid设置为当前登陆用户
            DiaryManager.AddComment(diaryCommentModel.CommentContent, (int)User.ID);
            //
            return RedirectToAction("UserIndex", "Diary",new { userid=(int)User.ID});
        }
        
        public ActionResult Delete(int diarycommentid)
        {
            //权限判断
            //获取UserCommentService服务，实例化服务对象，调用内部构造函数验证用户权限
            var messagecomment = _DiaryCommentManager.GetUserCommentService(diarycommentid,(int)User.ID);
            //DeleteComment方法内部调用Diarycommment中的Delete方法
            messagecomment.DeleteComment();
            //int   PamUserId = messagediary.UserId;
            return RedirectToAction("UserIndex","Diary", new
            {
                userid = User.ID
            });
        }
    }
}