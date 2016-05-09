using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TeacherBoardItemsRetrieverMetaAttribute : Attribute
    {
        public TeacherBoardCompositeFilterType FilterType { get; set; }

        public TeacherBoardItemsRetrieverMetaAttribute(TeacherBoardCompositeFilterType filterType)
        {
            FilterType = filterType;
        }
    }
}
