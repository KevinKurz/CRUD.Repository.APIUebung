using CRUD.Core.TableService;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;
using Moq;

namespace CRUD.xUnitTests.Core_TableService
{
    public class TableServiceExpectedBehavior
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
        public TableServiceExpectedBehavior()
        {
            // Mock of JsonService
            Mock<JsonService> mockService = new Mock<JsonService>(); // You need to mock the Class, which you do not want to be accessed by the test
            mockService.Setup(m => m.SaveListAsJsonFile(mockList)); // Create a fakeMethod of SaveList
            mockService.Setup(m => m.LoadListFromJsonFile()).Returns(mockList); // Create a fakeMethod of LoadList

            _fakeRepo = new TableRepository(mockService.Object);
        }

        [Fact]
        public void Core_TableService_TableRepository_Create_Successfull()
        {
            //Arrange
            CreateTableDto testDto = new CreateTableDto(1, "GoodTable");
            //Act
            _fakeRepo.Create(testDto);
            TableModel testModel = Mapper.Map(testDto);
            //Assert
            Assert.Equivalent(testModel, mockList[^1]); // [^1] is equal to the maximum count -1
        }

        [Fact]
        public void Core_TableService_TableRepository_UpdateByID_Successfull()
        {
            //Arrange
            CreateTableDto createDto = new CreateTableDto(1, "BadTable");
            UpdateTableDto updateDto = new UpdateTableDto(1, "GoodTable");
            //Act
            _fakeRepo.Create(createDto);
            _fakeRepo.UpdateById(updateDto, 4);
            TableModel testModel = Mapper.Map(updateDto);
            //Assert
            Assert.Equivalent(testModel, mockList[4]);
        }

        [Fact]
        public void Core_TableService_TableRepository_GetByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            mockList.Add(testModel);
            TableDto testDto = (TableDto)_fakeRepo.GetById(4);
            //Assert
            Assert.Equivalent(testDto, testModel);
        }

        [Fact]
        public void Core_TableService_TableRepository_GetAll_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            mockList.Add(testModel);
            List<TableDto> testList = (List<TableDto>)_fakeRepo.GetAll();
            //Assert
            Assert.Equivalent(testList, mockList);
        }

        [Fact]
        public void Core_TableService_TableRepository_DeleteByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            mockList.Add(testModel);
            _fakeRepo.DeleteById(4);
            //Assert
            Assert.DoesNotContain(testModel, mockList);
        }

        [Fact]
        public void Core_TableService_TableRepository_DeleteAll_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            mockList.Add(testModel);
            _fakeRepo.DeleteAll();
            //Assert
            Assert.DoesNotContain(testModel, mockList);
        }
    }
}
