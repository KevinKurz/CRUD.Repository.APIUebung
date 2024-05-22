using CRUD.Contracts.DTOs.TableDto;
using CRUD.Contracts.QueryParams;
using CRUD.Core.FilterService;
using FluentAssertions;

namespace CRUD.xUnitTests.Core_FilterService
{
    public class TableFilterServiceExpectedBehavior
    {
        private readonly TableFilterService _filterService;
        public TableFilterServiceExpectedBehavior()
        {
            _filterService = new TableFilterService();
        }

        public static IEnumerable<object[]> ListCapacityOnly()
        {
            yield return new object[]
            {
                //default
                new List<TableDto>()
                {
                    new TableDto(1,null),
                    new TableDto(8,null),
                    new TableDto(2,null)
                },
                //asc
                new List<TableDto>()
                {
                    new TableDto(1,null),
                    new TableDto(2,null),
                    new TableDto(8,null)
                },
                //dsc
                new List<TableDto>()
                {
                    new TableDto(8,null),
                    new TableDto(2,null),
                    new TableDto(1,null)
                },
                "capacity",
                new string[] {"", "asc", "dsc"}
            };
        }
        public static IEnumerable<object[]> ListNameOnly()
        {
            yield return new object[]
            {
                //default
                new List<TableDto>()
                {
                    new TableDto(0,"b"),
                    new TableDto(0,"c"),
                    new TableDto(0,"a")
                },
                //asc
                new List<TableDto>()
                {
                    new TableDto(0,"a"),
                    new TableDto(0,"b"),
                    new TableDto(0,"c")
                },
                //dsc
                new List<TableDto>()
                {
                    new TableDto(0,"c"),
                    new TableDto(0,"b"),
                    new TableDto(0,"a")
                },
                "name",
                new string[] {"", "asc", "dsc"}
            };
        }

        [Fact]
        public void Core_Filter_TableFilterService_ReturnsNull()
        {
            //Arrange
            string? filter = null;
            string? sortBy = null;
            List<TableDto> testListReturnNull = new List<TableDto>
            {
                new TableDto(1,""),
                new TableDto(2,""),
                new TableDto(8,"")
            };
            OptionsParameter optionsParameter = new OptionsParameter(filter, sortBy);
            //Act
            List<TableDto> shouldList = _filterService.Filter(optionsParameter, testListReturnNull);
            //Assert
            Assert.Equal(testListReturnNull[0].Capacity, shouldList[0].Capacity);
        }

        [Theory]
        [MemberData(nameof(ListCapacityOnly))]
        [MemberData(nameof(ListNameOnly))]
        public void Core_Filter_TableFilterService_FilteredByPropertyAndSorted(List<TableDto> testList, List<TableDto> ascendingList, List<TableDto> descendingList, string? filterInput, string[] sortyByInput)
        {
            //Arrange
            string? filter = filterInput;
            string? sortBy;
            //Act
            sortBy = sortyByInput[0];
            OptionsParameter optionsParameter = new OptionsParameter(filter, sortBy);
            List<TableDto> sortedListDefault = _filterService.Filter(optionsParameter, testList);
            sortBy = sortyByInput[1];
            optionsParameter = new OptionsParameter(filter, sortBy);
            List<TableDto> sortedListAscending = _filterService.Filter(optionsParameter, testList);
            sortBy = sortyByInput[2];
            optionsParameter = new OptionsParameter(filter, sortBy);
            List<TableDto> sortedListDescending = _filterService.Filter(optionsParameter, testList);
            //Assert
            sortedListDefault.Should().BeEquivalentTo(testList);
            sortedListAscending.Should().BeEquivalentTo(ascendingList);
            sortedListDescending.Should().BeEquivalentTo(descendingList);
        }
    }
}
