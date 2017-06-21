using System.Collections.Generic;
using System.Linq;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Data;
using TodoSampleBackend.Interfaces.Repository;

namespace TodoSampleBackend.Repository
{
    public class UserRepository : IEntityRepository<User>
    {
        #region P R I V A T E

        private readonly ITodoSampleDataContext _todoSampleDataContext;

        #endregion

        #region I  E N T I T Y  R E P O S I T O R Y
        public UserRepository(ITodoSampleDataContext todoSampleDataContext)
        {
            _todoSampleDataContext = todoSampleDataContext;
        }
        public User GetById(string id)
        {
            return _todoSampleDataContext.Users.Find(id);
        }

        public void Add(User entity)
        {
            _todoSampleDataContext.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            _todoSampleDataContext.Users.Remove(entity);
        }

        public void Update(User entity)
        {
            var user = _todoSampleDataContext.Users.Find(entity.Id);
            user = entity;
        }

        public List<User> All()
        {
            return _todoSampleDataContext.Users.ToList();
        }

        public void SubmitChanges()
        {
            _todoSampleDataContext.SaveChanges();
        }
        #endregion
    }
}