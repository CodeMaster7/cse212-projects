# Recursion - Implementation Guide

## Quick Review Questions

### What is the purpose of recursion?

**Answer:** To solve complex problems by breaking them down into smaller, simpler versions of the same problem until reaching a base case that can be solved directly. It's especially useful for problems with hierarchical or nested structures (trees, graphs, divide-and-conquer algorithms).

### What are the two rules of recursion?

**Answer:**

1. **Smaller Problem** - Each recursive call must work on a smaller or simpler version of the problem
2. **Base Case** - There must be a stopping condition where the function returns without recursing, preventing infinite loops

### What is a potential disadvantage of recursion?

**Answer:** Recursion uses more memory due to the call stack (each function call is stored in memory until it completes) and has slower performance compared to iteration because of function call overhead. Deep recursion can also cause stack overflow errors.

---

## What is Recursion?

**Recursion** is when a function calls **itself** to solve a problem. Think of it like looking into two mirrors facing each other - you see reflections within reflections!

Instead of using loops, recursion breaks a big problem into smaller versions of the same problem until it reaches something so simple it can be solved directly.

**Simple analogy:** Imagine eating a stack of pancakes:

-   **Iterative (loop) way:** "I'll eat pancake 1, then pancake 2, then pancake 3..."
-   **Recursive way:** "I'll eat the top pancake, then eat the rest of the stack" (and "rest of the stack" is the same problem, just smaller!)

---

## The Two Golden Rules of Recursion

Every recursive function MUST follow these rules or it will run forever:

### **Rule 1: Smaller Problem** 🔽

Each recursive call must work on a **smaller** version of the problem.

```csharp
// BAD - runs forever!
public void BadFunction(int n)
{
    BadFunction(n);  // Same problem, not smaller!
}

// GOOD - gets smaller!
public void GoodFunction(int n)
{
    GoodFunction(n - 1);  // Smaller problem!
}
```

### **Rule 2: Base Case** 🛑

You need a **stopping point** where recursion doesn't happen.

```csharp
// Without base case - runs forever!
public int NoBaseCase(int n)
{
    return n + NoBaseCase(n - 1);  // Never stops!
}

// With base case - stops eventually!
public int WithBaseCase(int n)
{
    if (n <= 0)        // BASE CASE - stop here!
        return 0;

    return n + WithBaseCase(n - 1);  // Smaller problem
}
```

---

## How Recursion Works: The Call Stack

When a function calls itself, each call is added to the **call stack** (like stacking plates):

```csharp
public int Factorial(int n)
{
    if (n <= 1)
        return 1;           // Base case

    return n * Factorial(n - 1);
}
```

**What happens when we call `Factorial(4)`:**

```
Call Stack (grows downward):
┌─────────────────────────┐
│ Factorial(4)            │ → Waiting for Factorial(3)...
│   return 4 * ???        │
├─────────────────────────┤
│ Factorial(3)            │ → Waiting for Factorial(2)...
│   return 3 * ???        │
├─────────────────────────┤
│ Factorial(2)            │ → Waiting for Factorial(1)...
│   return 2 * ???        │
├─────────────────────────┤
│ Factorial(1)            │ → BASE CASE! Return 1
│   return 1              │
└─────────────────────────┘

Now unwinding (going back up):
Factorial(2) = 2 * 1 = 2
Factorial(3) = 3 * 2 = 6
Factorial(4) = 4 * 6 = 24 ✓
```

---

## Simple Example: Sum Numbers from 1 to N

**Problem:** Find 1 + 2 + 3 + ... + n

### **Step 1: Define in terms of smaller problem**

```
Sum(n) = n + Sum(n-1)
```

**Why this works:**

-   Sum(5) = 5 + Sum(4)
-   Sum(4) = 4 + Sum(3)
-   Sum(3) = 3 + Sum(2)
-   Sum(2) = 2 + Sum(1)
-   Sum(1) = 1 ← BASE CASE

### **Step 2: Identify base case**

What's the simplest version we can solve without recursion?

```
Sum(1) = 1  (or Sum(0) = 0)
```

### **Step 3: Write the code**

