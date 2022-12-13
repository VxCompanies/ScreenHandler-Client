using Client.Entities;
using Client.Extensions;
using Client.Repositories;
using ScreenHandler.Handlers;

var registerCustomer = FormHandler.CreateBuilder("/Users/omar.nunez/Projects/ScreenHandler/Client/form.json")
    .Build();

IJsonRepository<Customer> formRepository = new FormRepository<Customer>();

Menu();

void Menu()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Welcome!{Environment.NewLine}");
        Console.WriteLine("1) Register new customer");
        Console.WriteLine("2) Search for existing customer");
        Console.WriteLine("3) Update existing customer");
        Console.WriteLine("4) Delete customer");
        Console.WriteLine("5) Exit");

        Console.Write($"{Environment.NewLine}>> ");
        var selectedOption = Console.ReadLine();

        switch (selectedOption)
        {
            case "1":
                RegisterCustomer();
                break;
            case "2":
                SearchCustomer();
                break;
            case "3":
                UpdateCustomer();
                break;
            case "4":
                DeleteCustomer();
                break;
            case "5":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Please select a valid option");
                break;
        }
    } while (true);
}

void DeleteCustomer()
{
    do
    {
        Console.Clear();
        var customer = SearchCustomerById();

        Console.WriteLine($"Customer info:{Environment.NewLine}");
        Console.WriteLine($"ID: {customer.Id}{Environment.NewLine}FirstName: {customer.FirstName}{Environment.NewLine}LastName: {customer.LastName}{Environment.NewLine}"
            + $"Age: {customer.Age}{Environment.NewLine}Email Address: {customer.EmailAddress ?? "N/A"}{Environment.NewLine}");

        Console.WriteLine($"Are you sure you want to delete this user? (y/n){Environment.NewLine}");

        Console.Write($"{Environment.NewLine}>> ");
        var option = Console.ReadLine()?.ToLower();

        switch (option)
        {
            case "y":
                Console.Clear();
                Console.WriteLine("Customer deleted successfully.");
                Console.ReadKey(true);
                formRepository.Delete(customer.Id);
                break;
            case "n":
                return;
            case "3":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var age = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(age))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.Age = int.Parse(age);
                formRepository.Update(customer.Id, customer);
                break;
            case "4":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.EmailAddress = email;
                formRepository.Update(customer.Id, customer);
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please select a valid option.");
                Console.ReadKey(true);
                continue;
        }
    } while (true);
}

void UpdateCustomer()
{
    do
    {
        Console.Clear();
        var customer = SearchCustomerById();

        Console.WriteLine($"Which field you want to update?{Environment.NewLine}");
        Console.WriteLine("1) First name");
        Console.WriteLine("2) Last name");
        Console.WriteLine("3) Age");
        Console.WriteLine("4) Email Address");

        Console.Write($"{Environment.NewLine}>> ");
        var selectedField = Console.ReadLine();

        switch (selectedField)
        {
            case "1":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var firstName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.FirstName = firstName;
                formRepository.Update(customer.Id, customer);
                break;
            case "2":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.LastName = lastName;
                formRepository.Update(customer.Id, customer);
                break;
            case "3":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var age = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(age))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.Age = int.Parse(age);
                formRepository.Update(customer.Id, customer);
                break;
            case "4":
                Console.Clear();

                Console.WriteLine("Enter the customer's first name");
                Console.Write($"{Environment.NewLine}>> ");
                var email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave field empty.");
                    Console.ReadKey(true);
                    continue;
                }

                customer.EmailAddress = email;
                formRepository.Update(customer.Id, customer);
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please select a valid option.");
                Console.ReadKey(true);
                continue;
        }
    } while (true);
}

void RegisterCustomer()
{
    Console.Clear();
    registerCustomer.Run();
    var newCustomerForm = registerCustomer.GetAnswers();

    var newCustomer = newCustomerForm.AsCustomer();
    formRepository.Create(newCustomer);
}

