using AutoMapper;
using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.TestimonialServices;

public class TestimonialService(IMongoDatabase database, IMapper _mapper, IDatabaseSettings databaseSettings) : ITestimonialService
{
    private readonly IMongoCollection<Testimonial> _testimonialCollection = database.GetCollection<Testimonial>(databaseSettings.TestimonialCollectionName);
    
    public async Task<List<ResultTestimonialDto>> GetAllTestimonialsAsync()
    {
        var values  = await _testimonialCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultTestimonialDto>>(values);
    }

    public async Task CreateTestimonialAsync(CreateTestimonialDto testimonialDto)
    {
        var value = _mapper.Map<Testimonial>(testimonialDto);
        await _testimonialCollection.InsertOneAsync(value);
    }

    public async Task UpdateTestimonialAsync(UpdateTestimonialDto testimonialDto)
    {
        var value = _mapper.Map<Testimonial>(testimonialDto);
        await _testimonialCollection.FindOneAndReplaceAsync(x => x.TestimonialId == testimonialDto.TestimonialId, value);
    }

    public async Task DeleteTestimonialAsync(string id)
    {
        await _testimonialCollection.DeleteOneAsync(x => x.TestimonialId == id);
    }

    public async Task<GetTestimonialByIdDto> GetTestimonialByIdAsync(string id)
    {
        var value = await _testimonialCollection.Find(x => x.TestimonialId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetTestimonialByIdDto>(value);
    }
}