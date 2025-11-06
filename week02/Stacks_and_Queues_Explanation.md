# Stacks and Queues - Data Structures Guide

## Overview

Stacks and Queues are fundamental data structures that differ in how items are added and removed. Understanding the difference is crucial for choosing the right data structure for your problem.

---

## Stacks - Last In, First Out (LIFO)

### What is a Stack?

A **stack** is a data structure that follows the **"Last In, First Out" (LIFO)** principle. Think of it like a stack of pancakes - the last pancake you add goes on top, and when you remove one, you take it from the top.

### Real-World Examples

#### 1. Stack of Pancakes 🥞
- **Push**: Add a pancake to the top of the stack
- **Pop**: Remove the top pancake (the most recent one)
- The first pancake cooked stays at the bottom and gets cold (Last In, First Out)

#### 2. Undo Function in Text Editors
- Every action you perform (typing, deleting, formatting) is **pushed** onto a stack
- When you press Undo, it **pops** the last action and reverses it
- This ensures you undo actions in the correct reverse order

**Example:**
```
Type "The rain in Spain stays mainly in the plain"
Stack: [Type "The", Type "rain", Type "in", ..., Type "plain"]
Press Undo → Pops "Type 'plain'" → Removes "plain" from screen
Press Undo again → Pops "Type 'mainly'" → Removes "mainly" from screen
```

#### 3. Function Call Stack (Used by ALL Programs!)
- When you call a function, it's **pushed** onto the call stack
- When the function finishes, it's **popped** from the stack
- The computer uses this to know where to return to after a function completes

**Example:**
```csharp
Function A calls Function B
Function B calls Function C
Call Stack: [A, B, C] ← C is currently running
When C finishes: [A, B] ← Returns to B
When B finishes: [A] ← Returns to A
```

### Stack Terminology

| Term | Description | Location |
|------|-------------|----------|
| **back** | Where push and pop occur | Top of stack (last item added) |
| **front** | First item added | Bottom of stack (index 0) |
| **push** | Add item to back | O(1) - Constant time |
| **pop** | Remove item from back | O(1) - Constant time |

### Stack Operations in C#

In C#, you can use the built-in `Stack<T>` class:

```csharp
var stack = new Stack<char>();

// Push - Add to back
stack.Push('A');
stack.Push('B');
stack.Push('C');
// Stack: [A, B, C] (C is at the back/top)

// Pop - Remove from back
char top = stack.Pop(); // Returns 'C'
// Stack: [A, B]

// Check size
int size = stack.Count; // Returns 2

// Check if empty
if (stack.Count == 0) {
    // Stack is empty
}
```

### Stack Performance

| Operation | C# Code | Big O Notation |
|-----------|---------|----------------|
| `push(value)` | `myStack.Push(value)` | O(1) - Constant time |
| `pop()` | `var value = myStack.Pop()` | O(1) - Constant time |
| `size()` | `myStack.Count` | O(1) - Constant time |
| `empty()` | `if (myStack.Count == 0)` | O(1) - Constant time |

**All operations are O(1)!** Stacks are very efficient.

### When to Use Stacks

✅ **Use stacks when:**
- You need to reverse data or operations
- You need to track history (like Undo)
- You're processing nested structures (like parentheses matching)
- You need recursion (functions naturally use the call stack)

❌ **Don't use stacks when:**
- You need to process items in the order they arrive (use Queue instead)
- You need random access to items in the middle

---

## Queues - First In, First Out (FIFO)

### What is a Queue?

A **queue** is a data structure that follows the **"First In, First Out" (FIFO)** principle. Think of it like a line at a grocery store - the first person in line is served first, and new people join at the end of the line.

### Real-World Examples

#### 1. Grocery Store Line
- **Front**: The person next in line (first person, will be served next)
- **Back**: The person at the end of the line (last person, just joined)
- **Enqueue**: New person joins at the back
- **Dequeue**: Person at front is served and leaves

