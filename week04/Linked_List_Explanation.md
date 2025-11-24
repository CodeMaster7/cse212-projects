# Linked Lists - Implementation Guide

## What is a Linked List?

A **linked list** is a data structure where elements are stored in **scattered locations** in memory (not next to each other like arrays). Each element (called a **node**) contains:

1. The actual **value/data**
2. A **pointer** to the next node's memory location

Think of it like a treasure hunt - each clue (node) tells you where to find the next clue!

---

## Linked List vs Dynamic Array

### Dynamic Array Structure

```
Memory: [8][12][31][15][4][42][27][ ]
         ↑ All items are RIGHT NEXT TO each other
```

-   **Contiguous memory** (items are neighbors)
-   Fast access by index: `address = start + (index × size)`
-   Good for: accessing random items quickly

### Linked List Structure

```
Memory scattered everywhere:
[8]→ [12]→ [31]→ [15]→ [4]
↑                       ↑
HEAD                   TAIL
```

-   **Non-contiguous memory** (items scattered around)
-   Must follow pointers to traverse
-   Good for: inserting/removing from beginning

---

## Types of Linked Lists

### Singly Linked List

Each node points to the **NEXT** node only (one direction):

```
HEAD → [8]→[12]→[31]→[15]→[4] → null
```

### Doubly Linked List

Each node points **BOTH** directions (next AND previous):

```
      ┌──────────────────────────────┐
HEAD  ↓                              ↓  TAIL
null ← [8] ⇄ [12] ⇄ [31] ⇄ [15] ⇄ [4] → null
```

**Each node has:**

-   `Value`: the data
-   `Next`: pointer to next node
-   `Prev`: pointer to previous node

---

## Inserting into a Linked List

### 1. Insert at the HEAD (Beginning)

**Four Steps:**

```csharp
// Step 1: Create new node
var newNode = new Node(42);

// Step 2: New node's NEXT points to current HEAD
newNode.Next = head;

// Step 3: Current HEAD's PREV points back to new node
head.Prev = newNode;

// Step 4: HEAD now points to the new node
head = newNode;
```

**Visual:**

```
Before: HEAD → [8] ⇄ [12] ⇄ [31]

Step 1-2: [42] → [8] ⇄ [12] ⇄ [31]

Step 3-4: HEAD → [42] ⇄ [8] ⇄ [12] ⇄ [31]
```

**Special Case:** If list is empty (`head == null`), then both `head` and `tail` point to the new node.

**Performance:** O(1) - Only adjusting a few pointers, no loops!

---

### 2. Insert at the TAIL (End)

**Four Steps:**

```csharp
// Step 1: Create new node
var newNode = new Node(23);

// Step 2: New node's PREV points to current TAIL
newNode.Prev = tail;

// Step 3: Current TAIL's NEXT points to new node
tail.Next = newNode;

// Step 4: TAIL now points to the new node
tail = newNode;
```

**Visual:**

```
Before: [8] ⇄ [12] ⇄ [31] ← TAIL

Step 1-2: [8] ⇄ [12] ⇄ [31] → [23]

Step 3-4: [8] ⇄ [12] ⇄ [31] ⇄ [23] ← TAIL
```

**Performance:** O(1) - Just pointer adjustments!

---

### 3. Insert in the MIDDLE

**Five Steps** (inserting AFTER a node called `current`):

```csharp
// Step 1: Create new node
var newNode = new Node(50);

// Step 2: New node's PREV points to current
newNode.Prev = current;

// Step 3: New node's NEXT points to the node after current
newNode.Next = current.Next;

// Step 4: The node after current points back to new node
current.Next.Prev = newNode;

// Step 5: Current node's NEXT points to new node
current.Next = newNode;
```

**Visual:**

```
Before: [8] ⇄ [12] ⇄ [31] ⇄ [15] ⇄ [4]
                    ↑
                 current

After:  [8] ⇄ [12] ⇄ [31] ⇄ [50] ⇄ [15] ⇄ [4]
                    ↑       ↑
                 current  new node
```

**Performance:** O(n) - Need to FIND the node first by looping through list. The insertion itself is O(1).

---

## Removing from a Linked List

### 1. Remove the HEAD (First Node)

**Two Steps:**

```csharp
// Step 1: Second node's PREV becomes null (no more previous node)
head.Next.Prev = null;

// Step 2: HEAD moves to the second node
head = head.Next;
```

**Visual:**

```
Before: HEAD → [8] ⇄ [12] ⇄ [31] ⇄ [15]

After:  HEAD → [12] ⇄ [31] ⇄ [15]
        [8] is now disconnected and will be garbage collected
```

