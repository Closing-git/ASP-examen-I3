using ProjectManager.BLL.Entities;

namespace ProjectManager.ASPMVC.Mappers
{
    public static class Mapper
    {
        #region Project
        public static Models.Project.ListItemViewModel ToListItem(this BLL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Models.Project.ListItemViewModel()
            {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                
            };
        }
        #endregion

    }
}
