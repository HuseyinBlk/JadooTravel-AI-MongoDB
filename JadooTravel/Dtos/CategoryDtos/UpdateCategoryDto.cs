namespace JadooTravel.Dtos.CategoryDtos;

public class UpdateCategoryDto
{
    public string CategoryId { get; set; }
    public string IconUrl { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public bool Status { get; set; }
}