using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Data;
using TodoSampleBackend.Interfaces.Repository;
using TodoSampleBackend.Repository;
using TodoSampleBackend.Tests.Helpers;

namespace TodoSampleBackend.Business.Tests.Repository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<ITodoSampleDataContext> _mockTodoSampleDataContext;

        private IEntityRepository<User> GetMockDbContextRepository()
        {
            _mockTodoSampleDataContext = new Mock<ITodoSampleDataContext>();
            return new UserRepository(_mockTodoSampleDataContext.Object);
        }

        private IEntityRepository<User> GetFakeDbContextRepository()
        {
            return new UserRepository(new FakeTodoSampleDataContext());
        }

        [Test]
        public void UserRepositoryAddShouldReturnAUser()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();
            var user = new User();

            //act
            repo.Add(user);
            var result = repo.GetById(user.Id);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public void UserRepositoryAllShouldReturnListOrderedByTermAlphabitically()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.All();

            //assert
            Assert.True(result.Last().UserName == "Zoo");
        }

        [Test]
        public void UserRepositoryAddShouldAddTheCorrectUser()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();
            var user = new User();

            //act
            repo.Add(user);
            var result = repo.GetById(user.Id);

            //assert
            Assert.AreEqual(result, user);
        }

        [Test]
        public void UserRepositoryGetByIdShouldReturnUser()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.GetById("1");

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public void UserRepositoryGetByIdShouldReturnTheCorrectUser()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.GetById("1");

            //assert
            Assert.NotNull(result);
            Assert.AreEqual("Brent", result.UserName);
        }

        [Test]
        public void UserRepositoryDeleteShouldWorkCorrectly()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.GetById("1");
            repo.Delete(result);
            result = repo.GetById("1");

            //assert
            Assert.Null(result);
        }

        [Test]
        public void UserRepositoryUpdateShouldCallEntry()
        {
            //arrange 
            var repo = GetMockDbContextRepository();

            //act
            repo.Update(It.IsAny<User>());

            //assert
            _mockTodoSampleDataContext.Verify(g => g.SetModified(It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void UserRepositoryUpdateShouldEntryWithTheCorrectEntity()
        {
            //arrange 
            var repo = GetMockDbContextRepository();
            var User = new User { UserName = "rath", Name = "rage" };
            var result = new User();
            _mockTodoSampleDataContext.Setup(g => g.SetModified(It.IsAny<object>())).Callback((object o) =>
            {
                result =
                    o as User;
            });
            //act
            repo.Update(User);

            //assert
            Assert.AreEqual(User, result);
        }

        [Test]
        public void UserRepositorySubmitChangesShouldCallContextSave()
        {
            //arrange 
            var repo = GetMockDbContextRepository();

            //act
            repo.SubmitChanges();

            //assert
            _mockTodoSampleDataContext.Verify(c => c.SaveChanges(), Times.Once());
        }
    }
}
