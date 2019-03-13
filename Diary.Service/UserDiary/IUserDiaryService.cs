using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service.Diary
{
    public interface IUserDiaryService
    {
        IDiaryService diary { get; }
        IUserService user { get; }

        void DeleteDiary();
        void Publish();
        void Update(string content, string title, bool isPrivate);

    }
}
