using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserTypeSpecificRetrieverMetaAttribute: Attribute
    {
         public UserType UserType
        {
            get;
            private set;
        }

         public UserTypeSpecificRetrieverMetaAttribute(UserType userType)
        {
            UserType = userType;
        }
    }
}
