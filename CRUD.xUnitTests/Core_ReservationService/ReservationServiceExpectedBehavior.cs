using CRUD.Core.ReservationService;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.ReservationDTO;
using Moq;

namespace CRUD.xUnitTests.Core_ReservationService
{
    public class ReservationServiceExpectedBehavior
    {
        // Mocklist
        private List<TableModel> _mockList = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };
        private ReservationRepository _fakeRepo;
        public ReservationServiceExpectedBehavior()
        {
            // Mock of JsonService
            Mock<DataService> mockService = new Mock<DataService>(); // You need to mock the Class, which you do not want to be accessed by the test
            mockService.Setup(m => m.SafeList(_mockList)); // Create a fakeMethod of SaveList
            mockService.Setup(m => m.LoadList()).Returns(_mockList); // Create a fakeMethod of LoadList

            _fakeRepo = new ReservationRepository(mockService.Object); // Crate a new instance of ReservationRepository with the "Fake-Jason-Service-class"
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_Create_Successfull()
        {
            //Arrange
            CreateReservationDto testDto = new CreateReservationDto(1, "test", "2:00", "4:00", "1.1.1001");
            //Act
            _fakeRepo.Create(testDto);
            ReservationModel testModel = Mapper.Map(testDto);
            //Assert
            Assert.Equivalent(testModel, _mockList[0].Availability[0], true);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_Update_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "1:00", "4:00", "2.1.1001");
            UpdateReservationDto updateDto = new UpdateReservationDto(1, "test", "1:00", "4:00", "3.1.1001");
            //Act
            _mockList[0].Availability.Add(listFiller);
            _mockList[0].Availability.Add(createDto);
            _fakeRepo.UpdateById(0, 1, updateDto);
            ReservationModel testModel = Mapper.Map(updateDto);

            //Assert
            Assert.Equivalent(testModel, _mockList[0].Availability[1], true);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_DeleteAll_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _mockList[0].Availability.Add(listFiller);
            _fakeRepo.DeleteAll();
            //Assert
            Assert.DoesNotContain(listFiller, _mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_DeletyById_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _mockList[0].Availability.Add(listFiller);
            _fakeRepo.DeleteById(0, 0);
            //Assert
            Assert.DoesNotContain(listFiller, _mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_GetAll_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _mockList[0].Availability.Add(listFiller);
            List<ReservationDto> testList = (List<ReservationDto>)_fakeRepo.GetAll(0);
            //Assert
            Assert.Equivalent(testList, _mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_GetById_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _mockList[0].Availability.Add(listFiller);
            ReservationDto shouldBeDto = Mapper.Map(listFiller);
            ReservationDto testDto = (ReservationDto)_fakeRepo.GetById(0, 0);
            //Assert
            Assert.Equivalent(shouldBeDto, testDto);
        }
    }
}
