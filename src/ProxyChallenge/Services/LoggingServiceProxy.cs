using ProxyChallenge.Document;
using AppUser = ProxyChallenge.User.User;

namespace ProxyChallenge.Services;

public class LoggingServiceProxy : IDocumentService
{
    private readonly IDocumentService _inner;
    private readonly List<string> _auditLog;

    public LoggingServiceProxy(IDocumentService inner, List<string>? auditLog = null)
    {
        _inner = inner;
        _auditLog = auditLog ?? new List<string>();
    }

    public IReadOnlyList<string> Logs => _auditLog;

    public ConfidentialDocument? ViewDocument(string documentId, AppUser user)
    {
        var entry = $"[{DateTime.Now:HH:mm:ss}] {user.Username} tentou acessar {documentId}";
        _auditLog.Add(entry);
        Console.WriteLine($"[Audit] {entry}");

        var doc = _inner.ViewDocument(documentId, user);
        if (doc == null)
        {
            _auditLog.Add($"[{DateTime.Now:HH:mm:ss}] ACESSO NEGADO para {user.Username}");
        }
        else
        {
            _auditLog.Add($"[{DateTime.Now:HH:mm:ss}] ACESSO PERMITIDO para {user.Username}");
        }
        return doc;
    }

    public void EditDocument(string documentId, AppUser user, string newContent)
    {
        var entry = $"[{DateTime.Now:HH:mm:ss}] {user.Username} tentou editar {documentId}";
        _auditLog.Add(entry);
        Console.WriteLine($"[Audit] {entry}");

        _inner.EditDocument(documentId, user, newContent);
        _auditLog.Add($"[{DateTime.Now:HH:mm:ss}] EDIÇÃO CONCLUÍDA por {user.Username}");
    }

    public void PrintAuditLog()
    {
        Console.WriteLine("\n=== Log de Auditoria ===");
        foreach (var e in _auditLog)
        {
            Console.WriteLine(e);
        }
    }
}
