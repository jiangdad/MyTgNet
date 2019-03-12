using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.Diary
{
    public interface IUserDiaryService
    {
        IDiaryService diary { get;}
        IUserService user { get; }
        bool HavePower { get;  }
        void DeleteDiary();
        void Publish();
        void UpdateDiary(string content, string title, bool isPrivate);
    }
}
