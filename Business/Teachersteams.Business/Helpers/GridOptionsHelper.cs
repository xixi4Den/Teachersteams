using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Helpers
{
    public class GridOptionsHelper: IGridOptionsHelper
    {
        IDictionary<SortingDirection, string> directionMapping = new Dictionary<SortingDirection, string>
        {
            {SortingDirection.Asc, "OrderBy"},
            {SortingDirection.Desc, "OrderByDescending"}
        }; 

        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> BuidDynamicOrderedQuery<TEntity>(GridOptions gridOptions)
        {
            Contract.NotNull<ArgumentException>(gridOptions);

            if (!string.IsNullOrWhiteSpace(gridOptions.SortingColumn) &&
                gridOptions.SortingDirection != SortingDirection.None)
            {
                var typeOfT = typeof(TEntity);
                var parameter = Expression.Parameter(typeOfT, "x");
                var propertyType = typeOfT.GetProperty(gridOptions.SortingColumn).PropertyType;
                var propertyAccess = Expression.PropertyOrField(parameter, gridOptions.SortingColumn);
                var orderExpression = Expression.Lambda(propertyAccess, parameter);

                var direction = directionMapping[gridOptions.SortingDirection];

                return x => (IOrderedQueryable<TEntity>)x.Provider
                    .CreateQuery<TEntity>(Expression.Call(typeof (Queryable), direction,
                        new[] {typeOfT, propertyType}, x.Expression, orderExpression));

            }
            return null;
        }
    }
}
