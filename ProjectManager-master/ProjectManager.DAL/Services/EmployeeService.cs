using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using ProjectManager.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Services
{
    internal class EmployeeService : IEmployeeRepository<Employee>
    {

        private readonly SqlConnection _connection;

        public EmployeeService(SqlConnection connection)
        {
            _connection = connection;
        }
        public bool CheckIsOnProject(Guid employeeId, Guid projectId)
        {
            try
            {
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SP_Employee_Check_WorkOnProject";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                    command.Parameters.AddWithValue(nameof(projectId), projectId);

                    _connection.Open();
                    return employeeId == (Guid)command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool CheckIsProjectManager(Guid employeeId)
        {
            try
            {
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SP_Employee_Check_IsProjectManager";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                    _connection.Open();
                    return employeeId == (Guid)command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Guid Create(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee Get(Guid id)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(id), id);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToEmployee();
                    }
                    throw new ArgumentOutOfRangeException(nameof(id));
                }
                _connection.Close();
            }
        }

        public IEnumerable<Employee> GetAvailableEmployees()
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_GetFree";
                command.CommandType = CommandType.StoredProcedure;
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
                _connection.Close();
            }
        }

        public IEnumerable<Employee> GetByProjectId(Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromProjectId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);

                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
                _connection.Close();
            }
        }

        public Employee GetByUserId(Guid userId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromUserId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(userId), userId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToEmployee();
                    }
                    throw new ArgumentOutOfRangeException(nameof(userId));
                }
                _connection.Close();
            }
        }

        public void Update(Guid id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
