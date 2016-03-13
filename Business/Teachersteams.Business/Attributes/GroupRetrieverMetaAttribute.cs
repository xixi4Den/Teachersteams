using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GroupRetrieverMetaAttribute: Attribute
    {
        public GroupFilterType FilterType
        {
            get;
            private set;
        }

        public GroupRetrieverMetaAttribute(GroupFilterType filterType)
        {
            FilterType = filterType;
        }
    }
}
