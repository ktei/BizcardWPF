using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Helpers;

namespace LiteApp.Bizcard.Data
{
    [Export]
    public class RepositoryFactory
    {
        public RepositoryFactory()
        {
            
        }

        public T GetRepository<T>() where T : IRepository
        {
            var repos = RepositoryPool.FirstOrDefault(x => x.Metadata.Contract == typeof(T) && x.Metadata.DataSource == Configuration.DataSource);
            if (repos == null)
                throw new Exception("Couldn't locate repository of type " + typeof(T));
            return (T)repos.Value;
        }

        [ImportMany]
        public IEnumerable<Lazy<IRepository, IRepositoryMetadata>> RepositoryPool { get; set; }

        [Import]
        public IConfiguration Configuration { get; set; }
    }
}
