using System.Collections.ObjectModel;

namespace doska.DTO;

public class PostEditRequest
{
    public Guid PostId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int Price { get; set; } = default!;
    public ReadOnlyCollection<Guid>? IdsToRemove { get; set; } = default!;
    public ICollection<IFormFile>? Pictures { get; set; } = default!;
}