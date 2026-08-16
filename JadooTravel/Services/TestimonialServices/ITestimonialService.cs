using JadooTravel.Dtos.TestimonialDtos;

namespace JadooTravel.Services.TestimonialServices;

public interface ITestimonialService
{
    Task<List<ResultTestimonialDto>> GetAllTestimonialsAsync();
    Task CreateTestimonialAsync(CreateTestimonialDto testimonialDto);
    Task UpdateTestimonialAsync(UpdateTestimonialDto testimonialDto);
    Task DeleteTestimonialAsync(string id);
    Task<GetTestimonialByIdDto> GetTestimonialByIdAsync(string id);
}