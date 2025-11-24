# Set Intersection and Union - Implementation Guide

## Scenario
An **intersection** of two sets contains items that are in **both** of the two sets.

A **union** of two sets contains **all items** that are in **either** set.

---

## Question 1: Set Intersection

### Overall Approach
To find the intersection of two sets without using the built-in intersection method, I would:
1. Create an empty result set to store items that appear in both sets
2. Loop through every item in the first set
3. For each item, check if it also exists in the second set
4. If the item exists in both sets, add it to the result set
5. Return the result set

### Step-by-Step Function Behavior

```csharp
public static HashSet<T> Intersection<T>(HashSet<T> set1, HashSet<T> set2)
{
    // Step 1: Create an empty result set
    var result = new HashSet<T>();

    // Step 2: Loop through each item in set1
    foreach (var item in set1)
    {
        // Step 3: Check if this item also exists in set2
        // HashSet.Contains() is O(1) - very fast!
        if (set2.Contains(item))
        {
            // Step 4: If found in both, add to result
            result.Add(item);
        }
    }

    // Step 5: Return all items that were in both sets
    return result;
}
```

**How it behaves:**
- If `set1 = {1, 2, 3, 4}` and `set2 = {3, 4, 5, 6}`
  - Loop through set1: check 1 (not in set2), check 2 (not in set2), check 3 (IN set2! add to result), check 4 (IN set2! add to result)
  - Result: `{3, 4}` ✓

### Big O Performance
**Time Complexity: O(n)** where n is the size of the first set
- We loop through set1 once: O(n)
- Each `Contains()` check on a HashSet is O(1)
- Total: O(n × 1) = O(n)

**Space Complexity: O(k)** where k is the number of items in the intersection
- We only store items that appear in both sets

**Optimization Note:** To make it even faster, we could loop through the *smaller* set and check against the *larger* set. This would be O(min(n, m)) where n and m are the sizes of the two sets.

### Test Cases

#### Test Case 1: Normal Case with Some Overlap
```csharp
set1 = {1, 2, 3, 4, 5}
set2 = {4, 5, 6, 7, 8}
Expected Result = {4, 5}
```
**Why:** Tests the basic functionality - some items in common, some not.

#### Test Case 2: No Overlap (Disjoint Sets)
```csharp
set1 = {"apple", "banana", "cherry"}
set2 = {"dog", "cat", "bird"}
Expected Result = {} (empty set)
```
**Why:** Tests edge case where sets have nothing in common. Should return empty set, not crash.

#### Test Case 3: One Set is Completely Inside the Other
```csharp
set1 = {1, 2, 3}
set2 = {1, 2, 3, 4, 5, 6}
Expected Result = {1, 2, 3}
```
**Why:** Tests when all items of one set are in the other (subset scenario).

#### Test Case 4: Empty Set
```csharp
set1 = {}
set2 = {1, 2, 3}
Expected Result = {} (empty set)
```
**Why:** Tests edge case with empty input. Intersection with nothing should be nothing.

#### Test Case 5: Identical Sets
```csharp
set1 = {"x", "y", "z"}
set2 = {"x", "y", "z"}
Expected Result = {"x", "y", "z"}
```
**Why:** When sets are exactly the same, intersection should be the entire set.

---

## Question 2: Set Union

### Overall Approach
To find the union of two sets without using the built-in union method, I would:
1. Create a result set and initialize it with all items from the first set
2. Loop through every item in the second set
3. Add each item from the second set to the result set
4. Since sets automatically prevent duplicates, items that exist in both sets will only appear once
5. Return the result set

### Step-by-Step Function Behavior

```csharp
public static HashSet<T> Union<T>(HashSet<T> set1, HashSet<T> set2)
{
    // Step 1: Create result set with all items from set1
    // This copies all items from set1 into result
    var result = new HashSet<T>(set1);

    // Step 2: Loop through each item in set2
    foreach (var item in set2)
    {
        // Step 3: Add item to result
        // If item already exists (was in set1), HashSet ignores the duplicate
        // If item is new, it gets added
        result.Add(item);
    }

    // Step 4: Return the combined set with no duplicates
    return result;
}
```

