using ProjectManager.BLL.Entities;
using ProjectManager.BLL.Mappers;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class PostService : IPostRepository<Post>
    {
        private readonly IPostRepository<DAL.Entities.Post> _dalService;
        public PostService(IPostRepository<DAL.Entities.Post> dalService)
        {
            _dalService = dalService;
        }
        public Guid Create(Post entity)
        {
            return _dalService.Create(entity.ToDAL());
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> Get()
        {
            throw new NotImplementedException();
        }

        public Post Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetByProjectId(Guid projectId)
        {
            return _dalService.GetByProjectId(projectId).Select(post => post.ToBLL());
        }

        public IEnumerable<Post> GetWorkingOnProject(Guid projectId, Guid employeeId)
        {
            return _dalService.GetWorkingOnProject(projectId, employeeId).Select(post => post.ToBLL());
        }

        public void Update(Guid id, Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
