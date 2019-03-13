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
        void UpdateDiary(string Content,string Title,bool IsPrivate);
        IQueryable<Data.DiaryComment> DiaryComment { get; }
        void Delete();
        void Publish();
        void AddComment(string commentcontent,int userid);
    }
}
