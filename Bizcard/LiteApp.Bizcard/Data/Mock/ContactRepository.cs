using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using System.Threading;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data.Mock
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Mock)]
    [ExportMetadata("Contract", typeof(IContactRepository))]
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        protected override AsyncResult<IEnumerable<Contact>> OnLoadingEntities()
        {
            Thread.Sleep(3000);
            var entities = new List<Contact>();
            for (int i = 0; i < 10; ++i)
            {
                entities.Add(new Contact() { Name = "Name " + i });
            }
            Entities = entities;
            return base.OnLoadingEntities();
        }
    }
}
