# Binary Search Trees - Implementation Guide

## Quick Review Questions

### What is a Binary Search Tree (BST)?

**Answer:** A tree data structure where each node has at most two children (left and right), and follows the BST property: all values in the left subtree are less than the node's value, and all values in the right subtree are greater than the node's value.

### What is the average and worst-case time complexity for BST operations?

**Answer:**

-   **Average case:** O(log n) for insert, search, and delete (when tree is balanced)
-   **Worst case:** O(n) when the tree becomes unbalanced (like a linked list)

### Why is tree balance important?

**Answer:** An unbalanced tree degrades to O(n) performance. For example, inserting sorted values 1, 2, 3, 4, 5 creates a "linked list" shape. A balanced tree ensures O(log n) operations by keeping the height minimal (approximately log₂(n)).

---

## What is a Tree?

A **tree** is a hierarchical data structure made up of **nodes** connected by **edges**. Unlike linked lists which are linear, trees branch out like an upside-down tree in nature!

**Key terminology:**

-   **Root:** The topmost node (like the trunk of a tree, but upside down!)
-   **Parent:** A node that has children
-   **Child:** A node connected below another node
-   **Leaf:** A node with no children (the end of a branch)
-   **Height:** The longest path from root to any leaf
-   **Subtree:** A node and all its descendants

```
         5        ← Root
       /   \
      3     7     ← Children of 5
     / \   / \
    1   4 6  10   ← Leaves (no children)
```

---

## What is a Binary Search Tree (BST)?

A **Binary Search Tree** is a special binary tree where:

1. Each node has **at most 2 children** (left and right)
2. **Left child < Parent < Right child** (the BST property)
3. This property applies to the **entire subtree**, not just immediate children

**Why is this useful?** The ordering makes searching incredibly fast - you eliminate half the tree with each comparison!

```csharp
// BST Property Visualization:
//
//          50
//        /    \
//      30      70
//     /  \    /  \
//   20   40  60   80
//
// Everything < 50 is on the left
// Everything > 50 is on the right
// This rule applies at EVERY node!
```

---

## Node Structure

Each node in a BST contains:

```csharp
public class Node
{
    public int Data { get; set; }        // The value stored
    public Node? Right { get; private set; }  // Right child (greater values)
    public Node? Left { get; private set; }   // Left child (lesser values)

    public Node(int data)
    {
        this.Data = data;
    }
}
```

**Why use `Node?` (nullable)?**

-   A node might not have a left or right child
-   The `?` tells C# "this can be null"
-   We check for `null` to know when we've reached a leaf

---

## Core BST Operations

### 1. Insert - Adding Values

**Goal:** Place a new value in the correct position to maintain BST property.

**Algorithm:**

1. Compare new value with current node
2. If smaller → go left
3. If larger → go right
4. If equal → don't insert (no duplicates)
5. When you find an empty spot, insert there!

```csharp
/// <summary>
/// Inserts a value into the BST recursively.
/// Time: O(log n) average, O(n) worst case
/// Space: O(log n) for call stack
/// </summary>
public void Insert(int value)
{
    // Don't allow duplicates - silently ignore if already exists
    if (value == Data)
    {
        return;
    }

    if (value < Data)
    {
        // Value is smaller, so it belongs on the LEFT side
        if (Left is null)
        {
            // Found empty spot! Create new node here
            Left = new Node(value);
        }
        else
        {
            // Spot is taken, keep looking down the left subtree
            Left.Insert(value);
        }
    }
    else // value > Data
    {
        // Value is larger, so it belongs on the RIGHT side
        if (Right is null)
        {
            // Found empty spot! Create new node here
            Right = new Node(value);
        }
        else
        {
            // Spot is taken, keep looking down the right subtree
            Right.Insert(value);
        }
    }
}
```

**Example trace - Inserting 4 into this tree:**

```
Start:     After Insert(4):
   5            5
  / \          / \
 3   7   →    3   7
             /
            4

Steps:
1. 4 < 5, go left to node 3
2. 4 > 3, go right (which is empty)
3. Insert 4 as right child of 3
```

---

### 2. Contains (Search) - Finding Values

