using CRUD.Core;
using CRUD.Core.QueryParams;
using CRUD.Core.Repositories;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.xUnitTests.Core_ReservationRepository
{
    public class ReservationRepositoryExpectedErrors
    {
        private readonly ReservationRepository _fakeRepo;
        private readonly MockConfigurator _helperClass;
        public ReservationRepositoryExpectedErrors()
        {
            _helperClass = new MockConfigurator();
            _fakeRepo = new ReservationRepository(_helperClass.MockserviceForDataservice().Object, new PathValidator(_helperClass.MockserviceForDataservice().Object)); // Crate a new instance of ReservationRepository with the "Fake-Jason-Service-class"
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_Create_NotimplementedException_ForStartime()
        {
            //Arrange
            CreateReservationDto dto = new CreateReservationDto(1, "test", "1:00", "4:00", "1.1.1001");
            CreateReservationDto dtoBetween = new CreateReservationDto(1, "test", "2:00", "5:00", "1.1.1001");
            //Act
            _fakeRepo.Create(dto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.Create(dtoBetween));
            Assert.Contains("Your starttime is in range with an already existing reservation", ex.Message);
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_Create_NotimplementedException_ForEndtime()
        {
            //Arrange
            CreateReservationDto dto = new CreateReservationDto(1, "test", "2:00", "6:00", "1.1.1001");
            CreateReservationDto dtoBetween = new CreateReservationDto(1, "test", "1:00", "5:00", "1.1.1001");
            //Act
            _fakeRepo.Create(dto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.Create(dtoBetween));
            Assert.Contains("Your endtime is in range with an already existing reservation", ex.Message);
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_Create_NotimplementedException_ModelSurroundsReservation()
        {
            //Arrange
            CreateReservationDto dto = new CreateReservationDto(1, "test", "2:00", "6:00", "1.1.1001");
            CreateReservationDto dtoBetween = new CreateReservationDto(1, "test", "1:00", "7:00", "1.1.1001");
            //Act
            _fakeRepo.Create(dto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.Create(dtoBetween));
            Assert.Contains("Your reservation surrounds an already existing reservation", ex.Message);
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_UpdateById_NotimplementedException_ModelSurroundsReservation()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "2:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "1:00", "4:00", "2.1.1001");
            UpdateReservationDto updateDto = new UpdateReservationDto(1, "test", "1:00", "5:00", "1.1.1001");
            QueryParameter queryParameter = new QueryParameter(0, 1);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _helperClass.mockList[0].Availability.Add(createDto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.UpdateById(queryParameter, updateDto));
            Assert.Contains("Your reservation surrounds an already existing reservation", ex.Message);
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_UpdateById_NotimplementedException_ForStarttime()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "2:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "2:00", "5:00", "2.1.1001");
            UpdateReservationDto UpdateDto = new UpdateReservationDto(1, "test", "3:00", "5:00", "1.1.1001");
            QueryParameter queryParameter = new QueryParameter(0, 1);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _helperClass.mockList[0].Availability.Add(createDto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.UpdateById(queryParameter, UpdateDto));
            Assert.Contains("Your starttime collides with already existing reservationtimes", ex.Message);
        }

        [Fact]
        public void CORE_Repository_ReservationRepository_UpdateById_NotimplementedException_ForEndtime()
        {
            //Arrange
            ReservationModel listFiller = new ReservationModel(1, "test", "2:00", "4:00", "1.1.1001");
            ReservationModel createDto = new ReservationModel(1, "test", "2:00", "5:00", "2.1.1001");
            UpdateReservationDto UpdateDto = new UpdateReservationDto(1, "test", "1:00", "3:00", "1.1.1001");
            QueryParameter queryParameter = new QueryParameter(0, 1);
            //Act
            _helperClass.mockList[0].Availability.Add(listFiller);
            _helperClass.mockList[0].Availability.Add(createDto);
            //Assert
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => _fakeRepo.UpdateById(queryParameter, UpdateDto));
            Assert.Contains("Your endtime collides with already existing reservationtimes", ex.Message);
        }
    }
}
