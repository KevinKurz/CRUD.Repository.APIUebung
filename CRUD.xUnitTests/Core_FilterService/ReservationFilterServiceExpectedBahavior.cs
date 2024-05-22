using FluentAssertions;
using CRUD.Core.FilterService;
using CRUD.Contracts.DTOs.ReservationDto;
using CRUD.Contracts.QueryParams;

namespace CRUD.xUnitTests.Core_FilterService
{
    public class ReservationFilterServiceExpectedBahavior
    {
        private readonly ReservationFilterService _filterService;
        public ReservationFilterServiceExpectedBahavior()
        {
            _filterService = new ReservationFilterService();
        }

        public static IEnumerable<object[]> ListCapacityOnly()
        {
            yield return new object[]
            {
                //default
                new List<ReservationDto>
                {
                    new ReservationDto(1,null,null,null,null),
                    new ReservationDto(8,null,null,null,null),
                    new ReservationDto(2,null,null,null,null)
                },
                //asc
                new List<ReservationDto>
                {
                    new ReservationDto(1,null,null,null,null),
                    new ReservationDto(2,null,null,null,null),
                    new ReservationDto(8,null,null,null,null)
                },
                //dsc
                new List<ReservationDto>
                {
                    new ReservationDto(8,null,null,null,null),
                    new ReservationDto(2,null,null,null,null),
                    new ReservationDto(1,null,null,null,null)
                },
                "capacity",
                new string[] {"","asc","dsc"},

            };
        }
        public static IEnumerable<object[]> ListNameOnly()
        {
            yield return new object[]
            {
                //default
                new List<ReservationDto>
                {
                    new ReservationDto(0,"a",null,null,null),
                    new ReservationDto(0,"c",null,null,null),
                    new ReservationDto(0,"b",null,null,null)
                },
                //asc
                new List<ReservationDto>
                {
                    new ReservationDto(0,"a",null,null,null),
                    new ReservationDto(0,"b",null,null,null),
                    new ReservationDto(0,"c",null,null,null)
                },
                //dsc
                new List<ReservationDto>
                {
                    new ReservationDto(0,"c",null,null,null),
                    new ReservationDto(0,"b",null,null,null),
                    new ReservationDto(0,"a",null,null,null)
                },
                "lastName",
                new string[] {"","asc","dsc"}
            };
        }
        public static IEnumerable<object[]> ListStarttimeOnly()
        {
            yield return new object[]
            {
                //default
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,"a",null,null),
                    new ReservationDto(0,null,"c",null,null),
                    new ReservationDto(0,null,"b",null,null)
                },
                //asc
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,"a",null,null),
                    new ReservationDto(0,null,"b",null,null),
                    new ReservationDto(0,null,"c",null,null)
                },
                //dsc
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,"c",null,null),
                    new ReservationDto(0,null,"b",null,null),
                    new ReservationDto(0,null,"a",null,null)
                },
                "startTime",
                new string[] {"","asc","dsc"}
            };
        }
        public static IEnumerable<object[]> ListEndtimeOnly()
        {
            yield return new object[]
            {
                //default
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,null,"a",null),
                    new ReservationDto(0,null,null,"c",null),
                    new ReservationDto(0,null,null,"b",null)
                },
                //asc
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,null,"a",null),
                    new ReservationDto(0,null,null,"b",null),
                    new ReservationDto(0,null,null,"c",null)
                },
                //dsc
                new List<ReservationDto>
                {
                    new ReservationDto(0,null,null,"c",null),
                    new ReservationDto(0,null,null,"b",null),
                    new ReservationDto(0,null,null,"a",null)
                },
                "endTime",
                new string[] {"","asc","dsc"}
            };
        }

        [Fact]
        public void Core_Filter_ReservationFilterService_ReturnsSameList()
        {
            //Arrange
            string? filter = null;
            string? sortBy = null;
            List<ReservationDto> testListReturnNull = new List<ReservationDto>
            {
                new ReservationDto(1,"","","",""),
                new ReservationDto(2,"","","",""),
                new ReservationDto(8,"","","","")
            };
            OptionsParameter optionsParameter = new OptionsParameter(filter, sortBy);
            //Act
            List<ReservationDto> shouldList = _filterService.Filter(optionsParameter, testListReturnNull);
            //Assert
            Assert.Equal(testListReturnNull[0].Capacity, shouldList[0].Capacity);
        }

        [Theory]
        [MemberData(nameof(ListCapacityOnly))]
        [MemberData(nameof(ListNameOnly))]
        [MemberData(nameof(ListStarttimeOnly))]
        [MemberData(nameof(ListEndtimeOnly))]
        public void Core_Filter_ReservationFilterService_FilteredByPropertyAndSorted(List<ReservationDto> testList, List<ReservationDto> ascendingList, List<ReservationDto> descendingList, string? filterInput, string[] sortyByInput)
        {
            //Arrange
            string? filter = filterInput;
            string? sortBy;
            //Act
            sortBy = sortyByInput[0];
            OptionsParameter optionsParameter = new OptionsParameter(filter, sortBy);
            List<ReservationDto> sortedListDefault = _filterService.Filter(optionsParameter, testList);
            sortBy = sortyByInput[1];
            optionsParameter = new OptionsParameter(filter, sortBy);
            List<ReservationDto> sortedListAscending = _filterService.Filter(optionsParameter, testList);
            sortBy = sortyByInput[2];
            optionsParameter = new OptionsParameter(filter, sortBy);
            List<ReservationDto> sortedListDescending = _filterService.Filter(optionsParameter, testList);
            //Assert
            sortedListDefault.Should().BeEquivalentTo(testList);
            sortedListAscending.Should().BeEquivalentTo(ascendingList);
            sortedListDescending.Should().BeEquivalentTo(descendingList);
        }
    }
}
