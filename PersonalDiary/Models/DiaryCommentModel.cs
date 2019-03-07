using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class DiaryCommentModel
    {
        public int DiaryCommentId { get; set; }
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int DiaryId { get; set; }
    }
}