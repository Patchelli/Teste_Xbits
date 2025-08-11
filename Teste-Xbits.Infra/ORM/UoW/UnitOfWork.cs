using Microsoft.EntityFrameworkCore.Infrastructure;
using Teste_Xbits.Domain.Interface;
using Teste_Xbits.Infra.ORM.Context;

namespace Teste_Xbits.Infra.ORM.UoW;

public sealed class UnitOfWork(
    ApplicationContext applicationContext)
    : IUnitOfWork
{
    private readonly DatabaseFacade _databaseFacade = applicationContext.Database;

    public void CommitTransaction()
    {
        try
        {
            _databaseFacade.CommitTransaction();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
    }

    public void RollbackTransaction() => _databaseFacade.RollbackTransaction();

    public void BeginTransaction() => _databaseFacade.BeginTransaction();
}