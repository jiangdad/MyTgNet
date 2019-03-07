using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string content { get; set; }
        public DiaryCommentModel[] commentList { get; set; }
        public Comment()
        {
            commentList = new DiaryCommentModel[0];   
        }
    }
}