namespace UmbracoBridgeApi.DataTransferObjects;

public class DocumentTypeRequestDto
{
    public required string Alias { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Icon { get; set; }
    public bool AllowedAsRoot { get; set; }
    public bool VariesByCulture { get; set; }
    public bool VariesBySegment { get; set; }
    public Collection? Collection { get; set; }
    public bool IsElement { get; set; }
}

public class Collection
{
    public Guid Id { get; set; }
}
