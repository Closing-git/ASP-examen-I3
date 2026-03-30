using ProjectManager.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Mappers
{
    internal static class Mapper
    {
        #region User
        public static BLL.Entities.User ToBLL(this DAL.Entities.User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new BLL.Entities.User(
                entity.UserId,
                entity.Email,
                entity.Password,
                entity.EmployeeId);
        }
        public static DAL.Entities.User ToDAL(this BLL.Entities.User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.User()
            {
                UserId = entity.UserId,
                Email = entity.Email,
                Password = entity.Password,
                EmployeeId = entity.EmployeeId

            };
        }
        #endregion


        #region Employee

        public static BLL.Entities.Employee ToBLL(this DAL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            BLL.Entities.Employee employee = new BLL.Entities.Employee(
                entity.EmployeeId,
                entity.FirstName,
                entity.LastName,
                entity.IsProjectManager,
                entity.HireDate
                );
            return employee;
        }
        public static DAL.Entities.Employee ToDAL(this BLL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.Employee()
            {
                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsProjectManager = entity.IsProjectManager,
                HireDate = entity.HireDate
            };
        }
        #endregion


        #region Post

        public static BLL.Entities.Post ToBLL(this DAL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            BLL.Entities.Post post = new BLL.Entities.Post(
                entity.PostId,
                entity.Subject,
                entity.Content,
                entity.SendDate,
                entity.EmployeeId,
                entity.ProjectId

                );
            return post;
        }
        public static DAL.Entities.Post ToDAL(this BLL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.Post()
            {
                PostId = entity.PostId,
                Subject = entity.Subject,
                Content = entity.Content,
                SendDate = entity.SendDate,
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId

            };
        }
        #endregion


        #region Project
        public static BLL.Entities.Project ToBLL(this DAL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            BLL.Entities.Project project = new BLL.Entities.Project(
                entity.ProjectId,
                entity.Name,
                entity.Description,
                entity.CreationDate,
                entity.ProjectManagerId

                );
            return project;
        }
        public static DAL.Entities.Project ToDAL(this BLL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.Project()
            {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                CreationDate = entity.CreationDate,
                ProjectManagerId = entity.ProjectManagerId


            };
        }
        #endregion


        #region TakePart


        public static BLL.Entities.TakePart ToBLL(this DAL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            BLL.Entities.TakePart takePart = new BLL.Entities.TakePart(
                entity.EmployeeId,
                entity.ProjectId
                );
            return takePart;
        }
        public static DAL.Entities.TakePart ToDAL(this BLL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.TakePart()
            {
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate?.Date,

            };
        }
        #endregion
    }
}
