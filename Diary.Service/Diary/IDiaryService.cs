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
        IQueryable<Data.DiaryComment> DiaryComment { get; }
        void DeleteDiary();
        void Publish();
        //void UpdateDiary(int userid, string content, string title, bool isPrivate);

        //void UpdateDiary(string content, string title, bool isPrivate);
    }
}
