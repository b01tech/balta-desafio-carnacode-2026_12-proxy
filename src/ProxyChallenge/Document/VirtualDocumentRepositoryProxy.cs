namespace ProxyChallenge.Document;

public class VirtualDocumentRepositoryProxy : IDocumentRepository
{
    private readonly Lazy<DocumentRepository> _lazy;

    public VirtualDocumentRepositoryProxy()
    {
        _lazy = new Lazy<DocumentRepository>(() => new DocumentRepository());
    }

    private DocumentRepository Inner => _lazy.Value;

    public ConfidentialDocument? GetDocument(string documentId)
    {
        return Inner.GetDocument(documentId);
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        Inner.UpdateDocument(documentId, newContent);
    }
}
