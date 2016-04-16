using System;
using System.Linq;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Helpers
{
    public interface IGridOptionsHelper: IDependency
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> BuidDynamicOrderedQuery<TEntity>(GridOptions gridOptions);
    }
}
