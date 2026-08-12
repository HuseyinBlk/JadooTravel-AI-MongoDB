namespace JadooTravel.Dtos.CategoryDtos;

public class GetCategoryByIdDto
{
    public string CategoryId { get; set; }
    public string IconUrl { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public bool Status { get; set; }
}