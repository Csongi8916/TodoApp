using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using TodoApp.Controllers;
using TodoApp.Dtos;
using TodoApp.Helpers;
using TodoApp.Repositories;

namespace TodoApp.Tests
{
    public class Tests
    {
        private Mock<ITodoRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test]
        public async Task Should_OkstatusCode_WhenGetTodo()
        {
            _mockRepo.Setup(repo => repo.GetTodo("4bbcfda8-b2bc-4e46-a174-f4c8cc001596")).ReturnsAsync(GetTestTodo());

            var controller = new TodoController(_mockRepo.Object, _mockMapper.Object);
            var result = await controller.Get("4bbcfda8-b2bc-4e46-a174-f4c8cc001596");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task Should_StatusCode201_WhenCreateTodo()
        {
            _mockRepo.Setup(repo => repo.Add(GetTestTodo()));
            _mockRepo.Setup(repo => repo.SaveAll()).ReturnsAsync(true);

            var controller = new TodoController(_mockRepo.Object, _mockMapper.Object);
            var result = await controller.CreateTodo(CreateTestTodoDetailedDto());
            var statusCodeResult = result as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(201, statusCodeResult.StatusCode);
        }

        [Test]
        public async Task Should_StatusCode404_WhenDeleteTodoIsNotExsist()
        {
            _mockRepo.Setup(repo => repo.Delete(GetTestTodo()));
            _mockRepo.Setup(repo => repo.SaveAll()).ReturnsAsync(true);

            var controller = new TodoController(_mockRepo.Object, _mockMapper.Object);
            var result = await controller.DeleteTodo("bad-id");
            var notFoundResult = result as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        } 

        private TodoDetailedDto CreateTestTodoDetailedDto()
        {
            return new TodoDetailedDto()
            {
                Title = "Teszt Feladat 1234",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                IsFinished = true
            };
        }

        private TodoItem GetTestTodo()
        {
            return new TodoItem()
            {
                Id = "4bbcfda8-b2bc-4e46-a174-f4c8cc001596",
                Title = "Teszt Feladat 1234",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                IsFinished = true
            };
        }

        private TodoParams GetTestTodoParams(int pageNumber, int pageSize, string title, bool isFinished)
        {
            return new TodoParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Title = title,
                IsFinished = isFinished
            };
        }
    }
}