```csharp
/// <summary>
/// Returns the sum of numbers from 1 to n using recursion
/// Time: O(n), Space: O(n) due to call stack
/// </summary>
public int Sum(int n)
{
    // BASE CASE: Stop when we reach 1 (or 0)
    if (n <= 1)
    {
        return n;
    }

    // RECURSIVE CASE: Smaller problem is (n-1)
    // Sum(n) = n + Sum(n-1)
    return n + Sum(n - 1);
}
```

**Trace of Sum(5):**

```
Sum(5) = 5 + Sum(4)
       = 5 + (4 + Sum(3))
       = 5 + (4 + (3 + Sum(2)))
       = 5 + (4 + (3 + (2 + Sum(1))))
       = 5 + (4 + (3 + (2 + 1)))
       = 5 + (4 + (3 + 3))
       = 5 + (4 + 6)
       = 5 + 10
       = 15 ✓
```

---

## Example: Factorial

**Problem:** Calculate n! = n × (n-1) × (n-2) × ... × 2 × 1

**Mathematical definition:**

```
n! = n × (n-1)!
1! = 1  (base case)
```

**Code:**

```csharp
/// <summary>
/// Calculates factorial of n recursively
/// Time: O(n), Space: O(n) for call stack
/// </summary>
public int Factorial(int n)
{
    // BASE CASE: 1! = 1 (or 0! = 1)
    if (n <= 1)
    {
        return 1;
    }

    // RECURSIVE CASE: n! = n × (n-1)!
    return n * Factorial(n - 1);
}
```

**Example:**

```
Factorial(5) = 5 × Factorial(4)
             = 5 × (4 × Factorial(3))
             = 5 × (4 × (3 × Factorial(2)))
             = 5 × (4 × (3 × (2 × Factorial(1))))
             = 5 × (4 × (3 × (2 × 1)))
             = 5 × (4 × (3 × 2))
             = 5 × (4 × 6)
             = 5 × 24
             = 120 ✓
```

---

## Example: Fibonacci Sequence

**Problem:** Generate Fibonacci numbers: 1, 1, 2, 3, 5, 8, 13, 21...

**Pattern:** Each number is the sum of the previous two numbers.

**Mathematical definition:**

```
Fib(n) = Fib(n-1) + Fib(n-2)
Fib(1) = 1  (base case)
Fib(2) = 1  (base case)
```

**Code (naive version):**

```csharp
/// <summary>
/// Calculates nth Fibonacci number
/// Time: O(2^n) - VERY SLOW! Space: O(n)
/// </summary>
public int Fibonacci(int n)
{
    // BASE CASES: First two Fibonacci numbers
    if (n <= 2)
    {
        return 1;
    }

    // RECURSIVE CASE: Sum of previous two
    return Fibonacci(n - 1) + Fibonacci(n - 2);
}
```

**Problem with this approach:**

```
Fibonacci(6) calls:
                    Fib(6)
                   /      \
              Fib(5)      Fib(4)
             /     \      /     \
        Fib(4)   Fib(3) Fib(3) Fib(2)
        /   \    /   \  /   \
    Fib(3) Fib(2) ...

Total calls: 15 function calls!
Notice: Fib(4) calculated twice, Fib(3) calculated 3 times!
```

**Time Complexity:** O(2^n) - doubles with each increase in n! 😱

---

## Memoization: Making Recursion Fast

**Memoization** = Remembering previous results to avoid recalculating them.

Think of it like doing homework:

-   **Without memoization:** Redo problem #5 every time you see it
-   **With memoization:** Write down answer to #5, look it up next time!

**Optimized Fibonacci with memoization:**

```csharp
/// <summary>
/// Calculates nth Fibonacci number using memoization
/// Time: O(n) - MUCH FASTER! Space: O(n)
/// </summary>
public long Fibonacci(int n, Dictionary<int, long>? remember = null)
{
    // Step 1: Create dictionary on first call
    if (remember == null)
    {
        remember = new Dictionary<int, long>();
    }

    // Step 2: Base cases
    if (n <= 2)
    {
        return 1;
    }

    // Step 3: Check if we already calculated this!
    if (remember.ContainsKey(n))
    {
        return remember[n];  // Return saved result
    }

    // Step 4: Calculate using recursion
    long result = Fibonacci(n - 1, remember) + Fibonacci(n - 2, remember);

    // Step 5: Save result for future use
    remember[n] = result;

    return result;
}
```

