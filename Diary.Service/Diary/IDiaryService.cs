using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.Diary
{
   public interface IDiaryService
    {
        int DiaryId { get; }
        int UserId { get; }
        string UserName { get; }
        string Content { get; }
        DateTime CreateTime { get; }

        void UpdateDiary(int userId, string Content);

        void Delete(int userId);
    }
}
