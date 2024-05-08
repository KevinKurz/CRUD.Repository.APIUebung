using CRUD.Core;
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
        [InlineData(2, 0)]
        public void Core_QueryValidator_IsReservationRequestQueryValide_ArgumentOutOfRangeException(int tableId, int rerservationId)
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "2:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            //Assert
            ArgumentOutOfRangeException ex;
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeValidator.IsReservationRequestQueryValide(tableId, rerservationId));
            Assert.Contains("You are looking out of your available range", ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Core_QueryValidator_IsTableRequestQueryValide_ArgumentOutOfRangeException(int tableId)
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "2:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            //Assert
            ArgumentOutOfRangeException ex;
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeValidator.IsTableRequestQueryValide(tableId));
            Assert.Contains("You are looking out of your available range", ex.Message);
        }
    }
}