#### 2. Web Server Queue
- Web servers receive many HTTP requests simultaneously
- Requests are **enqueued** into a queue when they arrive
- The server **dequeues** requests one at a time to process them
- All requests are processed fairly in the order they arrived

**Example:**
```
Request 1 arrives → Enqueued
Request 2 arrives → Enqueued
Request 3 arrives → Enqueued
Queue: [Request1, Request2, Request3] (Request1 is at front)

Server processes Request1 → Dequeued
Queue: [Request2, Request3] (Request2 is now at front)
```

#### 3. Customer Service Queue
- Customers call and are put in a queue
- Customer service agents serve customers in order
- First customer to call is served first (fair processing)

### Queue Terminology

| Term | Description | Location |
|------|-------------|----------|
| **front** | Where dequeue occurs | First item added (index 0) |
| **back** | Where enqueue occurs | Last item added |
| **enqueue** | Add item to back | O(1) with built-in Queue |
| **dequeue** | Remove item from front | O(1) with built-in Queue |

### Queue Operations in C#

In C#, you can use the built-in `Queue<T>` class:

```csharp
var queue = new Queue<char>();

// Enqueue - Add to back
queue.Enqueue('A');
queue.Enqueue('B');
queue.Enqueue('C');
// Queue: [A, B, C] (A is at front, C is at back)

// Dequeue - Remove from front
char first = queue.Dequeue(); // Returns 'A'
// Queue: [B, C] (B is now at front)

// Check size
int size = queue.Count; // Returns 2

// Check if empty
if (queue.Count == 0) {
    // Queue is empty
}
```

### Queue Performance

| Operation | C# Code | Big O Notation |
|-----------|---------|----------------|
| `enqueue(value)` | `myQueue.Enqueue(value)` | O(1) - Constant time |
| `dequeue()` | `var value = myQueue.Dequeue()` | O(1) - Constant time |
| `size()` | `myQueue.Count` | O(1) - Constant time |
| `empty()` | `if (myQueue.Count == 0)` | O(1) - Constant time |

**All operations are O(1)!** Queues are very efficient with the built-in class.

### Implementing Queue with List (Dynamic Array)

If you implement a queue using a `List<T>` instead of the built-in `Queue<T>`:

```csharp
// Using List to implement queue
List<int> queue = new List<int>();

// Enqueue - Add to back
queue.Add(value); // O(1) - Adding to end is fast

// Dequeue - Remove from front
queue.RemoveAt(0); // O(n) - Removing from front requires shifting all elements!
```

**Performance Warning:** Using `List<T>` for a queue can be slower because:
- `RemoveAt(0)` requires shifting all remaining elements
- This makes Dequeue O(n) instead of O(1)

**Better approach:** Use the built-in `Queue<T>` class for optimal performance!

### When to Use Queues

✅ **Use queues when:**
- You need to process items in the order they arrive (FIFO)
- You need fair processing (first come, first served)
- You're handling requests or tasks that need to wait
- You're implementing a customer service system

❌ **Don't use queues when:**
- You need to reverse operations (use Stack instead)
- You need to process the most recent item first (use Stack instead)

---

## Comparison: Stack vs Queue

| Feature | Stack (LIFO) | Queue (FIFO) |
|---------|--------------|--------------|
| **Order** | Last In, First Out | First In, First Out |
| **Add** | Push (to back) | Enqueue (to back) |
| **Remove** | Pop (from back) | Dequeue (from front) |
| **Use Case** | Undo, history, recursion | Requests, tasks, fair processing |
| **Real-World** | Pancakes, Undo button | Grocery line, web server |

### Visual Comparison

**Stack (LIFO):**
```
Push A → [A]
Push B → [A, B]
Push C → [A, B, C]
Pop → Returns C, Queue: [A, B]
Pop → Returns B, Queue: [A]
```

**Queue (FIFO):**
```
Enqueue A → [A]
Enqueue B → [A, B]
Enqueue C → [A, B, C]
Dequeue → Returns A, Queue: [B, C]
Dequeue → Returns B, Queue: [C]
```

