using CRUD.Core;
using CRUD.Core.QueryParams;
using CRUD.Core.Repositories;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.xUnitTests.Core_ReservationRepository
{
    public class ReservationRepositoryExpectedBehavior
    {

        private readonly ReservationRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;
        public ReservationRepositoryExpectedBehavior()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new ReservationRepository(_helperClass.MockserviceForDataservice().Object, new PathValidator(_helperClass.MockserviceForDataservice().Object));
        }

        [Fact]
        public void Core_Repository_ReservationRepository_Create_Successfull()
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
        public void Core_Repository_ReservationRepository_UpdateByID_Successfull()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "1:00", "4:00", "2.1.1001");
            UpdateReservationDto updateDto = new UpdateReservationDto(1, "test", "1:00", "4:00", "3.1.1001");
            QueryParameter queryParameter = new QueryParameter(0, 1);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _helperClass.mockList[0].Availability.Add(createDto);
            _fakeRepo.UpdateById(queryParameter, updateDto);
            ReservationModel testModel = Mapper.Map(updateDto);

            //Assert
            Assert.Equivalent(testModel, _helperClass.mockList[0].Availability[1], true);
        }

        [Fact]
        public void Core_Repository_ReservationRepository_DeleteAll_Successfull()
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
        public void Core_Repository_ReservationRepository_DeletyById_Successfull()
        {
            //Arrange
            int tableId = 0;
            int reservationId = 0;
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            QueryParameter queryParameter = new QueryParameter(tableId, reservationId);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _fakeRepo.DeleteById(queryParameter);
            //Assert
            Assert.DoesNotContain(listFiller, _helperClass.mockList[0].Availability);
        }

        [Fact]
        public void Core_Repository_ReservationRepository_GetAll_Successfull()
        {
            //Arrange
            int tableId = 0;
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            ReservationOptionsParameter optionsParameter = new("", "", "", "", "");
            QueryParameter queryParameter = new QueryParameter(tableId);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            List<ReservationDto> testList = (List<ReservationDto>)_fakeRepo.GetAll(queryParameter, optionsParameter);
            //Assert
            Assert.Equivalent(testList, _helperClass.mockList[0].Availability);
        }

        [Fact]
        public void Core_Repository_ReservationRepository_GetById_Successfull()
        {
            //Arrange
            int tableId = 0;
            int reservationId = 0;
            ReservationModel listFiller = new ReservationModel(1, "test", "1:00", "4:00", "1.1.1001");
            QueryParameter queryParameter = new QueryParameter(tableId, reservationId);
            ReservationOptionsParameter optionsParameter = new("", "", "", "", "");
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            ReservationDto shouldBeDto = Mapper.Map(listFiller);
            ReservationDto testDto = (ReservationDto)_fakeRepo.GetById(queryParameter, optionsParameter);
            //Assert
            Assert.Equivalent(shouldBeDto, testDto);
        }
    }
}
