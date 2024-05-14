using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using Moq;

namespace CRUD.xUnitTests
{
    public class MockConfigurator
    {
        public List<TableModel> mockList = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };

        public Mock<DataService> MockserviceForDataservice()
        {
            // Mock of Dataservice
            Mock<DataService> mockService = new Mock<DataService>(); // You need to mock the Class, which you do not want to be accessed directly by the test
            mockService.Setup(m => m.SafeList(mockList)); // Create a fakeMethod of SaveList
            mockService.Setup(m => m.LoadList()).Returns(mockList); // Create a fakeMethod of LoadList

            return mockService;
        }
    }
}
