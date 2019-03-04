using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class UserDiaryModel
    {
        public int UserDiaryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? CreateTime { get; set; }
        public int DiaryCount { get; set; }
    }
}