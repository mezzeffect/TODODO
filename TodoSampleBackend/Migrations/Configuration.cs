using System.Collections.Generic;
using TodoSampleBackend.DataObjects;
using Microsoft.Azure.Mobile.Server.Tables;
using TodoSampleBackend.Data;

namespace TodoSampleBackend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.TodoSampleDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
        }

        protected override void Seed(TodoSampleDataContext context)
        {
            
        }
    }
}
