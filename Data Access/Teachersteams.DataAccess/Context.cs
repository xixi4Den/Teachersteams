using System.Data.Entity;
using Teachersteams.DataAccess.Mappings;

namespace Teachersteams.DataAccess
{
    public class Context: DbContext
    {
        private const string DefaultConnection = "MainDbConnection";

        public Context(): base(DefaultConnection)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GroupMap());
        }
    }
}