**Performance comparison:**

```
Without memoization:
Fib(10) → 177 function calls
Fib(20) → 21,891 function calls
Fib(40) → 331,160,281 function calls! 💥

With memoization:
Fib(10) → 17 function calls ✓
Fib(20) → 37 function calls ✓
Fib(40) → 77 function calls ✓
Fib(90) → Works instantly! ✓
```

---

## Example: Binary Search (Recursive)

**Problem:** Find a value in a sorted array efficiently.

**Approach:**

1. Check the middle element
2. If match, we're done!
3. If target is smaller, search left half
4. If target is larger, search right half

```csharp
/// <summary>
/// Binary search using recursion
/// Time: O(log n), Space: O(log n) for call stack
/// </summary>
public bool BinarySearch(int[] sortedArray, int target)
{
    // BASE CASE 1: Empty array
    if (sortedArray.Length == 0)
    {
        return false;
    }

    // BASE CASE 2: Single element
    if (sortedArray.Length == 1)
    {
        return target == sortedArray[0];
    }

    // Find middle index
    int middle = sortedArray.Length / 2;

    // BASE CASE 3: Found it!
    if (target == sortedArray[middle])
    {
        return true;
    }

    // RECURSIVE CASE 1: Search left half
    if (target < sortedArray[middle])
    {
        return BinarySearch(sortedArray[..middle], target);
    }

    // RECURSIVE CASE 2: Search right half
    else
    {
        return BinarySearch(sortedArray[middle..], target);
    }
}
```

**Example trace:**

```
Search for 89 in [1, 3, 6, 18, 20, 25, 34, 38, 89, 95, 99, 100]

Step 1: Middle = 25, target 89 > 25 → search right half
        [34, 38, 89, 95, 99, 100]

Step 2: Middle = 89, target 89 == 89 → FOUND! ✓
```

**Why it's fast:** Each comparison eliminates **half** the remaining elements!

---

## Example: String Permutations

**Problem:** Generate all ways to rearrange letters in a word.

**Approach:**

1. Pick each letter as the first letter
2. Recursively find permutations of remaining letters
3. Combine first letter with each permutation

```csharp
/// <summary>
/// Generate all permutations of letters in a string
/// Time: O(n!), Space: O(n) for call stack
/// </summary>
public void Permutations(string letters, string word = "")
{
    // BASE CASE: No more letters to add
    if (letters.Length == 0)
    {
        Console.WriteLine(word);  // Print the completed permutation
        return;
    }

    // RECURSIVE CASE: Try each letter as the next one
    for (int i = 0; i < letters.Length; i++)
    {
        // Remove the letter we're using from available letters
        string lettersLeft = letters.Remove(i, 1);

        // Add this letter to our word and recurse
        Permutations(lettersLeft, word + letters[i]);
    }
}
```

**Example: Permutations("ABC")**

```
Start with ""
├─ Pick 'A' → Permutations("BC", "A")
│  ├─ Pick 'B' → Permutations("C", "AB")
│  │  └─ Pick 'C' → "ABC" ✓
│  └─ Pick 'C' → Permutations("B", "AC")
│     └─ Pick 'B' → "ACB" ✓
├─ Pick 'B' → Permutations("AC", "B")
│  ├─ Pick 'A' → Permutations("C", "BA")
│  │  └─ Pick 'C' → "BAC" ✓
│  └─ Pick 'C' → Permutations("A", "BC")
│     └─ Pick 'A' → "BCA" ✓
└─ Pick 'C' → Permutations("AB", "C")
   ├─ Pick 'A' → Permutations("B", "CA")
   │  └─ Pick 'B' → "CAB" ✓
   └─ Pick 'B' → Permutations("A", "CB")
      └─ Pick 'A' → "CBA" ✓

Results: ABC, ACB, BAC, BCA, CAB, CBA
```

---

## Recursion vs Iteration

