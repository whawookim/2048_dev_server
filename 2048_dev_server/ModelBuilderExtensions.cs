using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public static class ModelBuilderExtensions
{
    public static void ConfigureStringListProperty<TEntity>(
        this ModelBuilder modelBuilder,
        Expression<Func<TEntity, List<string>>> propertyExpression
    ) where TEntity : class
    {
        modelBuilder.Entity<TEntity>()
            .Property(propertyExpression)
            .HasConversion(ValueConverters.StringListToJson);
    }
}