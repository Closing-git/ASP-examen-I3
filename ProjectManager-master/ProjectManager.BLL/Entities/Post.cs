using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Post
    {
        public Guid PostId { get; private set; }
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length > 256) throw new FormatException();
                _subject = value;
            }
        }
        private string _content;
        public string Content
        {
            get { return _content; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                _content = value;
            }
        }
        public DateTime SendDate { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid ProjectId { get; private set; }

        public Post(string subject, string content, Guid employeeId, Guid projectId)
        {
            PostId = Guid.NewGuid();
            Subject = subject;
            Content = content;
            SendDate = DateTime.Now;
            EmployeeId = employeeId;
            ProjectId = projectId;
        }

        public Post(Guid postId, string subject, string content, DateTime sendDate, Guid employeeId, Guid projectId)
        {
            PostId = postId;
            Subject = subject;
            Content = content;
            SendDate = sendDate;
            EmployeeId = employeeId;
            ProjectId = projectId;
        }
    }
}
