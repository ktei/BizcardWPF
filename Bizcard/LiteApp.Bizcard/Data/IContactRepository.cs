using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data
{
    public interface IContactRepository : IRepository
    {
        void LoadEntitiesAsync(Action<AsyncResult<IEnumerable<Contact>>> callback);
    }
}
