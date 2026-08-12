using AutoMapper;
using JadooTravel.Dtos.CategoryDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }
    
    public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
    {
        var values = await _categoryCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var value = _mapper.Map<Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(value);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var value = _mapper.Map<Category>(categoryDto);
        await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == categoryDto.CategoryId, value);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categoryCollection.DeleteOneAsync( x=> x.CategoryId == id);
    }

    public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
    {
        var value =  await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetCategoryByIdDto>(value);
    }
}