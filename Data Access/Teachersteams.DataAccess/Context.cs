using System.Data.Entity;
using Teachersteams.DataAccess.Mappings;

namespace Teachersteams.DataAccess
{
    public class Context: DbContext
    {
        private const string DefaultConnection = "MainDbConnection";

        public Context(): base(DefaultConnection)
        {
            //to copy EntityFramework.SqlServer.dll to bin folder of Startup project. Otherwise we do not use this .dll and it is not copied, but we need it.
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new TeacherMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new AssignmentMap());
            modelBuilder.Configurations.Add(new AssignmentResultMap());
        }
    }
}
