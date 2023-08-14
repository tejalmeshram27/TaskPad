using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleProject
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDesc { get; set; }
        public string TaskStatus { get; set; }

        public string TaskDueDate { get; set; }

        public string TaskPriority { get; set; }
    }

    internal class Program
    {
        public static bool isNull(string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            { 
                return false; 
            }
        }

        public static void createTask()
        {

            Console.WriteLine("Enter task title: ");
            string taskTitle = Console.ReadLine();

            Console.WriteLine("Enter task description: ");
            string taskDesc = Console.ReadLine();

            Console.WriteLine("Enter task due date");


            //if(taskTitle != null && taskDesc != null)
            if(!string.IsNullOrEmpty(taskTitle) && !string.IsNullOrEmpty(taskDesc) )
            {
                Task task = new Task();

                Random random = new Random();

                task.TaskId = random.Next(1, 1000);

                task.TaskTitle = taskTitle;

                task.TaskDesc = taskDesc;

                task.TaskStatus = "Incomplete";

                List<Task> taskList = new List<Task>();

                taskList.Add(task);

                string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

                List<Task> existingTaskList = new List<Task>();

                string existingJson = File.ReadAllText(jsonfile);

                existingTaskList = JsonSerializer.Deserialize<List<Task>>(existingJson);

                List<Task> combinedTaskList = new List<Task>(existingTaskList);

                combinedTaskList.AddRange(taskList);

                string json = JsonSerializer.Serialize(combinedTaskList, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(jsonfile, json);

                //string jsonstr = JsonSerializer.Serialize(taskList);

                //string listToJson = JsonSerializer.Serialize(taskList, new JsonSerializerOptions
                //{
                //    WriteIndented = true // Makes the JSON output nicely formatted
                //});

                //File.AppendAllText (jsonfile, listToJson);

                Console.WriteLine($"Task added and the respective task ID is {task.TaskId}");

                Console.WriteLine("*********************************************************");

            }

            else
            {
                
                Console.WriteLine("Give something in the Task titile and Task description");
                Console.WriteLine("*********************************************************");

            }


        }

        public static void ViewAllTask()
        {
            string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

            string json = File.ReadAllText (jsonfile);

            List<Task> taskList1 = JsonSerializer.Deserialize<List<Task>>(json);

            Console.WriteLine();

            if (taskList1 != null )
            {
                foreach (Task task in taskList1)
                {
                    
                    Console.WriteLine($"Task ID: {task.TaskId} \n Task Title: {task.TaskTitle} \n Task Description: {task.TaskDesc} \n Task Status: {task.TaskStatus} ");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("*********************************************************");


        }

        public static void ViewSpecificTask()
        {
            Console.WriteLine("Enter the task ID you want to display:");
            string selectedId = Console.ReadLine();

            string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

            string json = File.ReadAllText(jsonfile);

            List<Task> taskList1 = JsonSerializer.Deserialize<List<Task>>(json);

            Task TaskIdToDisplay = taskList1.Find(p => p.TaskId.ToString() == selectedId);

            Console.WriteLine();

            if (taskList1 != null  )
            {

                foreach (Task task in taskList1)
                {
                    if( task.TaskId.ToString() == selectedId)
                    {
                        Console.WriteLine(task.TaskId.ToString());
                        Console.WriteLine($"Task ID: {task.TaskId} \n Task Title: {task.TaskTitle} \n Task Description: {task.TaskDesc} \n Task Status: {task.TaskStatus} ");
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************");
                        return;
                    }

                    

                }

                //else
                //{
                    Console.WriteLine("Enter a valid Task Id");
                    //Console.WriteLine();
                    Console.WriteLine("*********************************************************");
                //}


            }

            

        }

        public static void MarkCompleted()
        {
            Console.WriteLine("Enter the task ID you want to mark as completed: ");
            string selectedId = Console.ReadLine();

            string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

            string json = File.ReadAllText(jsonfile);

            List<Task> taskList1 = JsonSerializer.Deserialize<List<Task>>(json);

            Task TaskIdToUpdate = taskList1.Find(p => p.TaskId.ToString() == selectedId);

            Console.WriteLine();

            if (TaskIdToUpdate != null)
            {


                // Update the status
                TaskIdToUpdate.TaskStatus = "Complete";

                // Serialize the updated list back to JSON
                string updatedJson = JsonSerializer.Serialize(taskList1, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write the updated JSON content back to the file
                File.WriteAllText(jsonfile, updatedJson);

                Console.WriteLine($"{TaskIdToUpdate.TaskId} marked as completed!");

                Console.WriteLine("*********************************************************");
                

                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Task ID not found.");
                Console.WriteLine("*********************************************************");

                //Console.WriteLine();

            }

            //if (taskList1 != null)
            //{
            //    foreach (Task task in taskList1)
            //    {
            //        if (task.TaskId == selectedId)
            //        {
            //            //Console.WriteLine($"Task ID: {task.TaskId} \n Task Title: {task.TaskTitle} \n Task Description: {task.TaskDesc} \n Task Status: {task.TaskStatus} ");
            //            //Console.WriteLine();

            //            task.TaskStatus = "Complete";
            //            Console.WriteLine($"{task.TaskId} mark as completed!");

            //        }

            //    }
            //}

        }

        public static void UpdateTask()
        {
            Console.WriteLine("Enter the task ID you want to mark as update: ");
            string selectedId = Console.ReadLine();

            string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

            string json = File.ReadAllText(jsonfile);

            List<Task> taskList1 = JsonSerializer.Deserialize<List<Task>>(json);

            Task TaskIdToUpdate = taskList1.Find(p => p.TaskId.ToString() == selectedId);

            Console.WriteLine();

            if (TaskIdToUpdate != null)
            {
                Console.WriteLine($"Task ID: {TaskIdToUpdate.TaskId} \n Task Title: {TaskIdToUpdate.TaskTitle} \n Task Description: {TaskIdToUpdate.TaskDesc} \n Task Status: {TaskIdToUpdate.TaskStatus} ");
                Console.WriteLine();

                Console.WriteLine("What to do you want to update: Task Title, Task Description or Both?");
                Console.WriteLine("Press 1 to update Task Title, 2 to update the Task Description and 3 to update both");
                int entry = Convert.ToInt32(Console.ReadLine());


                if (entry == 1)
                {
                    // Update the Task title
                    Console.WriteLine("Enter the new task title");
                    string newTitle = Console.ReadLine();
                    TaskIdToUpdate.TaskTitle = newTitle;
                    Console.WriteLine($"Task Id: {TaskIdToUpdate.TaskId} updated!");
                    Console.WriteLine("*********************************************************");

                }

                else if(entry == 2)
                {
                    
                        // Update the Task description
                    Console.WriteLine("Enter the new task description");
                    string newDesc = Console.ReadLine();
                    TaskIdToUpdate.TaskDesc = newDesc;
                    Console.WriteLine($"Task Id: {TaskIdToUpdate.TaskDesc} updated!");
                    Console.WriteLine() ;
                    Console.WriteLine("*********************************************************");

                }

                else if (entry == 3)
                {
                    // Update the Task title and description
                    Console.WriteLine("Enter the new task title");
                    string newTitle = Console.ReadLine();
                    TaskIdToUpdate.TaskTitle = newTitle;

                    Console.WriteLine("Enter the new task description");
                    string newDesc = Console.ReadLine();
                    TaskIdToUpdate.TaskDesc = newDesc;

                    Console.WriteLine($"Task Id: {TaskIdToUpdate.TaskDesc} updated!");
                    Console.WriteLine();
                    Console.WriteLine("*********************************************************");
                }

                else
                {
                    Console.WriteLine("Invalid Entry");
                    Console.WriteLine();
                    Console.WriteLine("*********************************************************");


                }

                // Update the status
                //TaskIdToUpdate.TaskStatus = "Complete";

                // Serialize the updated list back to JSON
                string updatedJson = JsonSerializer.Serialize(taskList1, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write the updated JSON content back to the file
                File.WriteAllText(jsonfile, updatedJson);

               


                //Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Task ID not found.");
                Console.WriteLine("*********************************************************");

                //Console.WriteLine();

            }
        }

        public static void DeleteTask()
        {
            Console.WriteLine("Enter the task ID you want to delete: ");
            string selectedId = Console.ReadLine();

            string jsonfile = @"C:\Users\TejalMeshram\source\repos\ConsoleProject\Task.json";

            string json = File.ReadAllText(jsonfile);

            List<Task> taskList1 = JsonSerializer.Deserialize<List<Task>>(json);

            Task TaskIdToUpdate = taskList1.Find(p => p.TaskId.ToString() == selectedId);

            Console.WriteLine();

            if (TaskIdToUpdate != null)
            {
                Console.WriteLine($"Are you sure yo want to delete Task Id: {selectedId}, Yes or No? ");
                string input = Console.ReadLine();

                if(input == "Yes")
                {
                    taskList1.Remove(TaskIdToUpdate);
                    Console.WriteLine($"Task Id: {TaskIdToUpdate.TaskId} deleted!");
                    Console.WriteLine("*********************************************************");


                }

                else if(input == "No")
                {
                    Console.WriteLine($"Task Id:{{TaskIdToUpdate.TaskId}}, not deleted");
                    Console.WriteLine("*********************************************************");

                }


                //if()



                // Serialize the updated list back to JSON
                string updatedJson = JsonSerializer.Serialize(taskList1, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write the updated JSON content back to the file
                File.WriteAllText(jsonfile, updatedJson);

                //Console.WriteLine($"Task Id: {TaskIdToUpdate.TaskId} deleted!");

                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Task ID not found.");
                Console.WriteLine("*********************************************************");

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, User!");

            //Console.WriteLine("Enter your name: ");
            //string uname = Console.ReadLine();

            //Console.WriteLine("Hi, " + uname + "!" );

            Console.WriteLine();

            while (true) {

                Console.WriteLine("Select an option from the list given below:");

                Console.WriteLine("1. Add a task");
                Console.WriteLine("2. View all tasks");
                Console.WriteLine("3. View a specific task");
                Console.WriteLine("4. Mark a task as completed");
                Console.WriteLine("5. Update a task");
                Console.WriteLine("6. Delete a task");
                //Console.WriteLine("7. Save tasks to a file");
                //Console.WriteLine("8. Load tasks from a file");
                Console.WriteLine("7. Exit...");

                Console.WriteLine();

                //int selected = Convert.ToInt32(Console.ReadLine());

                string selected = Console.ReadLine();

                switch (selected)
                {
                    case "1":
                        createTask();
                        break;

                    case "2":
                        ViewAllTask();
                        break;

                    case "3":
                        ViewSpecificTask();
                        break;

                    case "4":
                        MarkCompleted();
                        break;

                    case "5":
                        UpdateTask();
                        break;

                    case "6":
                        DeleteTask();
                        break;

                    //case 7:

                    //    break;

                    //case 8:

                    //    break;

                    case "7":
                        Console.WriteLine("Thanks for Visiting. Visit Again!");
                        Console.WriteLine("*********************************************************");

                        return;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid Entry. Select a valid Entry: ");
                        Console.WriteLine("*********************************************************");

                        Console.WriteLine();
                        break;
                }

            }

            

        }
    }

    

}