**Special Case:** If there's only one node, set both `head` and `tail` to `null`.

**Performance:** O(1) - Quick pointer updates!

---

### 2. Remove the TAIL (Last Node)

**Two Steps:**

```csharp
// Step 1: Second-to-last node's NEXT becomes null
tail.Prev.Next = null;

// Step 2: TAIL moves to second-to-last node
tail = tail.Prev;
```

**Visual:**

```
Before: [8] ⇄ [12] ⇄ [31] ⇄ [15] ← TAIL

After:  [8] ⇄ [12] ⇄ [31] ← TAIL
        [15] is disconnected
```

**Performance:** O(1) - Just pointer adjustments!

---

### 3. Remove from MIDDLE

**Two Steps** (removing node called `current`):

```csharp
// Step 1: Node after current points back to node before current
current.Next.Prev = current.Prev;

// Step 2: Node before current points forward to node after current
current.Prev.Next = current.Next;
```

**Visual:**

```
Before: [8] ⇄ [12] ⇄ [31] ⇄ [15] ⇄ [4]
                    ↑
                 current (remove this!)

After:  [8] ⇄ [12] ⇄ [15] ⇄ [4]
        [31] is now disconnected (both neighbors skip over it)
```

**Performance:** O(n) - Need to FIND the node first. Removal itself is O(1).

---

## Traversing (Looping Through) a Linked List

### Going Forward (HEAD to TAIL)

```csharp
private void GoForward()
{
    // Start at the beginning
    var current = head;

    // Loop until we reach the end (null)
    while (current != null)
    {
        // Do something with current node
        Console.WriteLine(current.Data);

        // Move to next node by following the pointer
        current = current.Next;
    }
}
```

**Why this works:**

-   Start at `head` (first node)
-   Each time through loop, move to `current.Next`
-   When we reach past the last node, `current` becomes `null` and loop stops

**Performance:** O(n) - Must visit every node

---

### Going Backward (TAIL to HEAD)

```csharp
private void GoBackward()
{
    // Start at the end
    var current = tail;

    // Loop until we reach before the beginning (null)
    while (current != null)
    {
        // Do something with current node
        Console.WriteLine(current.Data);

        // Move to previous node by following the pointer
        current = current.Prev;
    }
}
```

**Advantage of doubly-linked lists:** Can traverse BOTH directions!

---

## C# LinkedList Class

C# provides `LinkedList<T>` built-in. Here are the common operations:

| Operation             | Description      | C# Code                            | Performance                   |
| --------------------- | ---------------- | ---------------------------------- | ----------------------------- |
| `insert_head(value)`  | Add at beginning | `linkedList.AddFirst(value)`       | **O(1)** ✓                    |
| `insert_tail(value)`  | Add at end       | `linkedList.AddLast(value)`        | **O(1)** ✓                    |
| `insert(node, value)` | Add after a node | `linkedList.AddAfter(node, value)` | **O(n)** - need to find node  |
| `remove_head()`       | Remove first     | `linkedList.RemoveFirst()`         | **O(1)** ✓                    |
| `remove_tail()`       | Remove last      | `linkedList.RemoveLast()`          | **O(1)** ✓                    |
| `remove(node)`        | Remove a node    | `linkedList.Remove(node)`          | **O(n)** - need to find node  |
| `size()`              | Get count        | `linkedList.Count`                 | **O(1)** - stored in variable |
| `empty()`             | Check if empty   | `linkedList.Count == 0`            | **O(1)**                      |

---

## Performance Comparison: Dynamic Array vs Linked List

| Operation           | Dynamic Array                   | Linked List                   | Winner           |
| ------------------- | ------------------------------- | ----------------------------- | ---------------- |
| Insert Front        | **O(n)** - must shift all items | **O(1)** - just pointers      | 🏆 Linked List   |
| Insert Middle       | **O(n)** - must shift items     | **O(n)** - must find location | Tie              |
| Insert End          | **O(1)** - direct access        | **O(1)** - have tail pointer  | Tie              |
| Remove Front        | **O(n)** - must shift all items | **O(1)** - just pointers      | 🏆 Linked List   |
| Remove Middle       | **O(n)** - must shift items     | **O(n)** - must find location | Tie              |
| Remove End          | **O(1)** - direct access        | **O(1)** - have tail pointer  | Tie              |
| **Access by Index** | **O(1)** - calculate address    | **O(n)** - must traverse      | 🏆 Dynamic Array |