**How it behaves:**
- If `set1 = {1, 2, 3}` and `set2 = {3, 4, 5}`
  - Start with result = {1, 2, 3}
  - Add 3 from set2 (already exists, no change)
  - Add 4 from set2 (new, added) → result = {1, 2, 3, 4}
  - Add 5 from set2 (new, added) → result = {1, 2, 3, 4, 5}
  - Final result: `{1, 2, 3, 4, 5}` ✓

### Big O Performance
**Time Complexity: O(n + m)** where n is size of set1 and m is size of set2
- Copying set1 into result: O(n)
- Looping through set2 and adding each item: O(m)
- Each `Add()` operation on HashSet is O(1)
- Total: O(n + m)

**Space Complexity: O(n + m)** in the worst case
- If sets have no overlap, result contains all items from both sets
- If sets are identical, result is O(n) = O(m)

### Test Cases

#### Test Case 1: Normal Case with Some Overlap
```csharp
set1 = {1, 2, 3}
set2 = {3, 4, 5}
Expected Result = {1, 2, 3, 4, 5}
```
**Why:** Tests basic functionality where sets share some elements. The number 3 should only appear once.

#### Test Case 2: No Overlap (Disjoint Sets)
```csharp
set1 = {"red", "blue"}
set2 = {"green", "yellow"}
Expected Result = {"red", "blue", "green", "yellow"}
```
**Why:** Tests when sets have nothing in common. All items from both sets should be included.

#### Test Case 3: Identical Sets
```csharp
set1 = {10, 20, 30}
set2 = {10, 20, 30}
Expected Result = {10, 20, 30}
```
**Why:** When sets are the same, union should be the same set (no duplicates).

#### Test Case 4: One Empty Set
```csharp
set1 = {1, 2, 3}
set2 = {}
Expected Result = {1, 2, 3}
```
**Why:** Union with an empty set should just be the non-empty set.

#### Test Case 5: One Set is Subset of Another
```csharp
set1 = {1, 2, 3, 4, 5, 6}
set2 = {2, 4}
Expected Result = {1, 2, 3, 4, 5, 6}
```
**Why:** When one set is completely contained in another, union is just the larger set.

---

## Key Concepts Summary

### Why HashSets are Perfect for These Operations:
1. **No Duplicates** - Automatically enforced
2. **Fast Lookups** - O(1) time to check if an item exists
3. **Fast Insertions** - O(1) time to add an item

### Visual Examples:

**Intersection (AND logic):**
```
Set A: [1] [2] [3] [4]
Set B:         [3] [4] [5] [6]
Result:        [3] [4]           (only items in BOTH)
```

**Union (OR logic):**
```
Set A: [1] [2] [3] [4]
Set B:         [3] [4] [5] [6]
Result: [1] [2] [3] [4] [5] [6] (all items from EITHER)
```

### Real-World Applications:

**Intersection:**
- Finding mutual friends on social media
- Finding products that match ALL filter criteria
- Finding students enrolled in BOTH courses

**Union:**
- Combining user preferences from different sources
- Merging contact lists without duplicates
- Finding products that match ANY filter criteria

---

## Complete Implementation Example

```csharp
public static class SetOperations
{
    /// <summary>
    /// Returns items that exist in BOTH sets
    /// Time: O(n), Space: O(k) where k is intersection size
    /// </summary>
    public static HashSet<T> Intersection<T>(HashSet<T> set1, HashSet<T> set2)
    {
        var result = new HashSet<T>();

        // Optimization: loop through smaller set
        var smallerSet = set1.Count <= set2.Count ? set1 : set2;
        var largerSet = set1.Count > set2.Count ? set1 : set2;

        foreach (var item in smallerSet)
        {
            if (largerSet.Contains(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    /// <summary>
    /// Returns all items from BOTH sets (no duplicates)
    /// Time: O(n + m), Space: O(n + m)
    /// </summary>
    public static HashSet<T> Union<T>(HashSet<T> set1, HashSet<T> set2)
    {
        // Start with all items from set1
        var result = new HashSet<T>(set1);

        // Add all items from set2 (duplicates automatically ignored)
        foreach (var item in set2)
        {
            result.Add(item);
        }

        return result;
    }
}
```

---

## Practice Problems

Try implementing these yourself:

1. **Set Difference**: Items in set1 but NOT in set2
2. **Symmetric Difference**: Items in either set but NOT in both (XOR logic)
3. **Is Subset**: Check if all items in set1 are also in set2
4. **Is Superset**: Check if set1 contains all items from set2
