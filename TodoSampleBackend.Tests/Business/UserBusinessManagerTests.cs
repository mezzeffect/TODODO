using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TodoSampleBackend.Interfaces;
using TodoSampleBackend.Interfaces.Data;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Business;
using TodoSampleBackend.Interfaces.Repository;

namespace TodoSampleBackend.Business.Tests
{
    [TestFixture]
    public class UserBusinessManagerTests
    {
        private Mock<IEntityRepository<User>> _mockUserRepository;

        private IBusinessManager<User> GetUserBussinessManager()
        {
            _mockUserRepository = new Mock<IEntityRepository<User>>();
            return new UserBusinessServiceManager(_mockUserRepository.Object);
        }

        [Test]
        public void UserBusinessManagerFindShouldCallRepoCallById()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            manager.Find(It.IsAny<string>());

            //assert
            _mockUserRepository.Verify(g=>g.GetById(It.IsAny<string>()),Times.Once());
        }

        [Test]
        public void UserBusinessManagerFindShouldCallRepoCallByIdWithCorrectId()
        {
            //arrange
            var manager = GetUserBussinessManager();
            string id = String.Empty;
            _mockUserRepository.Setup(g => g.GetById(It.IsAny<string>())).Callback((string i)=>
                                                                                        {
                                                                                            id = i;
                                                                                        });
            //act
            var testId = "test";
            manager.Find(testId);

            //assert
            Assert.AreEqual(testId, id);
        }

        [Test]
        public void UserBusinessManagerFindShouldReturnNullIfGetByIdThrowsException()
        {
            //arrange
            var manager = GetUserBussinessManager();
            int? id = 0;
            _mockUserRepository.Setup(g => g.GetById(It.IsAny<string>())).Throws(new Exception());
            //act
            var testId = "test";
            var result = manager.Find(testId);

            //assert
            Assert.Null(result);
        }

        [Test]
        public void UserBusinessManagerAddShouldReturnTrueIfNothingIsWrong()
        {
            //arrange
            var manager = GetUserBussinessManager();
            //act
            var result = manager.Add(new User());

            //assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public void UserBusinessManagerAddShouldCallRepoAddIfNothingIsWrong()
        {
            //arrange
            var manager = GetUserBussinessManager();
            //act
            var result = manager.Add(new User());

            //assert
            _mockUserRepository.Verify(m=>m.Add(It.IsAny<User>()),Times.Once());
        }

        [Test]
        public void UserBusinessManagerAddShouldCallRepoSubmitChanges()
        {
            //arrange
            var manager = GetUserBussinessManager();
            //act
            var result = manager.Add(new User());

            //assert
            _mockUserRepository.Verify(m => m.SubmitChanges(), Times.Once());
        }

        [Test]
        public void UserBusinessManagerAddShouldReturnFalseIfAddThowsException()
        {
            //arrange
            var manager = GetUserBussinessManager();
            _mockUserRepository.Setup(m => m.Add(It.IsAny<User>())).Throws(new Exception());
            
            //act
            var result = manager.Add(new User());

            //assert
            Assert.False(result);
            
        }

        [Test]
        public void UserBusinessManagerAddShouldReturnFalseIfSubmitChangesThrowsException()
        {
            //arrange
            var manager = GetUserBussinessManager();
            _mockUserRepository.Setup(m => m.SubmitChanges()).Throws(new Exception());

            //act
            var result = manager.Add(new User());

            //assert
            Assert.False(result);

        }

        [Test]
        public void UserBusinessManagerAllShouldCallRepoAll()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            manager.All();

            //assert
            _mockUserRepository.Verify(g=>g.All(),Times.Once());
        }

        [Test]
        public void UserBusinessManagerAllShouldReturnNullIfExceptionIsThrown()
        {
            //arrange
            var manager = GetUserBussinessManager();
            _mockUserRepository.Setup(g => g.All()).Throws(new Exception());
            
            //act
            var result = manager.All();

            //assert
            Assert.Null(result);
        }

        [Test]
        public void UserBusinessManagerRemoveShouldCallRepDelete()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            var result = manager.Remove(It.IsAny<User>());

            //assert
            _mockUserRepository.Verify(g=>g.Delete(It.IsAny<User>()),Times.Once());
        }

        [Test]
        public void UserBusinessManagerRemoveShouldCallRepSubmitChanges()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            manager.Remove(It.IsAny<User>());

            //assert
            _mockUserRepository.Verify(g => g.SubmitChanges(),Times.Once());
        }

        [Test]
        public void UserBusinessManagerRemoveShouldReturnFalseIfExceptionIsThrown()
        {
            //arrange
            var manager = GetUserBussinessManager();
            _mockUserRepository.Setup(g => g.Delete(It.IsAny<User>())).Throws(new Exception());
            //act
            var result = manager.Remove(It.IsAny<User>());

            //assert
            Assert.False(result);
        }

        [Test]
        public void UserBusinessManagerRemoveShouldReturnTrueIfSuccessfull()
        {
            //arrange
            var manager = GetUserBussinessManager();
            //act
            var result = manager.Remove(It.IsAny<User>());

            //assert
            Assert.True(result);
        }

        [Test]
        public void UserBusinessManagerUpdateShouldCallRepUpdate()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            var result = manager.Update(It.IsAny<User>());

            //assert
            _mockUserRepository.Verify(g => g.Update(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void UserBusinessManagerUpdateShouldCallRepSubmitChanges()
        {
            //arrange
            var manager = GetUserBussinessManager();

            //act
            var result = manager.Update(It.IsAny<User>());

            //assert
            _mockUserRepository.Verify(g => g.SubmitChanges(), Times.Once());
        }

        [Test]
        public void UserBusinessManagerUpdateShouldReturnFalseIfExceptionIsThrown()
        {
            //arrange
            var manager = GetUserBussinessManager();
            _mockUserRepository.Setup(g => g.Update(It.IsAny<User>())).Throws(new Exception());
            //act
            var result = manager.Update(It.IsAny<User>());

            //assert
            Assert.False(result);
        }

        [Test]
        public void UserBusinessManagerUpdateShouldReturnTrueIfSuccessfull()
        {
            //arrange
            var manager = GetUserBussinessManager();
            
            //act
            var result = manager.Update(It.IsAny<User>());

            //assert
            Assert.True(result);
        }


    }
}
