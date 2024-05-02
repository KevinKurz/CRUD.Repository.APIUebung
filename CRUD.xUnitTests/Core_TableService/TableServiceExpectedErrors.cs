using CRUD.Core.TableService;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;
using Moq;

namespace CRUD.xUnitTests.Core_TableService
{
    public class TableServiceExpectedErrors
    {
        // Mocklist
        private List<TableModel> mockList = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };
        private TableRepository _fakeRepo;
        public TableServiceExpectedErrors()
        {
            // Mock of JsonService
            Mock<JsonService> mockService = new Mock<JsonService>(); // You need to mock the Class, which you do not want to be accessed by the test
            mockService.Setup(m => m.SaveListAsJsonFile(mockList)); // Create a fakeMethod of SaveList
            mockService.Setup(m => m.LoadListFromJsonFile()).Returns(mockList); // Create a fakeMethod of LoadList

            _fakeRepo = new TableRepository(mockService.Object);
        }

        [Fact]
        public void Core_TableService_TableRepository_Create_NotImplementedException_KapacityIsToLarge()
        {
            //Arrange
            CreateTableDto testDto = new CreateTableDto(99, "Errortable");
            //Act

            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.Create(testDto));
            Assert.Contains("You exceeded your maximum capacity limit. Delete or edit some tables.", ex.Message);
        }

        [Fact]
        public void Core_TableService_TableRepository_UpdateById_NotImplementedException_KapacityIsToLarge()
        {
            //Arrange
            CreateTableDto createDto = new CreateTableDto(3, "Goodtable");
            UpdateTableDto updateDto = new UpdateTableDto(99, "Errortable");
            //Act
            _fakeRepo.Create(createDto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.UpdateById(updateDto, 4));
            Assert.Contains("You exceeded your maximum capacity limit. Delete or edit some tables.", ex.Message);
        }

        [Fact]
        public void Core_TableService_TableRepository_UpdateById_ArgumentOutOfRangeException_CantUpdateBaseTables()
        {
            //Arrange
            UpdateTableDto updateDto = new UpdateTableDto(2, "Errortable");
            //Act

            //Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeRepo.UpdateById(updateDto, 1));
            Assert.Contains("You can't update the first four base tables", ex.Message);
        }

        [Theory]
        [InlineData(-9)]
        [InlineData(1)]
        [InlineData(9)]
        public void Core_TableService_TableRepository_DeleteById_ArgumentOutOfRangeException_ParameterOutOfRange(int tableId)
        {
            //Arrange

            //Act

            //Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeRepo.DeleteById(tableId));
            Assert.Contains("You either try to delete one of your base tables or you try to delete out of your range", ex.Message);
        }

        [Theory]
        [InlineData(-9)]
        [InlineData(9)]
        public void Core_TableService_TableRepository_GetById_ArgumentOutOfRangeException_ParameterOutOfRange(int tableId)
        {
            //Arrange

            //Act

            //Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeRepo.GetById(tableId));
            Assert.Contains("You try to get a table which is not existing", ex.Message);
        }

    }
}
