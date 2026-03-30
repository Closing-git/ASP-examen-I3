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
    public class TakePartService:ITakePartRepository<TakePart>
    {
        private readonly ITakePartRepository<DAL.Entities.TakePart> _dalService;
        public TakePartService(ITakePartRepository<DAL.Entities.TakePart> dalService)
        {
            _dalService = dalService;
        }

        public Guid Create(TakePart entity)
        {
            return _dalService.Create(entity.ToDAL());
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TakePart> Get()
        {
            throw new NotImplementedException();
        }

        public TakePart Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SetEnd(Guid employeeId, Guid projectId, DateTime endTime)
        {
            _dalService.SetEnd(employeeId, projectId, endTime);
        }

        public void Update(Guid id, TakePart entity)
        {
            throw new NotImplementedException();
        }
    }
}
