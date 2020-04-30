using System;
using System.Collections.Generic;
using static System.Console;

namespace TodoApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to this Todo Application\nKindly Select the appropriate option");
           bool shouldContinueRunningApp = false;
            TodoService todoService = TodoService.GetTodoServiceInstance();
            do
            {
                RunTodoApp(todoService);
                WriteLine("Press 1 to continue the app or 2 to exit");
                int userInput = Convert.ToInt32(ReadLine());
                shouldContinueRunningApp = userInput == 1 ? true : false;
            } while (shouldContinueRunningApp);

            ReadKey();
        }

        public static void HandleTodosDisplay(TodoService todoService)
        {
            List<Todo> todos = todoService.GetTodos();
            WriteLine("Id\tTitle\tDescription\tStatus\tDate Created\tDate Updated");
            foreach (Todo todoItem in todos)
            {
                WriteLine($"{todoItem.Id}\t{todoItem.Title}\t{todoItem.Description}\t{todoItem.Status}\t{todoItem.DateCreated}\t{todoItem.DateCompleted}");
            }
        }

        public static void HandleAddTodos(TodoService todoService)
        {
            WriteLine("Enter the title of your todo");
            string title = ReadLine();
            WriteLine("Enter a short description for your todo");
            string description = ReadLine();
            try
            {
                Todo todo = todoService.AddTodo(title, description);
                WriteLine("Successfully added todo");
            }
            catch (ArgumentNullException exception)
            {
                WriteLine($"An error occurred while creating todo {exception.Message}");
            }
        }

        public static void HandleEditTodo(TodoService todoService)
        {
            WriteLine("Enter the title of your todo you wish to edit");
            string titleToEdit = ReadLine();
            Todo todoToEdit = todoService.GetTodo(titleToEdit);
            if (todoToEdit == null)
            {
                WriteLine("Invalid title");
            }
            else
            {
                WriteLine("Which of the properties do you wish to edit\n1 for Title\n2 for Description\n3 for Status i.e yes or no");
                int userResponse = Convert.ToInt32(ReadLine());
                if (userResponse == 1)
                {
                    WriteLine("Enter the new title ?");
                    string newTitle = ReadLine();
                    todoToEdit.Title = newTitle;
                    try
                    {
                        todoService.EditTodo(titleToEdit, todoToEdit);
                        WriteLine("Successfully edited todo");
                    }
                    catch (Exception exception)
                    {
                        WriteLine($"An error occurred while editing todo {exception.Message}");
                    }

                }
                else if (userResponse == 2)
                {
                    WriteLine("Enter the new description ?");
                    string newDescription = ReadLine();
                    todoToEdit.Description = newDescription;
                    try
                    {
                        todoService.EditTodo(titleToEdit, todoToEdit);
                        WriteLine("Successfully edited todo");
                    }
                    catch (Exception exception)
                    {
                        WriteLine($"An error occurred while editing todo {exception.Message}");
                    }
                }
                else if (userResponse == 3)
                {
                    WriteLine("Enter the new status ?");
                    string newStatus = ReadLine();
                    todoToEdit.Status = newStatus.Trim().ToLower() == "yes" ? TodoStatus.Completed : TodoStatus.NotCompleted;
                    try
                    {
                        todoService.EditTodo(titleToEdit, todoToEdit);
                        WriteLine("Successfully edited todo");
                    }
                    catch (Exception exception)
                    {
                        WriteLine($"An error occurred while editing todo {exception.Message}");
                    }
                }
                else
                {
                    WriteLine("Invalid input");
                }
            }
        }

        public static void HandleDeleteTodo(TodoService todoService)
        {
            WriteLine("Enter the title of your todo you wish to delete");
            string titleToDelete = ReadLine();
            bool result = todoService.DeleteTodo(titleToDelete);
            if (result)
            {
                WriteLine("Successfully deleted Todos");
            }
        }

        public static void RunTodoApp(TodoService todoService)
        {
            Console.WriteLine("Enter 1 to View Todos\n2 to Add a todo\n3 to edit a todo\n4 to delete a todo");

            int userInput;
            if (int.TryParse(ReadLine(), out userInput))
            {
                switch (userInput)
                {
                    case 1:
                        HandleTodosDisplay(todoService);
                        break;
                    case 2:
                        HandleAddTodos(todoService);
                        break;
                    case 3:
                        HandleEditTodo(todoService);

                        break;
                    case 4:
                        HandleDeleteTodo(todoService);
                        break;
                    default:
                        return;
                }
            }
            else
            {
                WriteLine("Invalid input");
            }
        }
    }
}
