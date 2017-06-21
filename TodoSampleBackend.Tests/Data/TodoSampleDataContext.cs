using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using TodoSampleBackend.DataObjects;

namespace TodoSampleBackend.Models
{
    public class TodoSampleDataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        #region P R I V A T E
        private const string ConnectionStringName = "Name=TodoDbConnection"; 
        #endregion

        #region C O N S T R U C T O R S
        public TodoSampleDataContext() : base(ConnectionStringName) {}
        #endregion

        #region D B  S E T S
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region O N  M O D E L  C R E A T I O N
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        } 
        #endregion
    }

    
}
