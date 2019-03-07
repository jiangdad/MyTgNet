using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalDiary.Controllers
{
    public class DiaryCommentController : Controller
    {
        //一个用户有多条评论，一篇日志有多条评论
        // GET: DiaryComment
        //评论
        //主页显示发布过的视图
        //用户是否登陆，登陆的用户可以评论
        //
        public ActionResult Index(int diaryid)
        {
            return View();
        }
    }
}