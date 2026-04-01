using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using ProjectManager.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Services
{
    public class TakePartService : ITakePartRepository<TakePart>
    {
        private readonly SqlConnection _connection;
        public TakePartService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid Create(TakePart entity)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                try
                {
                    command.CommandText = "SP_TakePart_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(TakePart.EmployeeId), entity.EmployeeId);
                    command.Parameters.AddWithValue(nameof(TakePart.ProjectId), entity.ProjectId);
                    command.Parameters.AddWithValue(nameof(TakePart.StartDate), entity.StartDate);

                    _connection.Open();
                    return (Guid)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
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

        public void SetEnd(Guid employeeId, Guid projectId, DateTime endDate)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_TakePart_SetEnd";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue("endDate", endDate);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void Update(Guid id, TakePart entity)
        {
            throw new NotImplementedException();
        }
    }
}
