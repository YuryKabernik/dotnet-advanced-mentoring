using System.Data;
using Dapper;

namespace CatalogService.DataAccess.SqlMappers;

public class UriTypeMapper : SqlMapper.TypeHandler<Uri>
{
    public override Uri? Parse(object value)
    {
        Uri? resultUri = default;

        if (value is string strValue)
        {
            Uri.TryCreate(strValue, UriKind.RelativeOrAbsolute, out resultUri);
        }

        return resultUri;
    }

    public override void SetValue(IDbDataParameter parameter, Uri? value)
    {
        parameter.Value = value?.ToString();
    }
}
