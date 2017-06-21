using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoSampleBackend.Tests.Helpers;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Data;

namespace TodoSampleBackend.Tests.Helpers
{
    public class FakeTodoSampleDataContext : ITodoSampleDataContext
    {
        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<User> Users { get; set; }
        public bool Saved { get; private set; }
        public bool Updated { get; private set; }
        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(FakeTodoSampleDataContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }
        public void SaveChanges()
        {
            Saved = true;
        }
        public FakeTodoSampleDataContext()
        {
            // Set up your collections
            Users = new FakeUserSet()
            {
                new User() {Id = "1",UserName = "Brent"},
                new User() {Id = "2",UserName = "Darth"},
                new User() {Id = "3",UserName = "Follower"},
                new User() {Id = "3",UserName = "Maker"},
                new User() {Id = "3",UserName = "Green"},
                new User() {Id = "3",UserName = "Red"},
                new User() {Id = "3",UserName = "Yellow"},
                new User() {Id = "3",UserName = "Fast"},
                new User() {Id = "3",UserName = "Zoo"},
                new User() {Id = "3",UserName = "Adam"}
            };
        }

        int ITodoSampleDataContext.SaveChanges()
        {
            Saved = true;
            return 1;
        }

        public void SetModified(object entity)
        {
            throw new NotImplementedException("Not To be tested here");
        }
    }
}
