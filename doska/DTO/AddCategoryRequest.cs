namespace doska.DTO;

public class AddCategoryRequest
{
    public string Name { get; set; } = default!;
    public Guid? ParentId { get; set; }
}