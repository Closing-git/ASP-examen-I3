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

        public Project Get(Guid projectId)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_ById";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToProject();
                    }
                    throw new ArgumentOutOfRangeException(nameof(projectId));
                }
            }
        }

        public IEnumerable<Project> GetByEmployeeId(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var projects = new List<Project>();
                    while (reader.Read())
                    {
                        projects.Add(reader.ToProject());
                    }
                    return projects; 
                }
            }
        }

        public IEnumerable<Project> GetByManagerId(Guid managerId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromProjectManagerId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("projectManagerId", managerId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var projects = new List<Project>();
                    while (reader.Read())
                    {
                        projects.Add(reader.ToProject());
                    }
                    return projects;
                }
            }
        }

        public void Update(Guid id, Project entity)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Update";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("projectId", id);
                command.Parameters.AddWithValue(nameof(entity.Description), entity.Description);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
