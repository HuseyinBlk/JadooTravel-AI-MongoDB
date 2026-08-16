using AutoMapper;
using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.CategoryServices;

public class CategoryService(IMongoDatabase database, IMapper mapper, IDatabaseSettings databaseSettings) : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection =  database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    
    
    public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
    {
        var values = await _categoryCollection.Find(x => true).ToListAsync();
        return mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var value = mapper.Map<Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(value);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var value = mapper.Map<Category>(categoryDto);
        await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == categoryDto.CategoryId, value);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categoryCollection.DeleteOneAsync( x=> x.CategoryId == id);
    }

    public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
    {
        var value =  await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return mapper.Map<GetCategoryByIdDto>(value);
    }
}