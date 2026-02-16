using ProxyChallenge.Document;
using ProxyChallenge.Services;
using ProxyChallenge.User;

Console.WriteLine("=== Sistema de Documentos Confidenciais (Proxy) ===");

IDocumentRepository repository = new VirtualDocumentRepositoryProxy();
IDocumentService baseService = new DocumentService(repository);
IDocumentService caching = new CachingServiceProxy(baseService);
IDocumentService auth = new AuthServiceProxy(caching, repository);
var logging = new LoggingServiceProxy(auth);

var manager = new User("joao.silva", 5);
var employee = new User("maria.santos", 2);

Console.WriteLine("\n--- Gerente acessando documento de alto nível ---");
var doc1 = logging.ViewDocument("DOC002", manager);

Console.WriteLine("\n--- Funcionário tentando acessar mesmo documento ---");
var doc2 = logging.ViewDocument("DOC002", employee);

Console.WriteLine("\n--- Gerente acessando novamente (usa cache) ---");
var doc3 = logging.ViewDocument("DOC002", manager);

Console.WriteLine("\n--- Funcionário acessando documento permitido ---");
var doc4 = logging.ViewDocument("DOC003", employee);

Console.WriteLine("\n--- Gerente edita documento permitido (invalida cache) ---");
logging.EditDocument("DOC003", manager, "Políticas e procedimentos atualizados... (3 MB)");

Console.WriteLine("\n--- Funcionário acessa novamente após edição (recarrega do repositório) ---");
var doc5 = logging.ViewDocument("DOC003", employee);

logging.PrintAuditLog();
