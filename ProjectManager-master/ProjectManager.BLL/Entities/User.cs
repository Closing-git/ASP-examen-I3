using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class User
    {
        public Guid UserId { get; private set; }
        private string _email;

        public string Email
        {
            get { return _email; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (!Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) throw new FormatException("L'e-mail n'est pas au bon format");
                _email = value;
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length > 32) throw new FormatException();
                _password = value;
            }
        }
        public Guid EmployeeId { get; private set; }


        public User(string email, string password, Guid employeeId)
        {
            UserId = Guid.NewGuid();
            Email = email;
            Password = password;
            EmployeeId = employeeId;
        }

        public User(Guid userId, string email, string password, Guid employeeId)
        {
            UserId = userId;
            Email = email;
            Password = password;
            EmployeeId = employeeId;
        }
    }
}