**Goal:** Check if a value exists in the tree.

**Algorithm:**

1. Compare target with current node
2. If equal → found it!
3. If smaller → search left
4. If larger → search right
5. If you hit null → not found

```csharp
/// <summary>
/// Searches for a value in the BST recursively.
/// Time: O(log n) average, O(n) worst case
/// Space: O(log n) for call stack
/// </summary>
public bool Contains(int value)
{
    // BASE CASE 1: Found the value!
    if (value == Data)
    {
        return true;
    }

    if (value < Data)
    {
        // Value would be on the left (if it exists)
        // If there's no left child, value doesn't exist
        return Left != null && Left.Contains(value);
    }
    else // value > Data
    {
        // Value would be on the right (if it exists)
        // If there's no right child, value doesn't exist
        return Right != null && Right.Contains(value);
    }
}
```

**Why this is efficient:**

```
Searching for 6 in a balanced tree of 15 nodes:

       8
      / \
     4   12
    /\   /\
   2  6 10 14

Step 1: 6 < 8, go left
Step 2: 6 > 4, go right
Step 3: 6 == 6, FOUND!

Only 3 comparisons instead of checking all 15 nodes!
```

---

### 3. Tree Traversal

Traversal means visiting every node in a specific order. There are three main ways:

#### In-Order Traversal (Left → Root → Right)

This gives you values in **sorted order**! It's the most common traversal.

```csharp
/// <summary>
/// In-order traversal: Left, Root, Right
/// Results in ascending sorted order
/// </summary>
private void TraverseForward(Node? node, List<int> values)
{
    if (node is not null)
    {
        TraverseForward(node.Left, values);   // 1. Visit all left children
        values.Add(node.Data);                 // 2. Add this node's value
        TraverseForward(node.Right, values);  // 3. Visit all right children
    }
}
```

**Example:**

```
       5
      / \
     3   7
    / \
   1   4

In-order: 1, 3, 4, 5, 7 (sorted!)
```

#### Reverse In-Order Traversal (Right → Root → Left)

This gives values in **descending order**:

```csharp
/// <summary>
/// Reverse in-order traversal: Right, Root, Left
/// Results in descending sorted order
/// </summary>
private void TraverseBackward(Node? node, List<int> values)
{
    if (node is not null)
    {
        TraverseBackward(node.Right, values);  // 1. Visit all right children
        values.Add(node.Data);                  // 2. Add this node's value
        TraverseBackward(node.Left, values);   // 3. Visit all left children
    }
}
```

**Example:**

```
       5
      / \
     3   7
    / \
   1   4

Reverse in-order: 7, 5, 4, 3, 1 (reverse sorted!)
```

---

### 4. Getting Tree Height

**Height** = The longest path from root to any leaf node.

An empty tree has height 0. A tree with just a root has height 1.

```csharp
/// <summary>
/// Calculates the height of the subtree rooted at this node.
/// Time: O(n) - must visit every node
/// Space: O(h) where h is height (call stack)
/// </summary>
public int GetHeight()
{
    // Get height of left subtree (0 if no left child)
    int leftHeight = Left?.GetHeight() ?? 0;

    // Get height of right subtree (0 if no right child)
    int rightHeight = Right?.GetHeight() ?? 0;

    // Height = 1 (for this node) + the taller of the two subtrees
    return 1 + Math.Max(leftHeight, rightHeight);
}
```

**Why `1 + Math.Max(left, right)`?**

```
       5        Height = 3
      / \
     3   7      Height = 2 (from 7's perspective)
    /
   1            Height = 1 (leaf node)

At node 5:
- Left subtree height: 2 (path 5→3→1)
- Right subtree height: 1 (path 5→7)
- Total: 1 + max(2, 1) = 3
```

---

## Building a Balanced BST from Sorted Data

### The Problem

If you insert sorted data into a BST, you get an unbalanced tree:

```
Inserting: 10, 20, 30, 40, 50 in order

     10
       \
        20
          \
           30
             \
              40
                \
                 50

This is basically a linked list!
Height = 5 (worst case)
Search time = O(n) 😱
```

### The Solution: Insert Middle First

Insert the **middle element first**, then recursively do the same for left and right halves:

