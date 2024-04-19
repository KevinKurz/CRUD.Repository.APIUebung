using CRUD.DataStructures.DTOs.ReservationDTO;
using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.AttributeService;

namespace CRUD.xUnitTests
{
    public class CreateReservationDtoExpectedErrors
    {
        CreateReservationDto testDto = new CreateReservationDto();

        [Fact]
        public void CreateReservationDto_Kapacity_IsValid()
        {
            //Arrange
            
            //Act
            testDto.Kapacity = -2;
            testDto.StartTime = "00:00";
            testDto.EndTime = "00:01";
            testDto.LastName = "";
            testDto.Date = "1.1.1001";
            //Assert
            Assert.Throws<ValidationException>(() => testDto.IsValid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("11111111111111111111111")]
        public void CreateReservationDto_LastName_IsValid(string? lastName)
        {
            //Arrange

            //Act
            testDto.Kapacity = 1;
            testDto.StartTime = "00:00";
            testDto.EndTime = "00:01";
            testDto.LastName = lastName;
            testDto.Date = "1.1.1001";
            //Assert
            Assert.Throws<ValidationException>(() => testDto.IsValid());
        }
    }
}