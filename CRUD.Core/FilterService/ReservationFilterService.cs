using CRUD.Core.QueryParams;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.Core.FilterService
{
    public class ReservationFilterService
    {
        /// <summary>
        /// <paramref name="optionsParameter"/> -> sortBy nedds to have the value "asc"(ascending) or "dsc"(descending)<br/>
        /// <paramref name="optionsParameter"/> -> filter nedds to be one of the properties oy your <see cref="ReservationDto"/>
        /// </summary>
        /// <param name="optionsParameter"></param>
        /// <param name="tempList"></param>
        /// <returns></returns>
        public List<ReservationDto> Filter(OptionsParameter optionsParameter, List<ReservationDto> tempList)
        {
            string? filter = optionsParameter.Filter;
            string? sortBy = optionsParameter.SortBy;

            string sortAsc = "asc";
            string sortDsc = "dsc";

            if (string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(sortBy))
            {
                return tempList;
            }

            List<ReservationDto> filteredList = new List<ReservationDto>();
            switch (filter)
            {
                case "lastName":
                    foreach (ReservationDto tableDto in tempList)
                    {
                        ReservationDto lastNameDto = new ReservationDto
                        {
                            LastName = tableDto.LastName,
                        };
                        filteredList.Add(lastNameDto);
                    }
                    if (sortBy == sortAsc)
                    {
                        filteredList = filteredList.OrderBy(c => c.LastName).ToList();
                    }
                    else if (sortBy == sortDsc)
                    {
                        filteredList = filteredList.OrderByDescending(c => c.LastName).ToList();
                    }
                    return filteredList;

                case "startTime":
                    foreach (ReservationDto tableDto in tempList)
                    {
                        ReservationDto lastNameDto = new ReservationDto
                        {
                            StartTime = tableDto.StartTime,
                        };
                        filteredList.Add(lastNameDto);
                    }
                    if (sortBy == sortAsc)
                    {
                        filteredList = filteredList.OrderBy(c => c.StartTime).ToList();
                    }
                    else if (sortBy == sortDsc)
                    {
                        filteredList = filteredList.OrderByDescending(c => c.StartTime).ToList();
                    }
                    return filteredList;

                case "endTime":
                    foreach (ReservationDto tableDto in tempList)
                    {
                        ReservationDto lastNameDto = new ReservationDto
                        {
                            EndTime = tableDto.EndTime,
                        };
                        filteredList.Add(lastNameDto);
                    }
                    if (sortBy == sortAsc)
                    {
                        filteredList = filteredList.OrderBy(c => c.EndTime).ToList();
                    }
                    else if (sortBy == sortDsc)
                    {
                        filteredList = filteredList.OrderByDescending(c => c.EndTime).ToList();
                    }
                    return filteredList;

                case "capacity":
                    foreach (ReservationDto tableDto in tempList)
                    {
                        ReservationDto lastNameDto = new ReservationDto
                        {
                            Capacity = tableDto.Capacity,
                        };
                        filteredList.Add(lastNameDto);
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

                default:
                    return tempList;
            }
        }

    }
}
