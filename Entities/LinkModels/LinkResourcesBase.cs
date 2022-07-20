namespace Entities.LinkModels;

public class LinkResourcesBase<T>
{
    public LinkResourcesBase()
    {
    }

    public List<Link> Links { get; set; } = new();
}