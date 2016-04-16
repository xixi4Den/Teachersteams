using Teachersteams.Business.Enums;

namespace Teachersteams.Business.ViewModels.Grid
{
    public class GridOptions
    {
        public SortingDirection SortingDirection { get; set; }

        public string SortingColumn { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
