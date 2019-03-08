﻿using System;
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
        void UpdateDiary(int DiaryId,int UserId, string Content,string Title);
        IQueryable<Data.DiaryComment> DiaryComment { get; }
        void Delete(int userid);
    }
}
