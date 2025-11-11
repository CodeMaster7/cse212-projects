using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and dequeue them. Higher priority should come out first.
    // Expected Result: "High" (priority 10), then "Medium" (priority 5), then "Low" (priority 1)
    // Defect(s) Found: The Dequeue method doesn't remove items from the queue after returning them.
    // Also, the loop condition "index < _queue.Count - 1" skips the last element, so it might miss
    // the highest priority item if it's at the end.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        // Add items with different priorities (lower number = lower priority)
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        // Dequeue should return highest priority first
        Assert.AreEqual("High", priorityQueue.Dequeue(), "Highest priority (10) should come out first");
        Assert.AreEqual("Medium", priorityQueue.Dequeue(), "Medium priority (5) should come out second");
        Assert.AreEqual("Low", priorityQueue.Dequeue(), "Low priority (1) should come out last");

        // Queue should be empty now
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue(),
            "Dequeue from empty queue should throw exception");
    }

    [TestMethod]
    // Scenario: Test that the highest priority item is found even when it's at the end of the queue.
    // Expected Result: "Last" (priority 100) should be dequeued first, even though it was added last
    // Defect(s) Found: The loop condition "index < _queue.Count - 1" skips checking the last element,
    // so if the highest priority item is at the end, it won't be found correctly.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        // Add items where the highest priority is at the end
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 3);
        priorityQueue.Enqueue("Last", 100); // Highest priority, added last

        // Should get "Last" first because it has highest priority (100)
        Assert.AreEqual("Last", priorityQueue.Dequeue(),
            "Highest priority item at the end should be found and dequeued first");

        // Then the rest in descending priority order
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}