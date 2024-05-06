using CRUD.Core;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using Moq;

namespace CRUD.xUnitTests.Core_QueryValidator
{
    public class QueryValidatorExpectedErrors
    {
        // Möglichkeit eine globale Mockklasse zu erstellen?
        // Mocklist
        private List<TableModel> _mockList = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };
        private QueryValidator _fakeValidator;
        public QueryValidatorExpectedErrors()
        {
            // Mock of JsonService
            Mock<DataService> mockService = new Mock<DataService>(); // You need to mock the Class, which you do not want to be accessed by the test
            mockService.Setup(m => m.SafeList(_mockList)); // Create a fakeMethod of SaveList
            mockService.Setup(m => m.LoadList()).Returns(_mockList); // Create a fakeMethod of LoadList

            _fakeValidator = new QueryValidator(mockService.Object);
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
            _mockList[0].Availability.Add(listFiller);
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
            _mockList[0].Availability.Add(listFiller);
            //Assert
            ArgumentOutOfRangeException ex;
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => _fakeValidator.IsTableRequestQueryValide(tableId));
            Assert.Contains("You are looking out of your available range", ex.Message);
        }
    }
}
