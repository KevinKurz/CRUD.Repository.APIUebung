using CRUD.Contracts.DTOs.TableDto;
using CRUD.Contracts.QueryParams;

namespace CRUD.Core.FilterService
{
    public class TableFilterService
    {
        /// <summary>
        /// <paramref name="optionsParameter"/> -> sortBy nedds to have the value "asc"(ascending) or "dsc"(descending)<br/>
        /// <paramref name="optionsParameter"/> -> filter nedds to be one of the properties oy your <see cref="TableDto"/>
        /// </summary>
        /// <param name="optionsParameter"></param>
        /// <param name="tempList"></param>
        /// <returns>
        /// </returns>
        public List<TableDto> Filter(OptionsParameter optionsParameter, List<TableDto> tempList)
        {
            string? filter = optionsParameter.Filter;
            string? sortBy = optionsParameter.SortBy;

            string sortAsc = "asc";
            string sortDsc = "dsc";

            if (string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(sortBy))
            {
                return tempList;
            }

            List<TableDto> filteredList = new List<TableDto>();
            switch (filter)
            {
                case "capacity":
                    foreach (TableDto tableDto in tempList)
                    {
                        TableDto capacityDto = new TableDto
                        {
                            Capacity = tableDto.Capacity,
                        };
                        filteredList.Add(capacityDto);
                    }
                    if (sortBy == sortAsc)
                    {
                        filteredList = filteredList.OrderBy(c => c.Capacity).ToList();
                    }
                    else if (sortBy == sortDsc)
                    {
                        filteredList = filteredList.OrderByDescending(c => c.Capacity).ToList();
                    }
                    return filteredList;

                case "name":
                    foreach (TableDto tableDto in tempList)
                    {
                        TableDto nameDto = new TableDto
                        {
                            Name = tableDto.Name,
                        };
                        filteredList.Add(nameDto);
                    }
                    if (sortBy == sortAsc)
                    {
                        filteredList = filteredList.OrderBy(c => c.Name).ToList();
                    }
                    else if (sortBy == sortDsc)
                    {
                        filteredList = filteredList.OrderByDescending(c => c.Name).ToList();
                    }
                    return filteredList;

                default:
                    return tempList;
            }
        }
    }
}