| Aspect          | Recursion                                | Iteration (Loops)       |
| --------------- | ---------------------------------------- | ----------------------- |
| **Readability** | Often cleaner for complex problems       | Can be verbose          |
| **Memory**      | Uses call stack (O(n) space)             | Uses O(1) space         |
| **Performance** | Slower (function call overhead)          | Faster                  |
| **Best for**    | Tree/graph traversal, divide-and-conquer | Simple repetitive tasks |
| **Risk**        | Stack overflow if too deep               | No stack risk           |

**When to use recursion:**

-   ✅ Working with trees or graphs
-   ✅ Divide-and-conquer algorithms (binary search, merge sort)
-   ✅ Backtracking problems (maze solving, N-Queens)
-   ✅ Problem naturally defined recursively (factorial, Fibonacci)

**When to use iteration:**

-   ✅ Simple loops (summing array, counting)
-   ✅ Performance critical code
-   ✅ Very deep recursion (would cause stack overflow)

---

## Common Recursion Patterns

### **Pattern 1: Accumulator**

Build up a result as you recurse:

```csharp
public int SumArray(int[] arr, int index = 0)
{
    if (index >= arr.Length)
        return 0;  // Base case

    return arr[index] + SumArray(arr, index + 1);
}
```

### **Pattern 2: Divide and Conquer**

Split problem in half:

```csharp
public int FindMax(int[] arr)
{
    if (arr.Length == 1)
        return arr[0];  // Base case

    int mid = arr.Length / 2;
    int leftMax = FindMax(arr[..mid]);
    int rightMax = FindMax(arr[mid..]);

    return Math.Max(leftMax, rightMax);
}
```

### **Pattern 3: Multiple Recursion**

Call function multiple times:

```csharp
public int Fibonacci(int n)
{
    if (n <= 2)
        return 1;  // Base case

    // Two recursive calls!
    return Fibonacci(n - 1) + Fibonacci(n - 2);
}
```

### **Pattern 4: Backtracking**

Try options, backtrack if they don't work:

```csharp
public bool SolveMaze(int x, int y)
{
    if (IsGoal(x, y))
        return true;  // Base case: found solution!

    if (!IsValid(x, y))
        return false;  // Base case: invalid path

    // Try all directions
    if (SolveMaze(x + 1, y)) return true;  // Right
    if (SolveMaze(x, y + 1)) return true;  // Down
    if (SolveMaze(x - 1, y)) return true;  // Left
    if (SolveMaze(x, y - 1)) return true;  // Up

    return false;  // No solution found
}
```

---

## Performance Analysis

### **Time Complexity:**

| Recursion Type               | Time Complexity | Example             |
| ---------------------------- | --------------- | ------------------- |
| Linear recursion             | O(n)            | Sum, Factorial      |
| Binary recursion (no memo)   | O(2^n)          | Naive Fibonacci     |
| Binary recursion (with memo) | O(n)            | Optimized Fibonacci |
| Divide & conquer             | O(log n)        | Binary Search       |
| Permutations                 | O(n!)           | String permutations |

### **Space Complexity:**

All recursive functions use **O(depth)** space for the call stack:

-   Factorial(n) → O(n) space
-   BinarySearch → O(log n) space
-   Fibonacci → O(n) space

**Stack Overflow Risk:**

-   Most systems limit call stack to ~1000-10000 calls
-   Deep recursion can crash your program!
-   Use iteration or tail recursion optimization if possible

---

## Debugging Tips

### **Tip 1: Add Print Statements**

See what's happening at each level:

```csharp
public int Factorial(int n, int depth = 0)
{
    string indent = new string(' ', depth * 2);
    Console.WriteLine($"{indent}Factorial({n}) called");

    if (n <= 1)
    {
        Console.WriteLine($"{indent}Returning 1");
        return 1;
    }

    int result = n * Factorial(n - 1, depth + 1);
    Console.WriteLine($"{indent}Returning {n} * {result / n} = {result}");
    return result;
}
```

### **Tip 2: Check Base Cases First**

Always verify your base cases work correctly before testing recursion.

### **Tip 3: Trace by Hand**

For small inputs, manually trace the recursion on paper to understand the flow.

---

## Common Interview Questions & Answers

### Question 1: Reverse a String

**Problem:** Reverse a string using recursion.

**Approach:**

