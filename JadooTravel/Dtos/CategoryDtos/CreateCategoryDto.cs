namespace JadooTravel.Dtos.CategoryDtos;

public class CreateCategoryDto
{
    public string IconUrl { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public bool Status { get; set; }
}