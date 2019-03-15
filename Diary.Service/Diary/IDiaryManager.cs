using Diary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Data;
using Diary.Service.Diary;

namespace Diary.Service
{
  public  interface IDiaryManager
    {
        IDiaryService Add(int userid,string title,bool isPrivate,string content);

        IQueryable<Data.Diary> MessageDiary { get; }

        IQueryable<Data.Diary> NoTackingDiary { get; }

        IDiaryService GetDiaryService(int DiaryId);
        IUserDiaryService GetUserDiaryService(int diaryid, int userid);
        
    }
}
