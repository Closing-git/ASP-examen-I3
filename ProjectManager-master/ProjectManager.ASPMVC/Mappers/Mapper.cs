using ProjectManager.BLL.Entities;

namespace ProjectManager.ASPMVC.Mappers
{
    public static class Mapper
    {
        #region User
        public static BLL.Entities.User ToBLL(this Models.Auth.LoginForm entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new BLL.Entities.User(entity.Email, entity.Password);
        }
        #endregion

    }
}