```csharp
/// <summary>
/// Recursively inserts the middle element of a range to build a balanced BST.
/// This is a divide-and-conquer approach.
/// Time: O(n), Space: O(log n) for call stack
/// </summary>
private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
{
    // BASE CASE: Invalid range (first > last means no elements to process)
    if (first > last)
    {
        return;
    }

    // Find the middle index of the current range
    int middle = (first + last) / 2;

    // Insert the middle value into the BST
    bst.Insert(sortedNumbers[middle]);

    // RECURSIVE CASE: Process left half (elements before middle)
    InsertMiddle(sortedNumbers, first, middle - 1, bst);

    // RECURSIVE CASE: Process right half (elements after middle)
    InsertMiddle(sortedNumbers, middle + 1, last, bst);
}
```

**Example: Building balanced tree from [10, 20, 30, 40, 50, 60]**

```
Step 1: Insert middle (index 2) = 30
Step 2: Left half [10, 20] - insert middle = 10
Step 3: Right of 10 is [20] - insert 20
Step 4: Right half [40, 50, 60] - insert middle = 50
Step 5: Left of 50 is [40] - insert 40
Step 6: Right of 50 is [60] - insert 60

Order inserted: 30, 10, 20, 50, 40, 60

Result:
       30
      /  \
    10    50
      \   / \
      20 40  60

Height = 3 (optimal for 6 nodes!)
```

---

## Performance Analysis

### Time Complexity

| Operation | Balanced BST | Unbalanced BST |
| --------- | ------------ | -------------- |
| Insert    | O(log n)     | O(n)           |
| Search    | O(log n)     | O(n)           |
| Delete    | O(log n)     | O(n)           |
| Traversal | O(n)         | O(n)           |
| Height    | O(n)         | O(n)           |

### Why O(log n)?

In a balanced tree with n nodes:

-   Height ≈ log₂(n)
-   Each comparison eliminates half the remaining nodes
-   Similar to binary search!

```
n = 1,000 nodes
Balanced height ≈ 10 comparisons
Unbalanced height = 1,000 comparisons

1,000,000 nodes:
Balanced: ~20 comparisons
Unbalanced: 1,000,000 comparisons!
```

### Space Complexity

-   **Tree storage:** O(n) - one node per value
-   **Recursive operations:** O(h) where h is height (call stack depth)
    -   Balanced: O(log n)
    -   Unbalanced: O(n)

---

## BST vs Other Data Structures

| Feature          | Array (sorted) | Linked List | BST (balanced) |
| ---------------- | -------------- | ----------- | -------------- |
| Search           | O(log n)       | O(n)        | O(log n)       |
| Insert           | O(n)           | O(1)\*      | O(log n)       |
| Delete           | O(n)           | O(1)\*      | O(log n)       |
| Sorted iteration | O(n)           | O(n)        | O(n)           |

\*If you have a reference to the position

**When to use a BST:**

-   ✅ Need fast search AND fast insert/delete
-   ✅ Need to maintain sorted order
-   ✅ Data changes frequently
-   ✅ Need to find min/max quickly

**When NOT to use a BST:**

-   ❌ Data rarely changes (sorted array might be better)
-   ❌ Don't need ordering (HashSet is faster)
-   ❌ Memory is very limited (arrays are more compact)

---

## Common BST Patterns

### Pattern 1: Recursive Node Operations

Most BST operations follow this pattern:

```csharp
public ReturnType Operation(params)
{
    // BASE CASE 1: Found what we're looking for
    if (condition)
        return result;

    // RECURSIVE CASE: Go left or right based on comparison
    if (value < Data)
        return Left?.Operation(params) ?? defaultValue;
    else
        return Right?.Operation(params) ?? defaultValue;
}
```

### Pattern 2: Traversal with Accumulator

Collect results during traversal:

```csharp
private void Traverse(Node? node, List<int> results)
{
    if (node is not null)
    {
        Traverse(node.Left, results);   // Process left
        results.Add(node.Data);          // Process current
        Traverse(node.Right, results);  // Process right
    }
}
```

### Pattern 3: Divide and Conquer

Split problems in half:

