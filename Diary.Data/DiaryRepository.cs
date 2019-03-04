using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet.Data;
using Tgnet.Data.Entity;

namespace Diary.Data
{
    public interface IDiaryRepository: IRepository<Diary>
    {

    }
    public class DiaryRepository : IDiaryRepository, EFRepository<DiariesEntities, Diary>
    {
        public DiaryRepository(DiariesEntities context) : base(context)
        {

        }
    }
}
