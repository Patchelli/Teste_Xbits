using Teste_Xbits.Domain.Entities;
using Teste_Xbits.Domain.Interface;
using Teste_Xbits.Infra.Interfaces.RepositoryContracts;

namespace Teste_Xbits.ApplicationService.Loggers;

public sealed class LoggerHandler(
    IDomainLoggerRepository domainLoggerRepository)
    : ILoggerHandler
{
    private readonly List<DomainLogger> _domainLoggers = [];

    public void CreateLogger(DomainLogger logger) => _domainLoggers.Add(logger);

    public bool HasLogger() => _domainLoggers.Count > 0;

    public async Task SaveInDataBase() => await domainLoggerRepository.SaveRangeAsync(_domainLoggers);
}