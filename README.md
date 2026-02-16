![ES-7](https://github.com/user-attachments/assets/61d1998c-69c4-484e-a6d8-7c84b03357b9)

## 🥁 CarnaCode 2026 - Desafio 12 - Proxy

Oi, eu sou o Bruno e este é o espaço onde compartilho minha jornada de aprendizado durante o desafio **CarnaCode 2026**, realizado pelo [balta.io](https://balta.io). 👻

Aqui você vai encontrar projetos, exercícios e códigos que estou desenvolvendo durante o desafio. O objetivo é colocar a mão na massa, testar ideias e registrar minha evolução no mundo da tecnologia.

### Sobre este desafio

No desafio **Proxy** eu tive que resolver um problema real implementando o **Design Pattern** em questão.
Neste processo eu aprendi:

- ✅ Boas Práticas de Software
- ✅ Código Limpo
- ✅ SOLID
- ✅ Design Patterns (Padrões de Projeto)

## Problema

Uma aplicação corporativa precisa controlar acesso a documentos sensíveis, fazer cache de documentos pesados e registrar todas as operações.
O código atual mistura lógica de negócio com controle de acesso, cache e logging.

# Solução

## Solução implementada

Apliquei o padrão Proxy para separar responsabilidades e adicionar comportamentos transversais sem modificar o core.

### Componentes principais

- Core:
  - `ConfidentialDocument.cs`: modelo do documento.
  - `DocumentRepository.cs`: repositório real.
  - `VirtualDocumentRepositoryProxy`: lazy loading do repositório.
  - `DocumentService.cs`: lógica de negócio pura.
  - `User.cs`: usuário com `Username` e `ClearanceLevel`.

- Proxies:
  - `AuthServiceProxy.cs`: valida autorização (Protection Proxy).
  - `CachingServiceProxy.cs`: cacheia leituras e invalida em edição.
  - `LoggingServiceProxy.cs`: registra tentativas, sucessos e falhas.

### Composição

Os proxies são encadeados mantendo a mesma interface:

1. `LoggingServiceProxy`
2. `AuthServiceProxy`
3. `CachingServiceProxy`
4. `DocumentService` (base)
5. `VirtualDocumentRepositoryProxy`

Essa ordem garante auditoria de todas as operações, bloqueio de acesso não autorizado, uso de cache nas leituras e inicialização preguiçosa do repositório.

## Como executar

No diretório `src/ProxyChallenge`:

```bash
dotnet build
dotnet run
```

## O que o exemplo demonstra

- Lazy loading do repositório apenas no primeiro acesso.
- Cache em acessos repetidos ao mesmo documento.
- Acesso negado quando o nível do usuário é inferior ao exigido.
- Edição por um usuário autorizado, seguida de invalidação do cache e recarregamento.

## Sobre o CarnaCode 2026

O desafio **CarnaCode 2026** consiste em implementar todos os 23 padrões de projeto (Design Patterns) em cenários reais. Durante os 23 desafios desta jornada, os participantes são submetidos ao aprendizado e prática na idetinficação de códigos não escaláveis e na solução de problemas utilizando padrões de mercado.

### eBook - Fundamentos dos Design Patterns

Minha principal fonte de conhecimento durante o desafio foi o eBook gratuito [Fundamentos dos Design Patterns](https://lp.balta.io/ebook-fundamentos-design-patterns).

### Veja meu progresso no desafio

[Repositório Central do Desafio](https://github.com/b01tech/desafio-carnacode-2026-design-patterns.git)
