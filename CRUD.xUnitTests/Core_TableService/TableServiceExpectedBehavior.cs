using CRUD.Core.Repositories;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.xUnitTests.Core_TableService
{
    public class TableServiceExpectedBehavior
    {
        private readonly TableRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;

        public TableServiceExpectedBehavior()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new TableRepository(_helperClass.MockserviceForDataservice().Object);
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
            Assert.Equivalent(testModel, _helperClass.mockList[^1]); // [^1] is equal to the maximum count -1
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
            Assert.Equivalent(testModel, _helperClass.mockList[4]);
        }

        [Fact]
        public void Core_TableService_TableRepository_GetByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            _helperClass.mockList.Add(testModel);
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
            _helperClass.mockList.Add(testModel);
            List<TableDto> testList = (List<TableDto>)_fakeRepo.GetAll();
            //Assert
            Assert.Equivalent(testList, _helperClass.mockList);
        }

        [Fact]
        public void Core_TableService_TableRepository_DeleteByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            _helperClass.mockList.Add(testModel);
            _fakeRepo.DeleteById(4);
            //Assert
            Assert.DoesNotContain(testModel, _helperClass.mockList);
        }

        [Fact]
        public void Core_TableService_TableRepository_DeleteAll_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            //Act
            _helperClass.mockList.Add(testModel);
            _fakeRepo.DeleteAll();
            //Assert
            Assert.DoesNotContain(testModel, _helperClass.mockList);
        }
    }
}