---

## Key Differences Summary

### Stack (LIFO)
- **Add and Remove**: Both happen at the **back** (same end)
- **Last item added**: First item removed
- **Use for**: Reversing, history, undo operations

### Queue (FIFO)
- **Add**: Happens at the **back**
- **Remove**: Happens at the **front** (different ends)
- **First item added**: First item removed
- **Use for**: Fair processing, waiting lists, requests

---

## Code Examples

### Stack Example: Balanced Parentheses

```csharp
public static bool IsBalanced(string line) {
    var stack = new Stack<char>();

    foreach (char c in line) {
        if (c == '(' || c == '[' || c == '{') {
            stack.Push(c); // Open bracket → push
        }
        else if (c == ')' || c == ']' || c == '}') {
            if (stack.Count == 0) return false; // No matching opening
            char top = stack.Pop(); // Pop to check match
            // Check if top matches closing bracket...
        }
    }

    return stack.Count == 0; // All brackets matched
}
```

### Queue Example: Customer Service

```csharp
public class CustomerService {
    private Queue<Customer> _queue = new Queue<Customer>();

    public void AddCustomer(Customer customer) {
        _queue.Enqueue(customer); // Add to back
    }

    public Customer ServeCustomer() {
        return _queue.Dequeue(); // Remove from front (FIFO)
    }
}
```

---

## Memory Aid

**Stack (LIFO):**
- Think: **"Last pancake added is first eaten"**
- Both operations at same end (back)
- Like a plate of pancakes 🥞

**Queue (FIFO):**
- Think: **"First person in line is first served"**
- Add at back, remove from front
- Like a grocery store line 🛒

---

## References

