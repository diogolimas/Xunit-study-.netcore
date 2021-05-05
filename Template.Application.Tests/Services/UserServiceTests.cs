using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Services;
using Template.Domain.Interfaces;
using Template.Application.ViewModels;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace Template.Application.Tests.Services
{
    public class UserServiceTests

    {
        private UserService userService;

        public UserServiceTests()
        {
            userService = new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object);

        }

        #region ValidatingSendingID
        [Fact]
        public void Post_SendingValidId()
        {
            
            //var result = userService.Post(new UserViewModel { Id = Guid.NewGuid() });
            var exception = Assert.Throws<Exception>(() => userService.Post(new UserViewModel { Id = Guid.NewGuid() }));
            Assert.Equal("UserID não pode ser vazio.", exception.Message);
        }

        [Fact]
        public void GetById_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => userService.GetById(""));
            Assert.Equal("UserID is not valid", exception.Message);
        }

        [Fact]
        public void Put_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => userService.Put(new UserViewModel()));
            Assert.Equal("ID is invalid", exception.Message);
        }

        [Fact]
        public void Delete_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => userService.Delete(""));
            Assert.Equal("UserID is not valid", exception.Message);
        }

        [Fact]
        public void Authenticate_SendingEmptyValues()
        {
            var exception = Assert.Throws<Exception>(() => userService.Authenticate(new UserAuthenticateRequestViewModel()));

            Assert.Equal("Email/Password are required.", exception.Message);
        }
        #endregion

        #region ValidatingCorrectObject
        [Fact]
        public void Post_SendingValidObject()
        {
            var result = userService.Post(new UserViewModel { Name = "Nicolas Fontes", Email = "nicolas.rfontes@gmail.com", Password = "12414124412" });
            Assert.True(result);
        }
        #endregion


        #region ValidatingRequireView
        [Fact]
        public void Post_SendingInvalidObject()
        {
            var exception = Assert.Throws<ValidationException>(() => userService.Post(new UserViewModel { Name = "Nicolas Fontes" }));
            Assert.Equal("The Email field is required.", exception.Message);
        }
        #endregion
    }
}
