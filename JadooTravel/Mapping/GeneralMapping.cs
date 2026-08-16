using AutoMapper;
using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.FeatureDtos;
using JadooTravel.Dtos.PartnerDtos;
using JadooTravel.Dtos.SubscribeDtos;
using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Dtos.TripPlanDtos;
using JadooTravel.Entities;

namespace JadooTravel.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category,ResultCategoryDto>().ReverseMap();
        CreateMap<Category,CreateCategoryDto>().ReverseMap();
        CreateMap<Category,UpdateCategoryDto>().ReverseMap();
        CreateMap<Category,GetCategoryByIdDto>().ReverseMap();
        
        CreateMap<Destination,ResultDestinationDto>().ReverseMap();
        CreateMap<Destination,CreateDestinationDto>().ReverseMap();
        CreateMap<Destination,UpdateDestinationDto>().ReverseMap();
        CreateMap<Destination,GetDestinationByIdDto>().ReverseMap();
        
        CreateMap<Feature,ResultFeatureDto>().ReverseMap();
        CreateMap<Feature,CreateFeatureDto>().ReverseMap();
        CreateMap<Feature,UpdateFeatureDto>().ReverseMap();
        CreateMap<Feature,GetFeatureByIdDto>().ReverseMap();
        
        CreateMap<TripPlan,ResultTripPlanDto>().ReverseMap();
        CreateMap<TripPlan,CreateTripPlanDto>().ReverseMap();
        CreateMap<TripPlan,UpdateTripPlanDto>().ReverseMap();
        CreateMap<TripPlan,GetTripPlanByIdDto>().ReverseMap();

        CreateMap<Testimonial,ResultTestimonialDto>().ReverseMap();
        CreateMap<Testimonial,CreateTestimonialDto>().ReverseMap();
        CreateMap<Testimonial,UpdateTestimonialDto>().ReverseMap();
        CreateMap<Testimonial,GetTestimonialByIdDto>().ReverseMap();
        
        CreateMap<Partner,ResultPartnerDto>().ReverseMap();
        CreateMap<Partner,CreatePartnerDto>().ReverseMap();
        CreateMap<Partner,UpdatePartnerDto>().ReverseMap();
        CreateMap<Partner,GetPartnerByIdDto>().ReverseMap();
        
        CreateMap<Subscribe,ResultSubscribeDto>().ReverseMap();
        CreateMap<Subscribe,CreateSubscribeDto>().ReverseMap();
        
        CreateMap<Booking, CreateBookingDto>().ReverseMap();
        CreateMap<Booking, ResultBookingDto>().ReverseMap();
    }
}