```csharp
private void Process(int first, int last)
{
    if (first > last) return;  // Base case

    int middle = (first + last) / 2;
    // Process middle
    Process(first, middle - 1);   // Left half
    Process(middle + 1, last);    // Right half
}
```

---

## Common Interview Questions

### Question 1: Find Minimum/Maximum Value

**Minimum:** Go left as far as possible
**Maximum:** Go right as far as possible

```csharp
public int FindMin()
{
    // The leftmost node has the smallest value
    if (Left is null)
        return Data;
    return Left.FindMin();
}

public int FindMax()
{
    // The rightmost node has the largest value
    if (Right is null)
        return Data;
    return Right.FindMax();
}
```

**Time:** O(h) where h is height

---

### Question 2: Validate BST

Check if a tree is a valid BST:

```csharp
public bool IsValidBST(Node? node, int? min = null, int? max = null)
{
    if (node is null)
        return true;

    // Check if current node violates BST property
    if ((min.HasValue && node.Data <= min) ||
        (max.HasValue && node.Data >= max))
        return false;

    // Check left subtree (all values must be < current)
    // Check right subtree (all values must be > current)
    return IsValidBST(node.Left, min, node.Data) &&
           IsValidBST(node.Right, node.Data, max);
}
```

---

### Question 3: Find Closest Value

```csharp
public int FindClosest(int target, int closest)
{
    // Update closest if current is better
    if (Math.Abs(Data - target) < Math.Abs(closest - target))
        closest = Data;

    // Search left or right based on target
    if (target < Data && Left != null)
        return Left.FindClosest(target, closest);
    else if (target > Data && Right != null)
        return Right.FindClosest(target, closest);

    return closest;
}
```

---

### Question 4: Count Nodes in Range

```csharp
public int CountInRange(int low, int high)
{
    int count = 0;

    // Count this node if in range
    if (Data >= low && Data <= high)
        count = 1;

    // Check left subtree if there might be values >= low
    if (Left != null && Data > low)
        count += Left.CountInRange(low, high);

    // Check right subtree if there might be values <= high
    if (Right != null && Data < high)
        count += Right.CountInRange(low, high);

    return count;
}
```

---

## Debugging Tips

### Tip 1: Visualize the Tree

Print the tree structure to understand what's happening:

```csharp
public void PrintTree(string indent = "", bool isLast = true)
{
    Console.WriteLine(indent + (isLast ? "└── " : "├── ") + Data);

    var children = new List<Node?> { Left, Right };
    for (int i = 0; i < children.Count; i++)
    {
        if (children[i] != null)
        {
            children[i]!.PrintTree(
                indent + (isLast ? "    " : "│   "),
                i == children.Count - 1);
        }
    }
}
```

### Tip 2: Check BST Property

After operations, verify the tree is still valid by doing an in-order traversal and checking if values are sorted.

### Tip 3: Watch for Null

Most BST bugs come from forgetting to check for null. Always ask: "What if Left/Right is null?"

---

## Key Takeaways

✅ **BST Property:** Left < Node < Right (at every node!)

✅ **Balanced = Fast:** O(log n) operations when balanced

✅ **Unbalanced = Slow:** O(n) operations when degraded to linked list

✅ **Recursion is natural:** Trees are recursive structures, so recursive algorithms fit perfectly

✅ **In-order traversal = sorted:** Visit left, root, right for ascending order

✅ **Build balanced trees:** Insert middle first when building from sorted data

✅ **Height matters:** Height determines performance - minimize it!

---

## Real-World Applications

**Where BSTs are used:**

-   **Databases:** B-trees (BST variants) power database indexes
-   **File Systems:** Directory structures use tree concepts
-   **Compilers:** Abstract Syntax Trees (AST) for parsing code
-   **Autocomplete:** Trie trees (similar concept) for word suggestions
-   **Game AI:** Decision trees for strategy games
-   **Networking:** Routing tables and IP lookups
-   **3D Graphics:** BSP trees for rendering optimization

**Why they're powerful:**

-   Fast search in large datasets
-   Natural way to represent hierarchical data
-   Efficient for range queries
-   Can be extended (AVL, Red-Black) for guaranteed balance
