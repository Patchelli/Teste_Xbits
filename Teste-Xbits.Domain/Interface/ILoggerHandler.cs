
using Teste_Xbits.Domain.Entities;

namespace Teste_Xbits.Domain.Interface;

public interface ILoggerHandler
{
    void CreateLogger(DomainLogger logger);
    bool HasLogger();
    Task SaveInDataBase();
}