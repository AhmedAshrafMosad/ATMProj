using DBApp;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private const int NewEmployeeIndex = 0;
    private const int DisplayEmployeesIndex = 1;
    private const int NewDepartmentIndex = 2;
    private const int DisplayDepartmentsIndex = 3;
    private const int SearchEmployeeIndex = 4;
    private const int ExitIndex = 5;

    static async Task Main(string[] args)
    {
        // Ensure database is created
        using (var context = new ApplicationDbContext())
        {
            await context.Database.EnsureCreatedAsync(); // Create database if it doesn't exist
        }

        string[] menu = { "New Employee", "Display Employees", "New Department", "Display Departments", "Search Employee", "Exit" };
        int highlight = 0;
        bool looping = true;

        do
        {
            Console.Clear();
            DisplayMenu(menu, highlight);

            // Handle user input
            ConsoleKeyInfo k = Console.ReadKey();
            switch (k.Key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? menu.Length - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == menu.Length - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    await HandleMenuSelection(highlight);
                    break;
                case ConsoleKey.Escape:
                    looping = false;
                    break;
            }
        } while (looping);
    }

    private static void DisplayMenu(string[] menu, int highlight)
    {
        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;
        int menuSpacing = 5; // Number of spaces between menu items
        int itemCount = menu.Length;

        // Calculate total height for each menu item
        int itemHeight = screenHeight / itemCount;

        // Display the menu
        for (int i = 0; i < itemCount; i++)
        {
            Console.SetCursorPosition((screenWidth / 2) - (menu[i].Length / 2), i * itemHeight);
            Console.BackgroundColor = (i == highlight) ? ConsoleColor.Green : ConsoleColor.Black;

            // Print the menu item with spaces
            Console.WriteLine(menu[i].PadRight(menu[i].Length + menuSpacing));
        }

        Console.BackgroundColor = ConsoleColor.Black; // Reset background color
    }

    private static async Task HandleMenuSelection(int highlight)
    {
        switch (highlight)
        {
            case NewEmployeeIndex:
                await AddNewEmployee();
                break;
            case DisplayEmployeesIndex:
                await DisplayEmployees();
                break;
            case NewDepartmentIndex:
                await AddNewDepartment();
                break;
            case DisplayDepartmentsIndex:
                await DisplayDepartments();
                break;
            case SearchEmployeeIndex:
                await SearchEmployee();
                break;
            case ExitIndex:
                Environment.Exit(0);
                break;
        }
    }

    private static async Task AddNewEmployee()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Employee Details:");

            string name = GetStringInput("Name: ");
            float salary = GetFloatInput("Salary: ");
            int age = GetIntInput("Age: ");
            string gender = GetStringInput("Enter Gender (Male/Female): ");
            int departmentId = GetIntInput("Enter Department ID: ");

            using (var context = new ApplicationDbContext())
            {
                if (!await context.Departments.AnyAsync(d => d.Id == departmentId))
                {
                    Console.WriteLine("Error: Department ID does not exist.");
                    Console.ReadKey();
                    return; // Exit if the department does not exist
                }

                var employee = new Employee
                {
                    Name = name,
                    Salary = salary,
                    Age = age,
                    Gender = gender,
                    DepartmentId = departmentId
                };

                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Employee added successfully!");
        }
        catch (Exception ex)
        {
            HandleException(ex, "Error adding employee");
        }
        Console.ReadKey();
    }

    private static async Task DisplayEmployees()
    {
        Console.Clear();
        using (var context = new ApplicationDbContext())
        {
            var employees = await context.Employees.Include(emp => emp.Department).ToListAsync();
            foreach (var emp in employees)
            {
                Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}, Age: {emp.Age}, Gender: {emp.Gender}, Department: {emp.Department?.Name}");
            }
        }
        Console.ReadKey();
    }

    private static async Task AddNewDepartment()
    {
        try
        {
            Console.Clear();
            string departmentName = GetStringInput("Enter Department Name: ");
            var department = new Department { Name = departmentName };

            using (var context = new ApplicationDbContext())
            {
                context.Departments.Add(department);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Department added successfully!");
        }
        catch (Exception ex)
        {
            HandleException(ex, "Error adding department");
        }
        Console.ReadKey();
    }

    private static async Task DisplayDepartments()
    {
        Console.Clear();
        using (var context = new ApplicationDbContext())
        {
            var departments = await context.Departments.ToListAsync();
            foreach (var dept in departments)
            {
                Console.WriteLine($"Department ID: {dept.Id}, Name: {dept.Name}");
            }
        }
        Console.ReadKey();
    }

    private static async Task SearchEmployee()
    {
        Console.Clear();
        Console.WriteLine("Search Employee by Name or ID:");
        string searchQuery = GetStringInput("Enter Name or ID: ");

        using (var context = new ApplicationDbContext())
        {
            // Check if input is an integer (ID) or a string (Name)
            if (int.TryParse(searchQuery, out int id))
            {
                var employeeById = await context.Employees.Include(emp => emp.Department)
                    .FirstOrDefaultAsync(emp => emp.Id == id);
                if (employeeById != null)
                {
                    Console.WriteLine($"ID: {employeeById.Id}, Name: {employeeById.Name}, Salary: {employeeById.Salary}, Age: {employeeById.Age}, Gender: {employeeById.Gender}, Department: {employeeById.Department?.Name}");
                }
                else
                {
                    Console.WriteLine("No employee found with that ID.");
                }
            }
            else
            {
                var employeesByName = await context.Employees.Include(emp => emp.Department)
                    .Where(emp => emp.Name.Contains(searchQuery)).ToListAsync();
                if (employeesByName.Any())
                {
                    foreach (var emp in employeesByName)
                    {
                        Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}, Age: {emp.Age}, Gender: {emp.Gender}, Department: {emp.Department?.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No employee found with that name.");
                }
            }
        }
        Console.ReadKey();
    }

    private static string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    private static float GetFloatInput(string prompt)
    {
        float value;
        while (!float.TryParse(GetStringInput(prompt), out value))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        return value;
    }

    private static int GetIntInput(string prompt)
    {
        int value;
        while (!int.TryParse(GetStringInput(prompt), out value))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        return value;
    }

    private static void HandleException(Exception ex, string message)
    {
        Console.WriteLine($"{message}: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
    }
}
