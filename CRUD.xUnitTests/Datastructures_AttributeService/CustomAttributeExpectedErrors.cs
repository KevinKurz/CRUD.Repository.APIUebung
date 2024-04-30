using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.AttributeService;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.xUnitTests.Datastructures_AttributeService
{
    public class CustomAttributeExpectedErrors
    {
        /// <summary>
        /// Checks if <see cref="DateValidationAttribute"/> is working
        /// </summary>
        [Fact]
        public void DataStructures_AttributeService_DateValidationAttribute_IsAttributeValid()
        {
            string testDate = "no_DateOnly";
            //Arrange
            ReservationDto dto = new ReservationDto(1, "TestName", "00:00", "01:00", testDate);
            //Act

            //Assert
            ValidationException ex = Assert.Throws<ValidationException>(() => dto.IsValid());
            Assert.Contains("Your input is not DateOnly convertable", ex.Message);
        }

        /// <summary>
        /// Checks if <see cref="EndtimeEarlierThanStarttimeAttribute"/> is working
        /// </summary>
        [Fact]
        public void DataStructures_AttributeService_EndtimeEarlierThanStarttimeAttribute_IsStarttimeInTimeonly()
        {
            string testTime = "no_TimeOnly";
            //Arrange
            ReservationDto dto = new ReservationDto(1, "TestName", testTime, "1:00", "1.1.1001");
            //Act

            //Assert
            ValidationException ex = Assert.Throws<ValidationException>(() => dto.IsValid());
            Assert.Contains("Your input is not TimeOnly convertable", ex.Message);
        }

        [Fact]
        public void DataStructures_AttributeService_EndtimeEarlierThanStarttimeAttribute_IsEndtimeInTimeonly()
        {
            string testTime = "no_TimeOnly";
            //Arrange
            ReservationDto dto = new ReservationDto(1, "TestName", "1:00", testTime, "1.1.1001");
            //Act

            //Assert
            ValidationException ex = Assert.Throws<ValidationException>(() => dto.IsValid());
            Assert.Contains("Your input is not TimeOnly convertable", ex.Message);
        }

        [Fact]
        public void DataStructures_AttributeService_EndtimeEarlierThanStarttimeAttribute_IsTimecomparisonValid()
        {
            string startTime = "2:00";
            string endTime = "1:00";
            //Arrange
            ReservationDto dto = new ReservationDto(1, "TestName", startTime, endTime, "1.1.1001");
            //Act

            //Assert
            ValidationException ex = Assert.Throws<ValidationException>(() => dto.IsValid());
            Assert.Contains("Your starttime is not allowed to start after your endtime", ex.Message);
        }

        /// <summary>
        /// Checks if IsValid Method is working
        /// </summary>
        [Fact]
        public void DataStructures_AttributeService_Validator_IsIsValidMethodWorking()
        {
            int kapacity = 12;
            //Arrange
            ReservationDto dto = new ReservationDto(kapacity, "TestName", "1:00", "2:00", "1.1.1001");
            //Act

            //Assert
            Assert.Throws<ValidationException>(() => dto.IsValid());
        }
    }
}
