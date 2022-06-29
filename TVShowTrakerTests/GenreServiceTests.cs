using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Helpers.Mappers;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Services;

namespace TVShowTrakerTests
{
    internal class GenreServiceTests
    {
        private GenreService _service;

        [SetUp]
        public void Init()
        {
            var mockSet = new Mock<DbSet<Genre>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Genres).Returns(mockSet.Object);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper _mapper = mappingConfig.CreateMapper();

            _service = new GenreService(mockContext.Object, _mapper);
        }

        [Test]
        public void ShouldBePossibleCreate()
        {
            var genre = new Genre() { Description = "TestingDescription" };
            var response = _service.Create(genre);

            Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
            
            var createdModel = _service.GetByGenreDescription(genre.Description);
            
            Assert.IsTrue(createdModel is not null, "Model shouldn't be null");
            Assert.IsTrue(createdModel.IsValid(), "Model is not valid");
        }

        [Test]
        public void ShouldBePossibleGetById()
        {

        }

        [Test]
        public void ShouldNotBePossibleCreateIfAlreadyExists()
        {

        }
    }
}
