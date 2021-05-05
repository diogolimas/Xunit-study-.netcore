﻿using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Services;
using Template.Domain.Interfaces;
using Template.Application.ViewModels;
using Xunit;

namespace Template.Application.Tests.Services
{
    public class UserServiceTests

    {
        private UserService userService;

        public UserServiceTests()
        {
            userService = new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object);

        }

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
        public void Put_SendingEMptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => userService.Put(new UserViewModel()));
            Assert.Equal("ID is invalid", exception.Message);
        }

    }
}
