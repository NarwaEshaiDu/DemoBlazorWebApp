namespace Blazor2App.Database.Entities
{
    public class BookEntity : BaseEntity
    {
        /// <summary>
        /// Name of Book
        /// </summary>
        public string BookName { get; set; }
        public StudentEntity Student { get; set; }
    }
}
