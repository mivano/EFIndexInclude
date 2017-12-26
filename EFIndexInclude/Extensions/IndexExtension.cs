using System;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFIndexInclude.Extensions
{
    static class IndexExtension
    {
        public static IndexBuilder Include<TEntity>(this IndexBuilder indexBuilder, Expression<Func<TEntity, object>> indexExpression)
        {                                                                         
            var includeStatement = new StringBuilder();
            foreach (var column in indexExpression.GetPropertyAccessList())
            {
                if (includeStatement.Length > 0)
                    includeStatement.Append(", ");

                includeStatement.AppendFormat("[{0}]", column.Name);
            }         
            
            indexBuilder.HasAnnotation("SqlServer:IncludeIndex", includeStatement.ToString());

            return indexBuilder;
        }
    }
}