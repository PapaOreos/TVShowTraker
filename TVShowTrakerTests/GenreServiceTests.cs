using AutoMapper;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Helpers.Mappers;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Services;
using TVShowTrakerTests.Context;

namespace TVShowTrakerTests
{
    internal class GenreServiceTests
    {
        private GenreService _service;

        [SetUp]
        public void Init()
        {
            var context = new ApplicationDbContext();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper _mapper = mappingConfig.CreateMapper();

            _service = new GenreService(context, _mapper);
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
            var genres = _service.GetAll();
            if (!genres.Any())
            {
                var genreToCreate = new Genre() { Description = "TestingDescription" };
                var response = _service.Create(genreToCreate);

                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
                genres = _service.GetAll();
            }

            var genre = _service.Get(genres.FirstOrDefault().Id);

            Assert.IsTrue(genre is not null, "Model shouldn't be null");
            Assert.IsTrue(genre.IsValid(), "Model is not valid");
        }

        [Test]
        public void ShouldBePossibleGetDelete()
        {
            var genres = _service.GetAll();
            if (!genres.Any())
            {
                var genreToCreate = new Genre() { Description = "TestingDescription" };
                var response = _service.Create(genreToCreate);

                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
                genres = _service.GetAll();
            }

            var genre = _service.Get(genres.FirstOrDefault().Id);

            Assert.IsTrue(genre is not null, "Model shouldn't be null");
            Assert.IsTrue(genre.IsValid(), "Model is not valid");

            var deleteResponse = _service.Delete(genre.Id);
            Assert.IsTrue(deleteResponse.Status == ExceptionMessages.Success, "Status should be success");
        }

        [Test]
        public void ShouldNotBePossibleCreateIfAlreadyExists()
        {
            var genre = new Genre() { Description = "TestingDescription" };
            var response = _service.Create(genre);
            var response2 = _service.Create(genre);

            Assert.IsTrue(response2.Status == ExceptionMessages.Fail, "Status should be Fail");
        }
    }
}
