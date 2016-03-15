using System.Collections.Generic;
using AutoMapper;

namespace Teachersteams.Business.Extensions
{
    public static class MapperExtensions
    {
        public static IEnumerable<TTarget> MapManyTo<TTarget>(this IMapper mapper, IEnumerable<object> sourceEnumerable)
        {
            return mapper.Map<IEnumerable<TTarget>>(sourceEnumerable);
        }
    }
}
