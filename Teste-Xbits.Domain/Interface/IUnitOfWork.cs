namespace Teste_Xbits.Domain.Interface;

public interface IUnitOfWork
{
    void CommitTransaction();
    void RollbackTransaction();
    void BeginTransaction();
}