-   Base case: Empty or single character string returns itself
-   Recursive case: Take first character, put it at the end after reversing the rest

```csharp
/// <summary>
/// Reverses a string using recursion
/// Time: O(n), Space: O(n) for call stack + new strings
/// </summary>
public string ReverseString(string str)
{
    // BASE CASE: Empty or single character
    if (str.Length <= 1)
    {
        return str;
    }

    // RECURSIVE CASE:
    // Take first character and put it after the reversed rest
    return ReverseString(str[1..]) + str[0];
}
```

**Example:**

```
ReverseString("HELLO")
= ReverseString("ELLO") + 'H'
= (ReverseString("LLO") + 'E') + 'H'
= ((ReverseString("LO") + 'L') + 'E') + 'H'
= (((ReverseString("O") + 'L') + 'L') + 'E') + 'H'
= ((("O" + 'L') + 'L') + 'E') + 'H'
= "OLLEH" ✓
```

**Performance:** O(n) time, O(n) space

---

### Question 2: Check if String is Palindrome

**Problem:** Determine if a string reads the same forwards and backwards.

**Approach:**

-   Base case: Empty string or single character is always a palindrome
-   Recursive case: Check if first and last characters match, then check the middle

```csharp
/// <summary>
/// Checks if string is palindrome using recursion
/// Time: O(n), Space: O(n) for call stack
/// </summary>
public bool IsPalindrome(string str)
{
    // BASE CASE: 0 or 1 character is always palindrome
    if (str.Length <= 1)
    {
        return true;
    }

    // RECURSIVE CASE: Check first and last characters
    if (str[0] != str[^1])  // ^1 means last character
    {
        return false;  // Mismatch, not a palindrome
    }

    // First and last match, check the middle part
    return IsPalindrome(str[1..^1]);  // Remove first and last
}
```

**Example:**

```
IsPalindrome("RACECAR")
= 'R' == 'R' ✓ AND IsPalindrome("ACECA")
= 'A' == 'A' ✓ AND IsPalindrome("CEC")
= 'C' == 'C' ✓ AND IsPalindrome("E")
= true ✓

IsPalindrome("HELLO")
= 'H' == 'O' ✗
= false ✗
```

**Performance:** O(n) time, O(n) space

---

### Question 3: Sum of Digits

**Problem:** Find the sum of all digits in a number (e.g., 1234 → 1+2+3+4 = 10).

**Approach:**

-   Base case: Single digit number (< 10) returns itself
-   Recursive case: Add last digit to sum of remaining digits

```csharp
/// <summary>
/// Calculates sum of digits recursively
/// Time: O(log n) where n is the number (digits = log10(n))
/// Space: O(log n) for call stack
/// </summary>
public int SumOfDigits(int n)
{
    // BASE CASE: Single digit
    if (n < 10)
    {
        return n;
    }

    // RECURSIVE CASE: Last digit + sum of remaining digits
    // n % 10 gets last digit
    // n / 10 removes last digit
    return (n % 10) + SumOfDigits(n / 10);
}
```

**Example:**

```
SumOfDigits(1234)
= 4 + SumOfDigits(123)
= 4 + (3 + SumOfDigits(12))
= 4 + (3 + (2 + SumOfDigits(1)))
= 4 + (3 + (2 + 1))
= 4 + (3 + 3)
= 4 + 6
= 10 ✓
```

**Performance:** O(d) where d is number of digits

---

### Question 4: Power Function

**Problem:** Calculate x^n (x raised to power n) using recursion.

**Approach:**

-   Base case: x^0 = 1
-   Recursive case: x^n = x × x^(n-1)
-   Optimization: Use divide and conquer for O(log n)

```csharp
/// <summary>
/// Calculates x^n using recursion (optimized version)
/// Time: O(log n), Space: O(log n)
/// </summary>
public double Power(double x, int n)
{
    // BASE CASE: Anything to power 0 is 1
    if (n == 0)
    {
        return 1;
    }

    // Handle negative exponents
    if (n < 0)
    {
        return 1.0 / Power(x, -n);
    }

    // OPTIMIZATION: Divide and conquer
    // x^8 = (x^4)^2 instead of x*x*x*x*x*x*x*x
    if (n % 2 == 0)
    {
        // Even exponent: x^n = (x^(n/2))^2
        double half = Power(x, n / 2);
        return half * half;
    }
    else
    {
        // Odd exponent: x^n = x * x^(n-1)
        return x * Power(x, n - 1);
    }
}
```

