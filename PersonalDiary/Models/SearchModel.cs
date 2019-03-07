using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class SearchModel
    {
        public int? userid { get; set; } = null;
        public int OrderValue { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public string NameSearch { get; set; } = null;
    }
}