---

## When to Use Each?

### Use Dynamic Array when:

-   ✓ Need fast random access by index
-   ✓ Mostly adding/removing from the END
-   ✓ Know size ahead of time
-   ✓ Example: **Stack** (only uses the end)

### Use Linked List when:

-   ✓ Frequently adding/removing from the BEGINNING
-   ✓ Frequently adding/removing from both ends
-   ✓ Don't know size ahead of time
-   ✓ Don't need random access by index
-   ✓ Example: **Queue** (uses both front and back)

**Key Insight:**

-   **Stack** can use either (only uses one end)
-   **Queue** should use linked list (uses both ends, and linked list is O(1) at both ends!)

---

## Complete Node Implementation Example

```csharp
/// <summary>
/// Node class for doubly-linked list
/// Contains data and pointers to next/previous nodes
/// </summary>
public class Node<T>
{
    // The actual data stored in this node
    public T Data { get; set; }

    // Pointer to the next node (null if this is the last node)
    public Node<T>? Next { get; set; }

    // Pointer to the previous node (null if this is the first node)
    public Node<T>? Prev { get; set; }

    // Constructor to create a new node
    public Node(T data)
    {
        Data = data;
        Next = null;  // No next node yet
        Prev = null;  // No previous node yet
    }
}
```

---

## Common Interview Questions & Answers

### Question 1: Reverse a Linked List

**Problem:** Reverse a singly-linked list in-place.

**Approach:**
As we traverse the list, flip each node's `Next` pointer to point backwards instead of forwards.

```csharp
/// <summary>
/// Reverses a singly-linked list by changing pointer directions
/// Time: O(n), Space: O(1)
/// </summary>
public Node<T> Reverse(Node<T> head)
{
    // These three pointers help us "flip" each connection
    Node<T>? prev = null;      // Previous node (starts as null)
    Node<T>? current = head;   // Current node we're working on
    Node<T>? next = null;      // Temporary storage for next node

    // Loop through entire list
    while (current != null)
    {
        // Step 1: Save the next node (so we don't lose it!)
        next = current.Next;

        // Step 2: Flip the pointer - current now points BACKWARDS
        current.Next = prev;

        // Step 3: Move prev and current forward by one node
        prev = current;
        current = next;
    }

    // After loop, prev is at the old tail (new head!)
    return prev;
}
```

**Example:**

```
Before: [1]→[2]→[3]→[4]→null

After:  null←[1]←[2]←[3]←[4]
        (reversed, [4] is new head)
```

**Performance:** O(n) time, O(1) space

---

### Question 2: Detect a Cycle in a Linked List