**Example (optimized):**

```
Power(2, 8)
= Power(2, 4)^2
= (Power(2, 2)^2)^2
= ((Power(2, 1)^2)^2)^2
= (((2 * Power(2, 0))^2)^2)^2
= (((2 * 1)^2)^2)^2
= ((4)^2)^2
= (16)^2
= 256 ✓

Only 4 multiplications instead of 8!
```

**Performance:** O(log n) time with optimization

---

### Question 5: Count Occurrences in Array

**Problem:** Count how many times a value appears in an array using recursion.

**Approach:**

-   Base case: Empty array has 0 occurrences
-   Recursive case: Check first element, then count in rest of array

```csharp
/// <summary>
/// Counts occurrences of target in array recursively
/// Time: O(n), Space: O(n) for call stack
/// </summary>
public int CountOccurrences(int[] arr, int target, int index = 0)
{
    // BASE CASE: Reached end of array
    if (index >= arr.Length)
    {
        return 0;
    }

    // RECURSIVE CASE: Check current element + count in rest
    int currentCount = (arr[index] == target) ? 1 : 0;
    return currentCount + CountOccurrences(arr, target, index + 1);
}
```

**Example:**

```
CountOccurrences([3, 5, 3, 7, 3], 3, 0)
= 1 + CountOccurrences(..., 1)  // arr[0] = 3 ✓
= 1 + (0 + CountOccurrences(..., 2))  // arr[1] = 5 ✗
= 1 + (0 + (1 + CountOccurrences(..., 3)))  // arr[2] = 3 ✓
= 1 + (0 + (1 + (0 + CountOccurrences(..., 4))))  // arr[3] = 7 ✗
= 1 + (0 + (1 + (0 + (1 + CountOccurrences(..., 5)))))  // arr[4] = 3 ✓
= 1 + (0 + (1 + (0 + (1 + 0))))  // End of array
= 3 ✓
```

**Performance:** O(n) time, O(n) space

---

### Question 6: Generate All Subsets (Power Set)

**Problem:** Generate all possible subsets of a set (including empty set).

**Approach:**

-   Base case: Empty set has only one subset (itself)
-   Recursive case: For each element, generate subsets with and without it

```csharp
/// <summary>
/// Generates all subsets of an array
/// Time: O(2^n), Space: O(n) for call stack
/// Total subsets: 2^n
/// </summary>
public List<List<int>> GenerateSubsets(int[] arr, int index = 0)
{
    // BASE CASE: No more elements, return empty subset
    if (index >= arr.Length)
    {
        return new List<List<int>> { new List<int>() };
    }

    // RECURSIVE CASE: Get all subsets of remaining elements
    var subsetsWithoutCurrent = GenerateSubsets(arr, index + 1);

    // Create new subsets by adding current element to each
    var subsetsWithCurrent = new List<List<int>>();
    foreach (var subset in subsetsWithoutCurrent)
    {
        var newSubset = new List<int>(subset);
        newSubset.Insert(0, arr[index]);
        subsetsWithCurrent.Add(newSubset);
    }

    // Combine both: subsets without current + subsets with current
    var allSubsets = new List<List<int>>();
    allSubsets.AddRange(subsetsWithoutCurrent);
    allSubsets.AddRange(subsetsWithCurrent);

    return allSubsets;
}
```

**Example:**

```
GenerateSubsets([1, 2, 3])

Start with [1, 2, 3]
├─ Subsets without 1: GenerateSubsets([2, 3])
│  ├─ Subsets without 2: GenerateSubsets([3])
│  │  ├─ Without 3: [ ]
│  │  └─ With 3: [3]
│  └─ With 2: [2], [2,3]
└─ With 1: [1], [1,3], [1,2], [1,2,3]

Results: [], [3], [2], [2,3], [1], [1,3], [1,2], [1,2,3]
Total: 2^3 = 8 subsets ✓
```

