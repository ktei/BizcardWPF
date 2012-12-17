using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Framework
{
    public interface IConfiguration
    {
        DataSource DataSource { get; }
        ThemeColor Color { get; set; }
        void Save();
        void Rollback();
        string LogFolderPath { get; }
    }
}
