/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Requirement: The user shall specify the maximum size of the Customer Service Queue when it is created.
        // If the size is invalid (less than or equal to 0) then the size defaults to 10.
        // Scenario: Create CustomerService with invalid sizes (<= 0) and valid sizes
        // Expected Result: Invalid sizes should default to 10, valid sizes should be used
        Console.WriteLine("Test 1");

        // Test invalid sizes - should all default to 10
        var service1 = new CustomerService(0);
        Console.WriteLine($"Size 0: {service1}"); // Should show max_size=10

        var service2 = new CustomerService(-5);
        Console.WriteLine($"Size -5: {service2}"); // Should show max_size=10

        var service3 = new CustomerService(-1);
        Console.WriteLine($"Size -1: {service3}"); // Should show max_size=10

        // Test valid size - should use the specified size
        var service4 = new CustomerService(5);
        Console.WriteLine($"Size 5: {service4}"); // Should show max_size=5

        // Defect(s) Found: None - constructor properly validates and defaults to 10

        Console.WriteLine("=================");

        // Test 2
        // Requirement: The AddNewCustomer method shall enqueue a new customer into the queue.
        // Scenario: Add customers to the queue and verify they are enqueued
        // Expected Result: Customers should be added to the queue, queue size should increase
        Console.WriteLine("Test 2");
        var service = new CustomerService(5);

        Console.WriteLine($"Queue before adding customers: {service}");
        service.AddNewCustomer(); // Customer 1
        Console.WriteLine($"After adding customer 1: {service}");

        service.AddNewCustomer(); // Customer 2
        Console.WriteLine($"After adding customer 2: {service}");

        service.AddNewCustomer(); // Customer 3
        Console.WriteLine($"After adding customer 3: {service}");
        // Defect(s) Found: None :)

        Console.WriteLine("=================");

        // Test 3
        // Requirement: If the queue is full when trying to add a customer, then an error message will be displayed.
        // Scenario: Fill queue to capacity and try to add another customer
        // Expected Result: Error message should be displayed, queue size should not exceed maxSize
        Console.WriteLine("Test 3");
        service = new CustomerService(4);

        // Fill queue to capacity
        service.AddNewCustomer(); // Customer 1
        service.AddNewCustomer(); // Customer 2
        service.AddNewCustomer(); // Customer 3
        service.AddNewCustomer(); // Customer 4 - queue is now full
        Console.WriteLine($"Queue after adding 4 customers (should be full): {service}");

        // Try to add 5th customer - should display error message
        Console.WriteLine("Attempting to add 5th customer (should show error message)...");
        service.AddNewCustomer(); // Should show error message
        Console.WriteLine($"Queue after attempting to add 5th customer: {service}");
        // Defect(s) Found: Need to use >= instead of > in AddNewCustomer check

        Console.WriteLine("=================");

        // Test 4
        // Requirement: The ServeCustomer function shall dequeue the next customer from the queue and display the details.
        // Scenario: Add customers and serve them
        // Expected Result: Customers should be dequeued and displayed in order (first in, first out)
        Console.WriteLine("Test 4");
        service = new CustomerService(4);

        service.AddNewCustomer(); // Customer 1 (will be served first)
        service.AddNewCustomer(); // Customer 2 (will be served second)
        Console.WriteLine($"Before serving customers: {service}");

        Console.WriteLine("Serving first customer (should be customer 1):");
        service.ServeCustomer(); // Should display customer 1
        Console.WriteLine($"After serving first customer: {service}");

        Console.WriteLine("Serving second customer (should be customer 2):");
        service.ServeCustomer(); // Should display customer 2
        Console.WriteLine($"After serving second customer: {service}");
        Console.WriteLine($"After serving customers: {service}");
        // Defect(s) Found: Need to get customer before removing it from the list

        Console.WriteLine("=================");

        // Test 5
        // Requirement: If the queue is empty when trying to serve a customer, then an error message will be displayed.
        // Scenario: Try to serve a customer when queue is empty
        // Expected Result: Error message should be displayed
        Console.WriteLine("Test 5");
        service = new CustomerService(4);

        Console.WriteLine("Attempting to serve customer from empty queue (should show error message)...");
        service.ServeCustomer(); // Should show error message
        // Defect(s) Found: Need to check queue length and display error message when empty
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count <= 0) {
            Console.WriteLine("No Customers in the queue");
            return;
        }
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}