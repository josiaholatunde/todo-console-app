using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoApp
{
    class TodoService
    {
        public List<Todo> todos = new List<Todo>();


        public Todo AddTodo(string title, string description)
        {
            if (String.IsNullOrEmpty(title.Trim()) || String.IsNullOrEmpty(description.Trim()))
                throw new ArgumentNullException("The title and description fields are required for creating a todo ");

            if (this.DoesTodoTitleExistPreviously(title.Trim().ToLower()))
            {
                throw new Exception("Todo title has been taken previously");
            }
            Todo newTodo = new Todo
            {
                Title = title,
                Description = description
            };
            todos.Add(newTodo);
            return newTodo;
        }

        public Todo EditTodo(string title, Todo editedTodo)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("The todo title is required to edit the todo");
            }
            Todo todoToEdit = todos.Where(todo => todo.Title.ToLower() == title).FirstOrDefault();
            if (todoToEdit == null)
            {
                throw new Exception($"The todo with the title {title} was not found");
            }

            var indexOfTodoInList = todos.FindIndex(todo => todo.Id == todoToEdit.Id);
            todos[indexOfTodoInList] = editedTodo;
            return editedTodo;
        }

        public bool DeleteTodo(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("The todo title is required to edit the todo");
            }
            var indexOfTodoInList = todos.FindIndex(todo => todo.Title.ToLower().Equals(title.ToLower()));
            if (indexOfTodoInList == -1)
            {
                throw new Exception($"The todo with the title {title} was not found");
            }
            todos.RemoveAt(indexOfTodoInList);
            return true;
        }

        public Todo GetTodo(string title)
        {
            Todo todo = todos.Where(todo => todo.Title.ToLower() == title.Trim().ToLower()).FirstOrDefault();
            return todo;
        }

        public List<Todo> GetTodos()
        {
            return todos;
        }

        public static TodoService GetTodoServiceInstance()
        {
            return new TodoService();
        }

        private bool DoesTodoTitleExistPreviously(string title)
        {
            return todos.Any(todo => todo.Title.ToLower() == title);
        }
    }
}
