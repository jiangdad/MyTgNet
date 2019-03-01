using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class BaseReponseModel
    {
        public string Msg { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}