using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Employee
    {

        public Guid EmployeeId { get; private set; }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length > 64) throw new FormatException();
                _firstName = value;
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length > 64) throw new FormatException();
                _lastName = value;
            }
        }
        public bool IsProjectManager { get; private set; }
        public DateTime HireDate { get; private set; }
        public Employee(string firstName, string lastName, bool isProjectManager, DateTime hireDate)
        {
            EmployeeId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            IsProjectManager = isProjectManager;
            HireDate = hireDate;
        }

        public Employee(Guid employeeId, string firstName, string lastName, bool isProjectManager, DateTime hireDate)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            IsProjectManager = isProjectManager;
            HireDate = hireDate;
        }
    }
}
