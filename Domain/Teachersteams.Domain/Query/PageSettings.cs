namespace Teachersteams.Domain.Query
{
    /// <summary>
    /// Contains page rules for retrieving entities from repository.
    /// </summary>
    public class PageSettings
    {
        /// <summary>
        /// Page index.
        /// </summary>
        public int Index
        {
            get; 
            set;
        }

        /// <summary>
        /// Items count per page.
        /// </summary>
        public int Size
        {
            get; 
            set;
        }
    }
}
