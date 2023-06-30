namespace doska.DTO;

public class EditCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid? ParentId { get; set; }
}