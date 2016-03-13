namespace Teachersteams.Domain.Query
{
    /// <summary>
    /// Contains page rules for retrieving entities from repository.
    /// </summary>
    public class PageSettings
    {
        public PageSettings(int index, int size)
        {
            Index = index;
            Size = size;
        }

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