void SearchCustomer()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Seach by:{Environment.NewLine}");
        Console.WriteLine("1) Show all");
        Console.WriteLine("2) ID");
        Console.WriteLine("3) Field");
        Console.WriteLine("4) Go back");

        Console.Write($"{Environment.NewLine}>> ");
        var selectedOption = Console.ReadLine();

        switch (selectedOption)
        {
            case "1":
                var allCustomers = formRepository.GetAll();
                Console.Clear();
                Console.WriteLine($"Customers info:{Environment.NewLine}");

                foreach (var ctmr in allCustomers)
                    Console.Write($"ID: {ctmr.Id}{Environment.NewLine}FirstName: {ctmr.FirstName}{Environment.NewLine}LastName: {ctmr.LastName}{Environment.NewLine}"
                        + $"Age: {ctmr.Age}{Environment.NewLine}Email Address: {ctmr.EmailAddress ?? "N/A"}");
                break;
            case "2":
                var customer = SearchCustomerById();
                Console.Clear();
                Console.WriteLine($"Customer info:{Environment.NewLine}");
                Console.Write($"ID: {customer.Id}{Environment.NewLine}FirstName: {customer.FirstName}{Environment.NewLine}LastName: {customer.LastName}{Environment.NewLine}"
                    + $"Age: {customer.Age}{Environment.NewLine}Email Address: {customer.EmailAddress ?? "N/A"}");
                break;
            case "3":
                var customers = SearchCustomerByField();
                Console.Clear();
                Console.WriteLine($"Customers info:{Environment.NewLine}");

                foreach (var ctmr in customers)
                    Console.Write($"ID: {ctmr.Id}{Environment.NewLine}FirstName: {ctmr.FirstName}{Environment.NewLine}LastName: {ctmr.LastName}{Environment.NewLine}"
                        + $"Age: {ctmr.Age}{Environment.NewLine}Email Address: {ctmr.EmailAddress ?? "N/A"}");
                break;
            case "4":
                return;
            default:
                Console.Clear();
                Console.Write("Please select a valid option.");
                break;
        }
        Console.ReadKey(true);
    } while (true);
}

IEnumerable<Customer> SearchCustomerByField()
{
    do
    {
        Console.Clear();

        Console.WriteLine("Seach by:");
        Console.WriteLine("1) First name");
        Console.WriteLine("2) Last name");
        Console.WriteLine("3) Age");
        Console.WriteLine("4) Email address");
        Console.WriteLine("5) Go back");

        Console.Write($"{Environment.NewLine}>> ");
        var selectedOption = Console.ReadLine();

        switch (selectedOption)
        {
            case "1":
                return SearchCustomerByFirstName();
            case "2":
                return SearchCustomerByLastName();
            case "3":
                return SearchCustomerByAge();
            case "4":
                return SearchCustomerByEmailAddress();
            case "5":
                SearchCustomer();
                break;
            default:
                Console.Write("Please select a valid option.");
                break;
        }
    } while (true);
}

IEnumerable<Customer> SearchCustomerByFirstName()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Enter customer's first name{Environment.NewLine}");
        Console.Write($"{Environment.NewLine}>> ");

        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.Write("Cannot leave field in blank.");
            Console.ReadKey(true);
            continue;
        }

        var customers = formRepository.GetAll(c => c.FirstName == value);
        return customers;
    } while (true);
}

IEnumerable<Customer> SearchCustomerByLastName()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Enter customer's last name{Environment.NewLine}");
        Console.Write($"{Environment.NewLine}>> ");

        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.Write("Cannot leave field in blank.");
            Console.ReadKey(true);
            continue;
        }

        var customers = formRepository.GetAll(c => c.LastName == value);
        return customers;
    } while (true);
}

IEnumerable<Customer> SearchCustomerByAge()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Enter customer's age{Environment.NewLine}");
        Console.Write($"{Environment.NewLine}>> ");

        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.Write("Cannot leave field in blank.");
            Console.ReadKey(true);
            continue;
        }

        if (int.TryParse(value, out int age))
        {
            Console.Write("Customer age must be an integer.");
            Console.ReadKey(true);
            continue;
        }

        var customers = formRepository.GetAll(c => c.Age == age);
        return customers;
    } while (true);
}

IEnumerable<Customer> SearchCustomerByEmailAddress()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Enter customer's email address{Environment.NewLine}");
        Console.Write($"{Environment.NewLine}>> ");

        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.Write("Cannot leave field in blank.");
            Console.ReadKey(true);
            continue;
        }

        var customers = formRepository.GetAll(c => c.EmailAddress == value);
        return customers;
    } while (true);
}

Customer SearchCustomerById()
{
    do
    {
        Console.Clear();

        Console.WriteLine($"Enter customer's ID{Environment.NewLine}");
        Console.Write($"{Environment.NewLine}>> ");

        var customerId = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerId))
        {
            Console.Write("Cannot leave customer's id blank.");
            Console.ReadKey(true);
            continue;
        }

        if (int.TryParse(customerId, out int id))
        {
            Console.Write("Customer ID must be an integer.");
            Console.ReadKey(true);
            continue;
        }

        var customer = formRepository.Get(id);
        return customer;
    } while (true);
}
