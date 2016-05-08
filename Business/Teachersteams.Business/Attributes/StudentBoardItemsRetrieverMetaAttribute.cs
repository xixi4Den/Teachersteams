using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StudentBoardItemsRetrieverMetaAttribute: Attribute
    {
        public StudentBoardFilterType FilterType { get; set; }

        public StudentBoardItemsRetrieverMetaAttribute(StudentBoardFilterType filterType)
        {
            FilterType = filterType;
        }
    }
}
