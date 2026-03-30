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
    public class ProjectService : IProjectRepository<Project>
    {
        private readonly SqlConnection _connection;
        public ProjectService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid Create(Project entity)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                try
                {
                    command.CommandText = "SP_Project_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Project.ProjectManagerId), entity.ProjectManagerId);
                    command.Parameters.AddWithValue(nameof(Project.Name), entity.Name);
                    command.Parameters.AddWithValue(nameof(Project.Description), entity.Description);

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

        public IEnumerable<Project> Get()
        {
            throw new NotImplementedException();
        }

        public Project Get(Guid id)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_GetById";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(id), id);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToProject();
                    }
                    throw new ArgumentOutOfRangeException(nameof(id));
                }
            }
        }

        public Project GetByEmployeeId(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToProject();
                    }
                    throw new ArgumentOutOfRangeException(nameof(employeeId));
                }
            }
        }

        public Project GetByManagerId(Guid managerId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromProjectManagerId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(managerId), managerId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToProject();
                    }
                    throw new ArgumentOutOfRangeException(nameof(managerId));
                }
            }
        }

        public void Update(Guid id, Project entity)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Update";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(id),id );
                command.Parameters.AddWithValue(nameof(entity.Description), entity.Description);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
