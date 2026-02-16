using ProxyChallenge.Document;
using AppUser = ProxyChallenge.User.User;

namespace ProxyChallenge.Services;

public class AuthServiceProxy : IDocumentService
{
    private readonly IDocumentService _inner;
    private readonly IDocumentRepository _repository;

    public AuthServiceProxy(IDocumentService inner, IDocumentRepository repository)
    {
        _inner = inner;
        _repository = repository;
    }

    public ConfidentialDocument? ViewDocument(string documentId, AppUser user)
    {
        var doc = _repository.GetDocument(documentId);
        if (doc == null)
        {
            Console.WriteLine($"❌ Documento {documentId} não encontrado");
            return null;
        }

        if (user.ClearanceLevel < doc.SecurityLevel)
        {
            Console.WriteLine(
                $"❌ Acesso negado! Nível {user.ClearanceLevel} < Requerido {doc.SecurityLevel}"
            );
            return null;
        }

        return _inner.ViewDocument(documentId, user);
    }

    public void EditDocument(string documentId, AppUser user, string newContent)
    {
        var doc = _repository.GetDocument(documentId);
        if (doc == null)
        {
            Console.WriteLine("❌ Operação não autorizada");
            return;
        }

        if (user.ClearanceLevel < doc.SecurityLevel)
        {
            Console.WriteLine("❌ Operação não autorizada");
            return;
        }

        _inner.EditDocument(documentId, user, newContent);
    }
}
