using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.TripPlanDtos;

namespace JadooTravel.ViewModels;

public class DefaultBookingViewModel
{
    public List<ResultTripPlanDto> TripPlans { get; set; }

    public List<ResultDestinationDto> Destinations { get; set; }
}