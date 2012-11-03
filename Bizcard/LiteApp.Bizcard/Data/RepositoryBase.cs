using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.Data
{
    public class RepositoryBase<T> : IRepository where T : class
    {
        public IEnumerable<T> Entities { get; protected set; }
        public RepositoryStat Stat { get; protected set; }

        public void LoadEntitiesAsync(Action<AsyncResult<IEnumerable<T>>> callback)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Stat = RepositoryStat.IsLoading;
                var result = OnLoadingEntities();
                Stat = RepositoryStat.IsLoaded;
                if (callback != null)
                    callback(result);
            });
        }

        protected virtual AsyncResult<IEnumerable<T>> OnLoadingEntities()
        {
            return new AsyncResult<IEnumerable<T>>() { Data = Entities, Error = null, Success = true };
        }
    }
}
