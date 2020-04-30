using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp
{
    public class Todo
    {
        private TodoStatus _todoStatus;
        public Guid Id { get; set; }
        public string Title { get; set; }

        public TodoStatus Status { 
            get {
                return _todoStatus;
            } set {
                _todoStatus = value;
                if (value == TodoStatus.Completed)
                {
                    this.DateCompleted = DateTime.Now;
                }
            } }

        public string Description { get; set;  }

        public DateTime DateCreated { get; set; }

        public DateTime DateCompleted { get; set;  }

        public Todo()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }

    public enum TodoStatus
    {
        Completed,
        NotCompleted
    }
}
