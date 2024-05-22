using CRUD.Repository.Repositories;
using CRUD.Contracts.QueryParams;
using CRUD.Contracts.Queries.TableQuery;
using CRUD.Core;
using CRUD.Core.FilterService;

namespace CRUD.xUnitTests.Core_TableRepository
{
    public class TableRepositoryExpectedErrors
    {
        private readonly TableRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;
        public TableRepositoryExpectedErrors()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new TableRepository(_helperClass.MockserviceForDataservice().Object, new PathValidator(_helperClass.MockserviceForDataservice().Object), new TableFilterService());
        }

        [Fact]
        public void Core_TableService_TableRepository_Create_NotImplementedException_KapacityIsToLarge()
        {
            //Arrange
            int kapacityToLarge = 99;
            CreateTableQuery testDto = new CreateTableQuery(kapacityToLarge, "Errortable");
            //Act

            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.Create(testDto));
            Assert.Contains("You exceeded your maximum capacity limit. Delete or edit some tables.", ex.Message);
        }

        [Fact]
        public void Core_TableService_TableRepository_UpdateById_NotImplementedException_KapacityIsToLarge()
        {
            //Arrange
            int tableIndexToEdit = 4;
            int kapacityToLarge = 99;
            CreateTableQuery createDto = new CreateTableQuery(3, "Goodtable");
            UpdateTableQuery updateDto = new UpdateTableQuery(kapacityToLarge, "Errortable");
            QueryParameter queryParameter = new QueryParameter(tableIndexToEdit);
            //Act
            _fakeRepo.Create(createDto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.UpdateById(queryParameter, updateDto));
            Assert.Contains("You exceeded your maximum capacity limit. Delete or edit some tables.", ex.Message);
        }

        [Fact]
        public void Core_TableService_TableRepository_UpdateById_ArgumentOutOfRangeException_CantUpdateBaseTables()
        {
            //Arrange
            int tableIndexToEdit = 1;
            UpdateTableQuery updateDto = new UpdateTableQuery(tableIndexToEdit, "Errortable");
            QueryParameter queryParameter = new QueryParameter(1);
            //Act

            //Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeRepo.UpdateById(queryParameter, updateDto));
            Assert.Contains("You can't update the first four base tables", ex.Message);
        }

        [Fact]
        public void Core_TableService_TableRepository_DeleteById_ArgumentOutOfRangeException_ParameterOutOfRange()
        {
            //Arrange
            int tableIndexToDelete = 1;
            QueryParameter queryParameter = new QueryParameter(tableIndexToDelete);
            //Act

            //Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeRepo.DeleteById(queryParameter));
            Assert.Contains("You try to delete one of your base tables", ex.Message);
        }
    }
}
