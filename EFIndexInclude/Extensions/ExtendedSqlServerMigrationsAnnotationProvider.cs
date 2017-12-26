using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace EFIndexInclude.Extensions
{
    class ExtendedSqlServerMigrationsAnnotationProvider : SqlServerMigrationsAnnotationProvider
    {
        public ExtendedSqlServerMigrationsAnnotationProvider(MigrationsAnnotationProviderDependencies dependencies) : base(dependencies)
        {

        }

        public override IEnumerable<IAnnotation> For(IIndex index)
        {
            var baseAnnotations = base.For(index);
            var customAnnotations = index.GetAnnotations().Where(a => a.Name == "SqlServer:IncludeIndex");

            return baseAnnotations.Concat(customAnnotations);
        }
    }
}