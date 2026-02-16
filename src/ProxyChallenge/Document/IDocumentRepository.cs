namespace ProxyChallenge.Document;

public interface IDocumentRepository
{
    ConfidentialDocument? GetDocument(string documentId);
    void UpdateDocument(string documentId, string newContent);
}
