using Teste_Xbits.Domain.Handlers.ValidationHandler;

namespace Teste_Xbits.Domain.Interface;

public interface IValidate<T> where T : class
{
    Task<ValidationResponse> ValidationAsync(T entity);
    ValidationResponse Validation(T entity);
}