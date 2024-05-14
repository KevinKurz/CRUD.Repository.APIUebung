using CRUD.Core;
using CRUD.Core.QueryParams;
using CRUD.DataStructures.DataModel;

namespace CRUD.xUnitTests.Core_PathValidator
{
    public class PathValidatorExpectedErrors
    {
        private readonly PathValidator _fakeValidator;
        private readonly MockConfigurator _helperClass;
        public PathValidatorExpectedErrors()
        {
            _helperClass = new MockConfigurator();
            _fakeValidator = new PathValidator(_helperClass.MockserviceForDataservice().Object);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(0, 2)]
        [InlineData(-1, 0)]
        [InlineData(7, 0)]
        public void Core_QueryValidator_IsPathParameterValide_ArgumentOutOfRangeException(int tableId, int rerservationId)
        {
            //Arrange
            int capacity = 1;
            ReservationModel listFiller = new ReservationModel(capacity, "test", "2:00", "4:00", "1.1.1001");
            QueryParameter pathParameter = new QueryParameter(tableId, rerservationId);

            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            //Assert
            ArgumentOutOfRangeException ex;
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeValidator.IsPathParameterValide(pathParameter));
            Assert.Contains("You are looking out of your available range", ex.Message);
        }
    }
}
