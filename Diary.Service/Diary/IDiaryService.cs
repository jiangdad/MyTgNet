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
        string Title { get; }
        void UpdateDiary(int DiaryId, string Content,string Title);

        void Delete(int diary);
    }
}
