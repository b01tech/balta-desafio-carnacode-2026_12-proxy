using ProxyChallenge.Document;
using AppUser = ProxyChallenge.User.User;

namespace ProxyChallenge.Services;

public class CachingServiceProxy : IDocumentService
{
    private readonly IDocumentService _inner;
    private readonly Dictionary<string, ConfidentialDocument> _cache = new();

    public CachingServiceProxy(IDocumentService inner)
    {
        _inner = inner;
    }

    public ConfidentialDocument? ViewDocument(string documentId, AppUser user)
    {
        if (_cache.TryGetValue(documentId, out var cached))
        {
            Console.WriteLine($"[Cache] Documento {documentId} encontrado no cache");
            return cached;
        }

        var doc = _inner.ViewDocument(documentId, user);
        if (doc != null)
        {
            _cache[documentId] = doc;
        }
        return doc;
    }

    public void EditDocument(string documentId, AppUser user, string newContent)
    {
        _inner.EditDocument(documentId, user, newContent);
        if (_cache.ContainsKey(documentId))
        {
            _cache.Remove(documentId);
        }
    }
}
