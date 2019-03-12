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
        IDiaryService Add(Data.Diary diary);

        IQueryable<Data.Diary> MessageDiary { get; }

        IQueryable<Data.Diary> NoTackingDiary { get; }

        IDiaryService GetDiaryService(int DiaryId);

        
    }
}
