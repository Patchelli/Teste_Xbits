using Teste_Xbits.Domain.Entities;
using Teste_Xbits.Infra.Interfaces.RepositoryContracts;
using Teste_Xbits.Infra.ORM.Context;
using Teste_Xbits.Infra.Repositories.Base;

namespace Teste_Xbits.Infra.Repositories;

public sealed class DomainLoggerRepository(
    ApplicationContext dbContext)
    : RepositoryBase<DomainLogger>(dbContext), IDomainLoggerRepository
{
    public async Task SaveRangeAsync(List<DomainLogger> loggers)
    {
        await DbSetContext.AddRangeAsync(loggers);

        await SaveInDatabaseAsync();
    }
}