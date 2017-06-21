using System.Data.Entity;

namespace TodoSampleBackend.Data
{
    public class TodoSampleDbInitializer : CreateDatabaseIfNotExists<TodoSampleDataContext>
    {
        protected override void Seed(TodoSampleDataContext context)
        {
            base.Seed(context);
        }
    }
}