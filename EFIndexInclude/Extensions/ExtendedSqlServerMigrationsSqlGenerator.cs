using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EFIndexInclude.Extensions
{
    class ExtendedSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        protected virtual string StatementTerminator => ";";

        public ExtendedSqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, IMigrationsAnnotationProvider migrationsAnnotations) :
            base(dependencies, migrationsAnnotations)
        {

        }

        protected override void Generate(CreateIndexOperation operation, IModel model, MigrationCommandListBuilder builder, bool terminate)
        {
            base.Generate(operation, model, builder, false);

            var includeIndexAnnotation = operation.FindAnnotation("SqlServer:IncludeIndex");

            if (includeIndexAnnotation != null)
                builder.Append($" INCLUDE({includeIndexAnnotation.Value})");
          
            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }
    }
}