**Performance:** O(2^n) time - exponential!

---

### Question 7: Tower of Hanoi

**Problem:** Move n disks from source rod to destination rod using an auxiliary rod. Rules:

-   Only one disk can be moved at a time
-   A disk can only be placed on top of a larger disk

**Approach:**

-   Base case: Moving 1 disk is trivial (just move it)
-   Recursive case: To move n disks:
    1. Move n-1 disks to auxiliary rod
    2. Move largest disk to destination
    3. Move n-1 disks from auxiliary to destination

```csharp
/// <summary>
/// Solves Tower of Hanoi puzzle
/// Time: O(2^n), Space: O(n) for call stack
/// Total moves: 2^n - 1
/// </summary>
public void TowerOfHanoi(int n, char source, char destination, char auxiliary)
{
    // BASE CASE: Only 1 disk to move
    if (n == 1)
    {
        Console.WriteLine($"Move disk 1 from {source} to {destination}");
        return;
    }

    // RECURSIVE CASE: Move n disks

    // Step 1: Move n-1 disks from source to auxiliary (using destination)
    TowerOfHanoi(n - 1, source, auxiliary, destination);

    // Step 2: Move largest disk from source to destination
    Console.WriteLine($"Move disk {n} from {source} to {destination}");

    // Step 3: Move n-1 disks from auxiliary to destination (using source)
    TowerOfHanoi(n - 1, auxiliary, destination, source);
}
```

**Example:**

```
TowerOfHanoi(3, 'A', 'C', 'B')

Output:
Move disk 1 from A to C
Move disk 2 from A to B
Move disk 1 from C to B
Move disk 3 from A to C
Move disk 1 from B to A
Move disk 2 from B to C
Move disk 1 from A to C

Visual:
    |         |         |
   [1]        |         |
   [2]        |         |
   [3]        |         |
  =====     =====     =====
    A         B         C

→ (7 moves later) →

    |         |         |
    |         |        [1]
    |         |        [2]
    |         |        [3]
  =====     =====     =====
    A         B         C
```

**Performance:** O(2^n) time - requires 2^n - 1 moves

---

## Additional Practice Problems

Try implementing these yourself:

1. **GCD (Greatest Common Divisor)**: Find GCD of two numbers using Euclidean algorithm
2. **Flatten Nested List**: Convert [[1,2],[3,[4,5]],6] to [1,2,3,4,5,6]
3. **Count Paths in Grid**: Count ways to reach bottom-right from top-left (only move right/down)
4. **Word Break**: Check if string can be segmented into dictionary words
5. **Generate Parentheses**: Generate all valid combinations of n pairs of parentheses
6. **Subset Sum**: Find if any subset sums to target value
7. **Decode Ways**: Count ways to decode a digit string (1=A, 2=B, ..., 26=Z)

---

## Key Takeaways

✅ **Recursion = Function calling itself** on a smaller problem

✅ **Two rules:** Smaller problem + Base case (or infinite loop!)

✅ **Call stack:** Each recursive call uses memory (risk of stack overflow)

✅ **Memoization:** Remember results to avoid duplicate calculations

✅ **When to use:**

-   Tree/graph problems
-   Divide and conquer
-   Backtracking
-   Naturally recursive problems

✅ **Performance trade-off:**

-   Cleaner code, but slower and uses more memory
-   Can be optimized with memoization

✅ **Common pitfalls:**

-   Forgetting base case
-   Not making problem smaller
-   Too many recursive calls (exponential time)
-   Stack overflow from deep recursion

---

## Real-World Applications

**Where Recursion is Used:**

-   **File Systems**: Traversing directories and subdirectories
-   **Compilers**: Parsing nested expressions and syntax trees
-   **Graphics**: Rendering fractals and recursive patterns
-   **Games**: AI decision trees, pathfinding
-   **Web Crawlers**: Following links recursively
-   **JSON/XML Parsing**: Handling nested structures
-   **Sorting Algorithms**: Merge sort, quick sort
-   **Search Algorithms**: Binary search, depth-first search

**Why it's powerful:**

-   Elegant solutions to complex problems
-   Natural way to think about hierarchical data
-   Essential for many algorithms and data structures