- [CSE 212 - Stacks Preparation Material](https://byui-cse.github.io/cse212-ww-course/week02/prepare-stacks.html)
- [CSE 212 - Queues Preparation Material](https://byui-cse.github.io/cse212-ww-course/week02/prepare-queues.html)

---

## Practice Questions

1. **Which data structure would you use for:**
   - Tracking browser history (Back button)?
   - Processing print jobs in order?
   - Matching opening and closing brackets?

2. **What's the Big O notation for:**
   - Stack.Push()?
   - Queue.Dequeue()?
   - Removing from front of List (used as queue)?

3. **True or False:**
   - Stacks and Queues both allow random access to middle elements?
   - Using List for a queue gives O(1) performance for dequeue?
   - The built-in Queue<T> class is more efficient than List<T> for queue operations?

---

## Interview Questions 🤔

### Question 1: Explain the Difference
**What's the difference between a Stack and a Queue?**

**Answer:**
- **Stack (LIFO):** Last In, First Out
  - Both add (push) and remove (pop) happen at the same end (back/top)
  - Last item added is the first item removed
  - Like a stack of pancakes

- **Queue (FIFO):** First In, First Out
  - Add (enqueue) happens at the back, remove (dequeue) happens at the front
  - First item added is the first item removed
  - Like a grocery store line

**Key difference:** Stack operations happen at one end, Queue operations happen at opposite ends.

---

### Question 2: Stack Operations
**What will be the output of this code?**

```csharp
var stack = new Stack<int>();
stack.Push(10);
stack.Push(20);
stack.Push(30);
Console.WriteLine(stack.Pop()); // ?
stack.Push(40);
Console.WriteLine(stack.Pop()); // ?
Console.WriteLine(stack.Pop()); // ?
```

**Answer:**
- `stack.Pop()` returns **30** (last item pushed)
- `stack.Pop()` returns **40** (next last item)
- `stack.Pop()` returns **20** (remaining item)

**Explanation:** Stack follows LIFO - always removes the most recently added item.

---

### Question 3: Queue Operations
**What will be the output of this code?**

```csharp
var queue = new Queue<int>();
queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);
Console.WriteLine(queue.Dequeue()); // ?
queue.Enqueue(40);
Console.WriteLine(queue.Dequeue()); // ?
Console.WriteLine(queue.Dequeue()); // ?
```

**Answer:**
- `queue.Dequeue()` returns **10** (first item enqueued)
- `queue.Dequeue()` returns **20** (next first item)
- `queue.Dequeue()` returns **30** (next first item)

**Explanation:** Queue follows FIFO - always removes the least recently added item (front).

---

### Question 4: Balanced Parentheses Problem
**Implement a function to check if parentheses are balanced using a stack.**

```csharp
public static bool IsBalanced(string expression) {
    // Your implementation here
}
```

**Example:**
- `"()"` → true
- `"()()"` → true
- `"(()"` → false
- `"((()))"` → true
- `"((())"` → false

**Answer:**

```csharp
public static bool IsBalanced(string expression) {
    var stack = new Stack<char>();

    foreach (char c in expression) {
        if (c == '(') {
            stack.Push(c); // Push opening bracket
        }
        else if (c == ')') {
            if (stack.Count == 0) {
                return false; // Closing bracket without opening
            }
            stack.Pop(); // Match found, pop opening bracket
        }
    }

    return stack.Count == 0; // All brackets matched
}
```

**Why Stack?** Need to match the most recent opening bracket with closing bracket - LIFO is perfect!

---

### Question 5: When to Use Stack vs Queue
**Which data structure would you use for:**
1. Implementing an Undo feature?
2. Processing HTTP requests in order?
3. Evaluating postfix expressions?
4. Managing a print job queue?

**Answer:**
1. **Stack** - Need to reverse operations (LIFO)
2. **Queue** - Need to process in arrival order (FIFO)
3. **Stack** - Postfix evaluation uses stack for operand storage
4. **Queue** - Print jobs should be processed in order (FIFO)

---

### Question 6: Performance Analysis
**What's the time complexity of these operations?**

```csharp
// Option A: Using built-in Queue<T>
var queue1 = new Queue<int>();
queue1.Enqueue(10);
var item1 = queue1.Dequeue();

// Option B: Using List<T> as queue
var queue2 = new List<int>();
queue2.Add(10);
queue2.RemoveAt(0);
```

**Answer:**
- **Option A:** Both `Enqueue()` and `Dequeue()` are **O(1)** - constant time
- **Option B:** `Add()` is **O(1)**, but `RemoveAt(0)` is **O(n)** - linear time

**Why:** `RemoveAt(0)` requires shifting all remaining elements to the left, making it O(n).

**Recommendation:** Always use built-in `Queue<T>` for queue operations!

---

### Question 7: Implement Stack with Two Queues
**Can you implement a Stack using two Queues?**

**Hint:** When pushing, enqueue to queue1. When popping, move all but one element from queue1 to queue2, pop the last one, then swap queues.

**Answer:**

```csharp
public class StackUsingQueues<T> {
    private Queue<T> queue1 = new Queue<T>();
    private Queue<T> queue2 = new Queue<T>();

    public void Push(T item) {
        queue1.Enqueue(item); // Add to queue1
    }

    public T Pop() {
        // Move all but last element from queue1 to queue2
        while (queue1.Count > 1) {
            queue2.Enqueue(queue1.Dequeue());
        }

        // Get the last element (top of stack)
        T top = queue1.Dequeue();

        // Swap queues
        var temp = queue1;
        queue1 = queue2;
        queue2 = temp;

        return top;
    }

    public int Count => queue1.Count;
}
```

**Time Complexity:**
- `Push()`: O(1)
- `Pop()`: O(n) - need to move all elements

---

### Question 8: Reverse a Queue
**Write a function to reverse a queue using a stack.**

**Answer:**

```csharp
public static Queue<int> ReverseQueue(Queue<int> queue) {
    var stack = new Stack<int>();

    // Dequeue all elements and push to stack
    while (queue.Count > 0) {
        stack.Push(queue.Dequeue());
    }

    // Pop all elements and enqueue back (now reversed)
    while (stack.Count > 0) {
        queue.Enqueue(stack.Pop());
    }

    return queue;
}
```

**Why Stack?** Stack naturally reverses order (LIFO), perfect for reversing FIFO queue!

---

### Question 9: Min Stack Problem
**Design a stack that supports push, pop, top, and retrieving the minimum element in O(1) time.**

**Answer:**

```csharp
public class MinStack {
    private Stack<int> stack = new Stack<int>();
    private Stack<int> minStack = new Stack<int>();

    public void Push(int val) {
        stack.Push(val);
        // Push to minStack only if it's empty or val <= current min
        if (minStack.Count == 0 || val <= minStack.Peek()) {
            minStack.Push(val);
        }
    }

    public void Pop() {
        if (stack.Count == 0) return;

        int val = stack.Pop();
        // Pop from minStack if we're removing the current min
        if (minStack.Count > 0 && val == minStack.Peek()) {
            minStack.Pop();
        }
    }

    public int Top() {
        return stack.Peek();
    }

    public int GetMin() {
        return minStack.Peek(); // O(1) - always the minimum
    }
}
```

**Key Insight:** Use a second stack to track minimum values at each level.

---

### Question 10: Implement Queue with Two Stacks
**Can you implement a Queue using two Stacks?**

**Answer:**

```csharp
public class QueueUsingStacks<T> {
    private Stack<T> stack1 = new Stack<T>(); // For enqueue
    private Stack<T> stack2 = new Stack<T>(); // For dequeue

    public void Enqueue(T item) {
        stack1.Push(item); // O(1) - just push to stack1
    }

    public T Dequeue() {
        // If stack2 is empty, move all from stack1 to stack2
        if (stack2.Count == 0) {
            while (stack1.Count > 0) {
                stack2.Push(stack1.Pop());
            }
        }

        // Pop from stack2 (FIFO order achieved!)
        return stack2.Pop();
    }

    public int Count => stack1.Count + stack2.Count;
}
```

**Time Complexity:**
- `Enqueue()`: O(1) - amortized constant time
- `Dequeue()`: O(1) - amortized constant time (O(n) worst case when stack2 is empty)

**How it works:** Stack2 reverses the order, making LIFO behave like FIFO!

---

### Question 11: Browser History
**How would you implement browser back/forward buttons?**

**Answer:** Use two stacks!

```csharp
public class BrowserHistory {
    private Stack<string> backStack = new Stack<string>();
    private Stack<string> forwardStack = new Stack<string>();
    private string currentPage;

    public void Navigate(string url) {
        if (currentPage != null) {
            backStack.Push(currentPage); // Save current page
        }
        currentPage = url;
        forwardStack.Clear(); // Clear forward history when navigating
    }

    public string GoBack() {
        if (backStack.Count == 0) return currentPage;

        forwardStack.Push(currentPage);
        currentPage = backStack.Pop();
        return currentPage;
    }

    public string GoForward() {
        if (forwardStack.Count == 0) return currentPage;

        backStack.Push(currentPage);
        currentPage = forwardStack.Pop();
        return currentPage;
    }
}
```

**Why Stacks?** Need LIFO to reverse navigation history!

---

### Question 12: Check if Queue is Empty
**Your code throws an exception when dequeuing from an empty queue. What's wrong?**

```csharp
public void ProcessQueue(Queue<int> queue) {
    while (queue.Count != 0) {
        int item = queue.Dequeue();
        // Process item...
    }
}
```

**Answer:** Actually, this code is correct! It checks `queue.Count != 0` before dequeuing.

**Common mistake:** Calling `Dequeue()` without checking:

```csharp
// WRONG - throws exception if queue is empty
int item = queue.Dequeue();

// CORRECT - check first
if (queue.Count > 0) {
    int item = queue.Dequeue();
}
```

**Alternative:** Use `TryDequeue()`:

```csharp
if (queue.TryDequeue(out int item)) {
    // Process item
}
```

---

### Question 13: Postfix Expression Evaluation
**Evaluate the postfix expression "3 4 + 2 *" using a stack.**

**Answer:**

```csharp
public static int EvaluatePostfix(string expression) {
    var stack = new Stack<int>();
    string[] tokens = expression.Split(' ');

    foreach (string token in tokens) {
        if (int.TryParse(token, out int num)) {
            stack.Push(num); // Push operands
        }
        else {
            // Pop two operands for operation
            int b = stack.Pop();
            int a = stack.Pop();

            int result = token switch {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => throw new ArgumentException("Invalid operator")
            };

            stack.Push(result); // Push result back
        }
    }

    return stack.Pop(); // Final result
}
```

**Example:** `"3 4 + 2 *"` → Push 3, Push 4, Pop 4, Pop 3, Push 7, Push 2, Pop 2, Pop 7, Push 14 → Result: 14

**Why Stack?** Need to access the most recent operands first (LIFO).

---

### Question 14: Sliding Window Maximum
**Given an array and window size k, find the maximum in each window.**

**Hint:** Use a deque (double-ended queue) or think about which data structure maintains order.

**Answer:** Use a deque (can be implemented with two stacks or Queue):

```csharp
public static int[] MaxSlidingWindow(int[] nums, int k) {
    var result = new List<int>();
    var deque = new LinkedList<int>(); // Stores indices

    for (int i = 0; i < nums.Length; i++) {
        // Remove indices outside current window
        while (deque.Count > 0 && deque.First.Value < i - k + 1) {
            deque.RemoveFirst();
        }

        // Remove indices whose values are smaller than current
        while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i]) {
            deque.RemoveLast();
        }

        deque.AddLast(i);

        // Add max to result when window is complete
        if (i >= k - 1) {
            result.Add(nums[deque.First.Value]);
        }
    }

    return result.ToArray();
}
```

**Note:** This is an advanced problem, but demonstrates when you need a deque (queue that allows removal from both ends).

---

### Question 15: Explain in Simple Terms
**Explain what a Stack is to a non-programmer.**

**Answer Template:**
"A stack is like a stack of plates at a restaurant. You can only:
- Add a plate to the top (push)
- Remove a plate from the top (pop)

You can't grab a plate from the middle! This is called 'Last In, First Out' - the last plate you added is the first one you can remove. It's perfect for:
- Undo buttons (undo your last action)
- Browser back button (go back to last page)
- Function calls (return to last function)

The opposite is a queue, which is like a line at a store - first person in line is served first!"

---

## Tips for Stack/Queue Interview Questions

1. **Visualize the operations** - Draw out push/pop or enqueue/dequeue
2. **Know when to use each** - Stack for reversing/LIFO, Queue for FIFO
3. **Check for empty before operations** - Always validate Count > 0
4. **Understand performance** - Built-in classes are O(1), List implementations may be O(n)
5. **Think about real-world analogies** - Makes explanations easier
6. **Practice common problems** - Balanced parentheses, postfix evaluation, reverse queue
7. **Consider combined data structures** - Min stack, queue with two stacks, etc.
8. **Know the terminology** - Push/Pop vs Enqueue/Dequeue, Front vs Back

---

## Common Interview Patterns

### Pattern 1: Stack for Matching Problems
- Balanced parentheses
- Matching tags (HTML)
- Valid bracket sequences

### Pattern 2: Stack for Reversal
- Reverse a string
- Reverse a queue
- Undo operations

### Pattern 3: Queue for Order Processing
- Task scheduling
- Level-order traversal (BFS)
- Request processing

### Pattern 4: Combined Structures
- Min/Max stack
- Queue with two stacks
- Stack with two queues

---

## Additional Resources

- Practice implementing Stack and Queue from scratch
- Study common LeetCode problems: Valid Parentheses, Implement Queue using Stacks, etc.
- Understand when to use Deque (double-ended queue)
- Learn about Priority Queues (advanced topic)

---
