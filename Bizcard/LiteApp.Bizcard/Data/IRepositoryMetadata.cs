using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data
{
    public interface IRepositoryMetadata
    {
        DataSource DataSource { get; }
        Type Contract { get; }
    }
}
