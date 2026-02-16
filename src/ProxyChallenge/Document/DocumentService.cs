using ProxyChallenge.User;

namespace ProxyChallenge.Document;

public interface IDocumentService
{
    ConfidentialDocument? ViewDocument(string documentId, User.User user);
    void EditDocument(string documentId, User.User user, string newContent);
}

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;

    public DocumentService(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public ConfidentialDocument? ViewDocument(string documentId, User.User user)
    {
        return _repository.GetDocument(documentId);
    }

    public void EditDocument(string documentId, User.User user, string newContent)
    {
        _repository.UpdateDocument(documentId, newContent);
    }
}
