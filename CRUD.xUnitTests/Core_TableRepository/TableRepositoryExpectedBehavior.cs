using CRUD.Core;
using CRUD.Core.Filter;
using CRUD.Core.QueryParams;
using CRUD.Core.Repositories;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.xUnitTests.Core_TableRepository
{
    public class TableRepositoryExpectedBehavior
    {
        private readonly TableRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;

        public TableRepositoryExpectedBehavior()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new TableRepository(_helperClass.MockserviceForDataservice().Object, new PathValidator(_helperClass.MockserviceForDataservice().Object), new TableFilterService());
        }

        [Fact]
        public void Core_Repository_TableRepository_Create_Successfull()
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
        public void Core_Repository_TableRepository_UpdateByID_Successfull()
        {
            //Arrange
            CreateTableDto createDto = new CreateTableDto(1, "BadTable");
            UpdateTableDto updateDto = new UpdateTableDto(1, "GoodTable");
            QueryParameter queryParameter = new QueryParameter(4);
            //Act
            _fakeRepo.Create(createDto);
            _fakeRepo.UpdateById(queryParameter, updateDto);
            TableModel testModel = Mapper.Map(updateDto);
            //Assert
            Assert.Equivalent(testModel, _helperClass.mockList[4]);
        }

        [Fact]
        public void Core_Repository_TableRepository_GetByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            OptionsParameter optionsParameter = new("", "");
            QueryParameter queryParameter = new QueryParameter(4);
            //Act
            _helperClass.mockList.Add(testModel);
            TableDto testDto = (TableDto)_fakeRepo.GetById(queryParameter, optionsParameter);
            //Assert
            Assert.Equivalent(testDto, testModel);
        }

        [Fact]
        public void Core_Repository_TableRepository_GetAll_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable"); ;
            QueryParameter queryParameter = new QueryParameter();
            OptionsParameter optionsParameter = new("", "");
            //Act
            _helperClass.mockList.Add(testModel);
            List<TableDto> testList = (List<TableDto>)_fakeRepo.GetAll(queryParameter, optionsParameter);
            //Assert
            //Assert.Equivalent(_helperClass.mockList, testList);
            Assert.NotNull(testList.Find(o => o.Name == testModel.Name));
        }

        [Fact]
        public void Core_Repository_TableRepository_DeleteByID_Successfull()
        {
            //Arrange
            TableModel testModel = new TableModel(1, "GoodTable");
            QueryParameter queryParameter = new QueryParameter(4);
            //Act
            _helperClass.mockList.Add(testModel);
            _fakeRepo.DeleteById(queryParameter);
            //Assert
            Assert.DoesNotContain(testModel, _helperClass.mockList);
        }

        [Fact]
        public void Core_Repository_TableRepository_DeleteAll_Successfull()
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
