namespace Teste_Xbits.Infra.ORM.EntitiesMapping.Base;

public abstract class MappingBase(string schema)
{
    private const string SchemaDefault = "xbits";
    protected string Schema = schema;

    protected MappingBase() : this(SchemaDefault)
    {
    }
}