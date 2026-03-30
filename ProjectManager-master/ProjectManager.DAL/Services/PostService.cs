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
    public class PostService : IPostRepository<Post>
    {

        private readonly SqlConnection _connection;
        public PostService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid Create(Post entity)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                try
                {
                    command.CommandText = "SP_Post_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Post.AuthorId), entity.AuthorId);
                    command.Parameters.AddWithValue(nameof(Post.ProjectId), entity.ProjectId);
                    command.Parameters.AddWithValue(nameof(Post.Subject), entity.Subject);
                    command.Parameters.AddWithValue(nameof(Post.Content), entity.Content);

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
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_ProjectManager";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);

                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToPost();
                    }
                }
                _connection.Close();
            }
        }

        public IEnumerable<Post> GetWorkingOnProject(Guid projectId, Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);


                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToPost();
                    }
                }
                _connection.Close();
            }
        }

        public void Update(Guid id, Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
