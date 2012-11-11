using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.ViewModels
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}
