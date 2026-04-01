using ProjectManager.ASPMVC.Models.Employee;
using ProjectManager.BLL.Entities;
using ProjectManager.DAL.Entities;

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
                ManagerId = entity.ProjectManagerId,
                Name = entity.Name,
                Description = entity.Description,
                CreationDate = entity.CreationDate.ToString("d"),
                ProjectManagerName = "",
                TeamMembers = new List<TeamMemberViewModel>()
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

        public static BLL.Entities.Project ToBLL(this Models.Project.EditForm entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new BLL.Entities.Project(
                entity.ProjectName,
                entity.Description,
                entity.ManagerId
                );
        }

        public static Models.Project.EditForm ToEditDescription(this BLL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Models.Project.EditForm
            {
                Description = entity.Description,
                ProjectName = entity.Name,
                ProjectId = entity.ProjectId
            };
        }

        #endregion

        #region Post
        public static Models.Post.ListItemViewModel ToListItem(this BLL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Models.Post.ListItemViewModel()
            {
                PostId = entity.PostId,
                Subject = entity.Subject,
                Content = entity.Content,
                EmployeeId = entity.EmployeeId,
                SendDate = entity.SendDate,
                ProjectId = entity.ProjectId
            };
        }

        public static BLL.Entities.Post ToBLL(this Models.Post.CreateForm entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new BLL.Entities.Post(
                entity.Subject,
                entity.Content,
                entity.EmployeeId,
                entity.ProjectId);
        }

        #endregion

        #region TeamMember

        public static Models.Employee.TeamMemberViewModel ToTeamMember(this BLL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Models.Employee.TeamMemberViewModel
            {
                EmployeeId = entity.EmployeeId,
                Name = $"{entity.FirstName} {entity.LastName}",
                Email = ""

            };
        }

        #endregion
    }

}

