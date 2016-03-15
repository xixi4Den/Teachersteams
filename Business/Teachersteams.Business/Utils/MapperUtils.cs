using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Teachersteams.Business.Utils
{
    public static class MapperUtils
    {
        public static IEnumerable<Profile> GetAllProfiles()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x))
                .Select(x => (Profile)Activator.CreateInstance(x));
        }
    }
}
