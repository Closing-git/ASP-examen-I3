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

        public static Models.Project.DetailsViewModel ToDetails(this BLL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Models.Project.DetailsViewModel()
            {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                CreationDate = entity.CreationDate.ToString("d"),
                ProjectManagerName = "",
                Team = new List<string>()
            };
        }

        public static BLL.Entities.Project ToBLL(this Models.Project.CreateForm entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new BLL.Entities.Project(
                entity.Name,
                entity.Description,
                entity.ProjectManagerId);
        }
    }
    #endregion

}

