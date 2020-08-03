using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using TodoApp.Controllers;
using TodoApp.Dtos;
using TodoApp.Helpers;
using TodoApp.Repositories;
using Microsoft.AspNetCore.Http;
using NWebsec.AspNetCore.Core.Web;

namespace TodoApp.Tests
{
    public class Tests
    {
        private Mock<ITodoRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        //private Mock<DefaultHttpContext> _httpContext;
        //private Mock<HttpContextWrapper> _httpResponse;


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
        public async Task Should_OkstatusCode_WhenGetTodos()
        {
            var todos = await GetTestTodos();
            var testTodoParams = GetTestTodoParams(1, 25, "", false);

            _mockRepo.Setup(repo => repo.GetTodos(testTodoParams)).ReturnsAsync(todos);
            _mockRepo.Setup(repo => repo.GetTodos(testTodoParams)).ReturnsAsync(todos);

            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new TodoController(_mockRepo.Object, _mockMapper.Object)
            {
                ControllerContext = controllerContext,
            };

            var result = await controller.Get(testTodoParams);
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

        private async Task<PageList<TodoItem>> GetTestTodos()
        {
            var todos = new List<TodoItem>();
            todos.Add(new TodoItem()
            {
                Id = "da57e03d-aa64-4655-9e9f-00bcc5e7fc9d",
                Title = "Teszt Feladat 1234",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                IsFinished = false
            });
            todos.Add(new TodoItem()
            {
                Id = "2083af66-2b81-420b-ad3b-3b5e2ed3d3eb",
                Title = "Teszt Feladat 212389",
                Description = "Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
                IsFinished = false
            });
            todos.Add(new TodoItem()
            {
                Id = "a1b3f565-884a-4ac9-bfb1-9a73ce411923",
                Title = "Teszt Feladat 12974",
                Description = "Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.",
                IsFinished = false
            });
            todos.Add(new TodoItem()
            {
                Id = "a64c051d-23fa-4a6e-8f17-0e3ddb328d6d",
                Title = "Teszt Feladat 3447",
                Description = "Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu.",
                IsFinished = false
            });
            todos.Add(new TodoItem()
            {
                Id = "79629919-a85c-4215-8951-e0005cf26945",
                Title = "Teszt Feladat 9999",
                Description = " In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo.",
                IsFinished = true
            });
            todos.Add(new TodoItem()
            {
                Id = "e9aba336-e89b-489b-ab34-d10765ef38c0",
                Title = "Teszt Feladat 1357",
                Description = "Nullam dictum felis eu pede mollis pretium. Integer tincidunt.",
                IsFinished = true
            });
            todos.Add(new TodoItem()
            {
                Id = "e2174a67-8d46-4fb4-b360-d14cd4404247",
                Title = "Teszt Feladat",
                Description = "Cras dapibus. Vivamus elementum semper nisi. ",
                IsFinished = true
            });

            var todosMock = todos.AsQueryable().BuildMock();
            var pageList = await PageList<TodoItem>.CreateAsync(todosMock.Object, 1, 25);
            return pageList;
        }
    }
}