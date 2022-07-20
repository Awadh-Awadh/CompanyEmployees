namespace Entities.LinkModels;

public class LinkCollectionWrapper<T> : LinkResourcesBase<T>
{
    public List<T> Value { get; set; } = new List<T>();

    public LinkCollectionWrapper()
    {
            
    }

    public LinkCollectionWrapper(List<T> value)
    {
        Value = value;
    }
}