using CRUD.Core.Repositories;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.xUnitTests.Core_ReservationService
{
    public class ReservationServiceExpectedBehavior
    {

        private readonly ReservationRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;
        public ReservationServiceExpectedBehavior()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new ReservationRepository(_helperClass.MockserviceForDataservice().Object);
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
            Assert.Equivalent(testModel, _helperClass.mockList[0].Availability[0], true);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_Update_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "1:00", "4:00", "2.1.1001");
            UpdateReservationDto updateDto = new UpdateReservationDto(1, "test", "1:00", "4:00", "3.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _helperClass.mockList[0].Availability.Add(createDto);
            _fakeRepo.UpdateById(0, 1, updateDto);
            ReservationModel testModel = Mapper.Map(updateDto);

            //Assert
            Assert.Equivalent(testModel, _helperClass.mockList[0].Availability[1], true);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_DeleteAll_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _fakeRepo.DeleteAll();
            //Assert
            Assert.DoesNotContain(listFiller, _helperClass.mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_DeletyById_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _fakeRepo.DeleteById(0, 0);
            //Assert
            Assert.DoesNotContain(listFiller, _helperClass.mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_GetAll_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            List<ReservationDto> testList = (List<ReservationDto>)_fakeRepo.GetAll(0);
            //Assert
            Assert.Equivalent(testList, _helperClass.mockList[0].Availability);
        }

        [Fact]
        public void Core_ReservationService_ReservationRepository_GetById_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            ReservationDto shouldBeDto = Mapper.Map(listFiller);
            ReservationDto testDto = (ReservationDto)_fakeRepo.GetById(0, 0);
            //Assert
            Assert.Equivalent(shouldBeDto, testDto);
        }
    }
}