**Problem:** Determine if a linked list has a cycle (a node's `Next` points back to a previous node, creating a loop).

**Approach:** Use "Floyd's Cycle Detection" (tortoise and hare algorithm)

-   Slow pointer moves 1 step at a time
-   Fast pointer moves 2 steps at a time
-   If there's a cycle, fast will eventually catch up to slow
-   If there's no cycle, fast will reach the end (null)

```csharp
/// <summary>
/// Detects if linked list has a cycle using two-pointer technique
/// Time: O(n), Space: O(1)
/// </summary>
public bool HasCycle(Node<T> head)
{
    // Edge case: empty list has no cycle
    if (head == null) return false;

    // Slow moves 1 step, fast moves 2 steps
    Node<T> slow = head;
    Node<T> fast = head;

    // Keep going while fast can move 2 steps
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;           // Move slow by 1
        fast = fast.Next.Next;      // Move fast by 2

        // If they meet, there's a cycle!
        if (slow == fast)
        {
            return true;
        }
    }

    // Fast reached the end (null), so no cycle
    return false;
}
```

**Why this works:**

-   Imagine a circular race track
-   One runner goes 1 mph, another goes 2 mph
-   Eventually the faster runner will "lap" the slower one
-   If they meet, it proves it's circular!

**Performance:** O(n) time, O(1) space

---

### Question 3: Find the Middle Node

**Problem:** Find the middle node of a linked list in one pass.

**Approach:** Use two-pointer technique

-   Slow pointer moves 1 step at a time
-   Fast pointer moves 2 steps at a time
-   When fast reaches the end, slow is at the middle!

```csharp
/// <summary>
/// Finds the middle node using two-pointer technique
/// Time: O(n), Space: O(1)
/// If even number of nodes, returns the second middle node
/// </summary>
public Node<T> FindMiddle(Node<T> head)
{
    // Edge case: empty or single node
    if (head == null) return null;

    Node<T> slow = head;
    Node<T> fast = head;

    // Fast moves twice as fast as slow
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;           // Move slow by 1
        fast = fast.Next.Next;      // Move fast by 2
    }

    // When fast reaches end, slow is at middle!
    return slow;
}
```

**Example:**

```
List: [1]→[2]→[3]→[4]→[5]

When fast reaches [5], slow is at [3] (middle!)

List: [1]→[2]→[3]→[4]→[5]→[6]

When fast reaches end, slow is at [4] (second middle)
```

**Performance:** O(n) time, O(1) space

---

### Question 4: Remove Nth Node From End

**Problem:** Remove the nth node from the end of the list in one pass.

**Approach:** Use two-pointer technique with a "gap"

-   Move `fast` pointer n steps ahead
-   Then move both `fast` and `slow` together
-   When `fast` reaches end, `slow` is right before the node to remove

```csharp
/// <summary>
/// Removes the nth node from the end of the list
/// Time: O(n), Space: O(1)
/// </summary>
public Node<T> RemoveNthFromEnd(Node<T> head, int n)
{
    // Create a dummy node to handle edge cases (like removing head)
    var dummy = new Node<T>(default(T));
    dummy.Next = head;

    Node<T> fast = dummy;
    Node<T> slow = dummy;

    // Move fast pointer n+1 steps ahead
    // +1 so slow ends up BEFORE the node to remove
    for (int i = 0; i <= n; i++)
    {
        fast = fast.Next;
    }

    // Move both pointers until fast reaches the end
    while (fast != null)
    {
        slow = slow.Next;
        fast = fast.Next;
    }

    // Now slow is right before the node to remove
    // Skip over the node to remove it
    slow.Next = slow.Next.Next;

    return dummy.Next;  // Return real head (not dummy)
}
```

**Example:**

```
Remove 2nd from end in: [1]→[2]→[3]→[4]→[5]

Step 1: Move fast 2 steps ahead
        slow         fast
         ↓            ↓
[dummy]→[1]→[2]→[3]→[4]→[5]

Step 2: Move both until fast reaches end
                    slow         fast
                     ↓            ↓
[dummy]→[1]→[2]→[3]→[4]→[5]→null

Step 3: Skip node after slow
Result: [1]→[2]→[3]→[5]
```

**Performance:** O(n) time, O(1) space

---

### Question 5: Merge Two Sorted Linked Lists

**Problem:** Merge two sorted linked lists into one sorted list.

**Approach:** Use a pointer to build a new list by comparing nodes

-   Compare the first node of each list
-   Add the smaller one to result
-   Move that list's pointer forward
-   Repeat until one list is empty
-   Attach remaining nodes from non-empty list

```csharp
/// <summary>
/// Merges two sorted linked lists into one sorted list
/// Time: O(n + m), Space: O(1) - just rearranging pointers
/// </summary>
public Node<T> MergeTwoLists(Node<T> list1, Node<T> list2)
    where T : IComparable<T>
{
    // Dummy node makes code cleaner (no special case for first node)
    var dummy = new Node<T>(default(T));
    var current = dummy;

    // While both lists have nodes
    while (list1 != null && list2 != null)
    {
        // Compare the two front nodes
        if (list1.Data.CompareTo(list2.Data) <= 0)
        {
            // list1's node is smaller, add it to result
            current.Next = list1;
            list1 = list1.Next;  // Move list1 forward
        }
        else
        {
            // list2's node is smaller, add it to result
            current.Next = list2;
            list2 = list2.Next;  // Move list2 forward
        }

        // Move result pointer forward
        current = current.Next;
    }

    // One list is empty, attach the remaining nodes from the other
    // (They're already sorted, so just attach the rest!)
    current.Next = list1 != null ? list1 : list2;

    return dummy.Next;  // Return real head (skip dummy)
}
```

**Example:**

```
List1: [1]→[3]→[5]
List2: [2]→[4]→[6]

Result: [1]→[2]→[3]→[4]→[5]→[6]
```

**Performance:** O(n + m) time where n and m are lengths of the two lists, O(1) space

---

### Question 6: Check if Linked List is Palindrome

**Problem:** Determine if a linked list reads the same forwards and backwards.

**Approach:**

1. Find the middle using two-pointer technique
2. Reverse the second half of the list
3. Compare first half with reversed second half

```csharp
/// <summary>
/// Checks if linked list is a palindrome
/// Time: O(n), Space: O(1)
/// </summary>
public bool IsPalindrome(Node<T> head) where T : IEquatable<T>
{
    if (head == null) return true;

    // Step 1: Find middle using slow/fast pointers
    Node<T> slow = head;
    Node<T> fast = head;

    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;
        fast = fast.Next.Next;
    }

    // Step 2: Reverse second half (starting from slow)
    Node<T>? prev = null;
    Node<T>? current = slow;

    while (current != null)
    {
        Node<T>? next = current.Next;
        current.Next = prev;
        prev = current;
        current = next;
    }

    // Step 3: Compare first half with reversed second half
    Node<T>? left = head;
    Node<T>? right = prev;  // prev is now head of reversed second half

    while (right != null)  // Only need to check second half length
    {
        if (!left.Data.Equals(right.Data))
        {
            return false;  // Found mismatch, not a palindrome
        }
        left = left.Next;
        right = right.Next;
    }

    return true;  // All matched!
}
```

**Example:**

```
Palindrome: [1]→[2]→[3]→[2]→[1]
            ↑           ↑
            Compare these, they match!

Not Palindrome: [1]→[2]→[3]→[4]
                ↑           ↑
                1 ≠ 4, not a palindrome
```

**Performance:** O(n) time, O(1) space

---

### Question 7: Find Intersection of Two Linked Lists

**Problem:** Find the node where two linked lists intersect (share the same nodes).

**Approach:**

-   Calculate length of both lists
-   Move the pointer of the longer list forward by the difference
-   Move both pointers together until they meet

```csharp
/// <summary>
/// Finds the node where two linked lists intersect
/// Time: O(n + m), Space: O(1)
/// </summary>
public Node<T> GetIntersectionNode(Node<T> headA, Node<T> headB)
{
    if (headA == null || headB == null) return null;

    // Step 1: Calculate lengths of both lists
    int lenA = GetLength(headA);
    int lenB = GetLength(headB);

    // Step 2: Move the longer list's pointer forward by difference
    while (lenA > lenB)
    {
        headA = headA.Next;
        lenA--;
    }

    while (lenB > lenA)
    {
        headB = headB.Next;
        lenB--;
    }

    // Step 3: Move both pointers together until they meet
    while (headA != headB)
    {
        headA = headA.Next;
        headB = headB.Next;
    }

    // Either the intersection node or null (no intersection)
    return headA;
}

/// <summary>
/// Helper method to get length of linked list
/// </summary>
private int GetLength(Node<T> head)
{
    int length = 0;
    while (head != null)
    {
        length++;
        head = head.Next;
    }
    return length;
}
```

**Visual:**

```
List A: [1]→[2]→[3]
                  ↘
                   [7]→[8]→[9]
                  ↗
List B:     [4]→[5]

Intersection at node [7]
```

**Performance:** O(n + m) time, O(1) space

---

## Additional Practice Problems

Try implementing these yourself:

1. **Remove Duplicates**: Remove all duplicate values from an unsorted linked list
2. **Partition List**: Partition list around value x (all nodes < x before nodes ≥ x)
3. **Add Two Numbers**: Two linked lists represent numbers, add them (each node is one digit)
4. **Rotate List**: Rotate list to the right by k places
5. **Reorder List**: Reorder L₀→L₁→...→Ln₋₁→Ln to L₀→Ln→L₁→Ln₋₁→L₂→Ln₋₂→...
6. **Copy List with Random Pointer**: Deep copy a linked list where each node has a random pointer
7. **Flatten Multilevel List**: Flatten a linked list that has child pointers creating sublists

---

## Key Takeaways

✅ **Linked lists use pointers** to connect scattered memory locations

✅ **O(1) insertion/removal** at head and tail (with doubly-linked)

✅ **Two-pointer techniques** solve many linked list problems efficiently

✅ **Trade-offs:** Great for insertion/removal at ends, poor for random access

✅ **Use for Queues:** Perfect for queue implementation (O(1) at both ends)

✅ **Common patterns:**

-   Slow/fast pointers for middle/cycle detection
-   Reverse pointers for reversal
-   Dummy nodes to simplify edge cases

---

## Real-World Applications

**Where Linked Lists are Used:**

-   **Music Playlists**: Previous/next song navigation
-   **Browser History**: Back/forward buttons
-   **Undo/Redo Functionality**: Chain of actions
-   **Process Scheduling**: Operating system task management
-   **Image Viewer**: Navigate between images
-   **Train Cars**: Each car linked to next/previous

**Why they're useful:**

-   Easy to insert/remove without shifting
-   Dynamic size (grow/shrink as needed)
-   Efficient for queues and deques
