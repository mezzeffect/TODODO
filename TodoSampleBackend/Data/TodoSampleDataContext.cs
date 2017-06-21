using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Data;

namespace TodoSampleBackend.Data
{
    public class TodoSampleDataContext : DbContext,ITodoSampleDataContext
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

        #region I L o p o c a S a m p l e D a t a C o n t e x t
        #region D B  S E T S
        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<User> Users { get; set; }
        #endregion

        #region M E T H O D S

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        #endregion 
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
