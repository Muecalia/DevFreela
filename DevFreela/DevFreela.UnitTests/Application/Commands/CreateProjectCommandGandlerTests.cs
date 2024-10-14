using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandGandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_returnProjectId()
        {
            //Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand 
            {
                Title = "Titulo de Teste ",
                Description = "Descrição do teste",
                IdClient = 1, 
                IdFreelancer = 2,
                TotalCost = 50000
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // Fact
            Assert.True(id >= 0);

            projectRepository.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
