
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Extensions
{
    
        public static class ModelBuilderExtension
        {
            public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
            {
                var entities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null).Select(e => e.ClrType);
                foreach (var entity in entities)
                {
                    var newParam = Expression.Parameter(entity);
                    var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                    modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
                }
            }

            public static void AppendQueryFilter<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
            {
                var entities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null).Select(e => e.ClrType);
                foreach (var entity in entities)
                {
                    var newParam = Expression.Parameter(entity);
                    var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);

                    if (modelBuilder.Entity(entity).Metadata.GetQueryFilter() != null)
                    {
                        var currentQueryFilter = modelBuilder.Entity(entity).Metadata.GetQueryFilter();
                        var currentExpressionFilter = ReplacingExpressionVisitor.Replace(currentQueryFilter.Parameters.Single(), newParam, currentQueryFilter.Body);
                        newbody = Expression.AndAlso(currentExpressionFilter, newbody);
                    }

                    modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
                }
            }
        }
    }
