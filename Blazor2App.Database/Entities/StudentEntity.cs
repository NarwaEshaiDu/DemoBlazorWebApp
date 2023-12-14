using Blazor2App.Application.Models;

namespace Blazor2App.Database.Entities
{
    public class StudentEntity : BaseEntity
    {
        /// <summary>
        /// Name of Student
        /// </summary>
        public string Name { get; set; }
        public ICollection<BookEntity> Books { get; set; }

        public static IEnumerable<StudentModel> ToModel(IEnumerable<StudentEntity> entities)
        {
            if (entities != null)
            {
                return entities.Select(e => ToModel(e));
            }
            return null;
        }

        public static StudentModel ToModel(StudentEntity entity)
        {
            if (entity != null)
            {
                return new StudentModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
            return null;
        }

        public static IEnumerable<StudentEntity> FromModelToEntity(IEnumerable<StudentModel> models)
        {
            if (models != null)
            {
                return models.Select(e => FromModelToEntity(e)).ToList();
            }
            return null;
        }

        public static StudentEntity FromModelToEntity(StudentModel model)
        {
            if (model != null)
            {
                return new StudentEntity
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
            return null;
        }
    }
}
