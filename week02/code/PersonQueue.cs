/// <summary>
/// A basic implementation of a Queue
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the queue
    /// </summary>
    /// <param name="person">The person to add</param>
    public void Enqueue(Person person)
    {
        // Bug fix: Changed from Insert(0, person) to Add(person)
        // Original bug: Insert(0, person) added items to the front (index 0), making it LIFO (stack-like)
        // This caused the wrong order - when enqueuing Bob, Tim, Sue, they became [Sue, Tim, Bob]
        // instead of [Bob, Tim, Sue], so dequeuing returned Sue first instead of Bob
        // Fix: Add to the end (back) of the queue for proper FIFO behavior
        _queue.Add(person);
    }

    public Person Dequeue()
    {
        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}