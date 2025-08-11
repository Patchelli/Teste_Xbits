using Teste_Xbits.Domain.Entities;

namespace Teste_Xbits.Infra.Interfaces.RepositoryContracts;

public interface IDomainLoggerRepository : IDisposable
{
    Task SaveRangeAsync(List<DomainLogger> loggers);
}