namespace Entities.DataTransferObjects
{
    public record CourseDtoForUpdate(int Id, String? CourseName, String? CourseDescription, String? CourseThumbnail, bool IsRequire, int Rank);
}
