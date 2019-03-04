using Diary.Data;
using Diary.Service.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service
{
   public class DiaryManager:IDiaryManager
    {
        public IDiaryRepository _diarepository;
        public DiaryManager(IDiaryRepository diaryRepository)
        {
            _diarepository = diaryRepository;
        }
        public IQueryable<Data.Diary> MessageDiary
        {
            get
            {
                return _diarepository.EnableDiary;
            }
        }

        public IQueryable<Data.Diary> NoTackingDiary
        {
            get
            {
                return _diarepository.NoTackingDiary;
            }
        }

        public IDiaryService Add(Data.Diary diary)
        {
            _diarepository.Add(diary);
            _diarepository.SaveChanges();
            return GetDiaryService(diary.DiaryId);
        }

        public IDiaryService GetDiaryService(int DiaryId)
        {
            return new DiaryService(DiaryId, _diarepository);
        }

    }
}
