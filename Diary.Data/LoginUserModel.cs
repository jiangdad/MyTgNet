using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Data
{
   public class LoginUserModel
    {
        public int UserId { get; set; }
        public string Msg { get; set; }
        public string Status { get; set; }
        public object UserName { get; set; }
    }
}
