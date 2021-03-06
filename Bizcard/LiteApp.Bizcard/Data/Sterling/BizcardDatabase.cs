﻿using System.Collections.Generic;
using LiteApp.Bizcard.Models;
using Wintellect.Sterling.Database;

namespace LiteApp.Bizcard.Data.Sterling
{
    public class BizcardDatabase : BaseDatabaseInstance
    {
        /// <summary>
        ///     The name of the database instance
        /// </summary>
        public override string Name
        {
            get { return "Bizcard"; }
        }

        /// <summary>
        ///     Method called from the constructor to register tables
        /// </summary>
        /// <returns>The list of tables for the database</returns>
        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>
                       {
                           // Defines Contact table
                           CreateTableDefinition<Contact, int>(x => x.Id)
                                .WithIndex<Contact, string, int>("Name", x => x.Name),
                           
                           // Defines Group table
                           CreateTableDefinition<Group, int>(x => x.Id)
                                .WithIndex<Group, string, int>("Name", x => x.Name),
                           
                           // Defines UserSettings table
                           CreateTableDefinition<UserSettings, int>(x => x.Id)
                       };
        }
    }
}
