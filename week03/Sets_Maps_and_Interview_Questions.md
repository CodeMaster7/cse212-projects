# Sets and Maps - Complete Guide

## Table of Contents
1. [What is a Set?](#what-is-a-set)
2. [What is a Map/Dictionary?](#what-is-a-mapdictionary)
3. [When to Use Sets vs Maps](#when-to-use-sets-vs-maps)
4. [Performance Characteristics](#performance-characteristics)
5. [Articulating Answers to Technical Questions](#articulating-answers-to-technical-questions)
6. [Interview Questions](#interview-questions)

---

## What is a Set?

### Simple Explanation
Imagine you have a box where you collect unique stickers. If you try to put a duplicate sticker in the box, it just disappears - the box only keeps ONE of each sticker. That's a Set!

### Technical Definition
A **Set** is a data structure that stores a collection of **unique values**. It automatically prevents duplicates and provides extremely fast lookups.

### Key Properties
1. **No Duplicates** - Each value can only appear once
2. **Unordered** - Items don't have a specific position (no index like arrays)
3. **Fast Lookup** - Checking if something exists is O(1) - basically instant!
4. **Fast Insertion** - Adding new items is O(1)
5. **Fast Deletion** - Removing items is O(1)

### In C#
```csharp
// Creating a HashSet
var mySet = new HashSet<int>();

// Adding items - O(1)
mySet.Add(5);
mySet.Add(10);
mySet.Add(5);  // Won't be added - duplicate!

// Checking if item exists - O(1)
if (mySet.Contains(10)) {
    Console.WriteLine("Found it!");
}

// Result: Set contains {5, 10} - only unique values
```

### Real-World Use Cases
- **Tracking unique visitors** to a website
- **Checking for duplicate letters** in a word
- **Finding unique items** in a list
- **Membership checking** (is this user in the premium group?)

---

## What is a Map/Dictionary?

### Simple Explanation
Think of a real dictionary: you look up a **word** (key) and find its **definition** (value). A Map/Dictionary in programming works the same way - you have pairs of keys and values!

Example: A phone book maps **names** (keys) to **phone numbers** (values).

### Technical Definition
A **Map** (called `Dictionary` in C#) is a data structure that stores **key-value pairs**. Each unique key maps to exactly one value. You can quickly look up a value by its key.

### Key Properties
1. **Key-Value Pairs** - Each key is associated with one value
2. **Unique Keys** - Each key can only appear once (but values can repeat)
3. **Fast Lookup by Key** - Finding a value by key is O(1)
4. **Fast Insertion** - Adding new pairs is O(1)
5. **Fast Deletion** - Removing pairs is O(1)
6. **Fast Update** - Changing a value for a key is O(1)

### In C#
```csharp
// Creating a Dictionary (Map)
var phoneBook = new Dictionary<string, string>();

// Adding key-value pairs - O(1)
phoneBook["Alice"] = "555-1234";
phoneBook["Bob"] = "555-5678";

// Looking up by key - O(1)
string aliceNumber = phoneBook["Alice"]; // "555-1234"

// Checking if key exists - O(1)
if (phoneBook.ContainsKey("Alice")) {
    Console.WriteLine("Found Alice!");
}

// Updating a value - O(1)
phoneBook["Alice"] = "555-9999";
```

### Real-World Use Cases
- **Counting occurrences** (word → count)
- **Caching data** (ID → user object)
- **Translating** (English word → Spanish word)
- **Accumulating totals** (player ID → total points)
- **Configuration settings** (setting name → value)

---

## When to Use Sets vs Maps

### Use a SET when:
- ✅ You only care about **presence/absence** of items
- ✅ You need to **track unique items**
- ✅ You need to **check for duplicates**
- ✅ You need to **check membership** quickly

**Example Problems:**
- Are all letters in this string unique?
- Have I seen this user before?
- What are all the unique tags on this blog?

### Use a MAP/DICTIONARY when:
- ✅ You need to **associate two pieces of information** (key → value)
- ✅ You need to **count occurrences** of things
- ✅ You need to **accumulate values** for different keys
- ✅ You need to **look up information quickly** by a key

**Example Problems:**
- Count how many times each word appears
- What's the total score for each player?
- Translate words from English to Spanish
- Find pairs of numbers that sum to a target

---

## Performance Characteristics

### Big O Comparison

| Operation | Array | Set | Dictionary/Map |
|-----------|-------|-----|----------------|
| Search/Lookup | O(n) | O(1) | O(1) by key |
| Insert | O(1) end, O(n) middle | O(1) | O(1) |
| Delete | O(n) | O(1) | O(1) |
| Access by index | O(1) | ❌ Not supported | ❌ Not for keys |

### Why Are Sets and Maps So Fast?

They use **hashing** under the hood!

**Simple Explanation:** Imagine you have 10 buckets numbered 0-9. When you store a value, you use a "magic formula" (hash function) that instantly tells you which bucket to put it in. When you look for it later, you use the same formula to know exactly which bucket to check!

**Technical:** Hash tables use a hash function to compute an index into an array of buckets. This allows O(1) average-case lookup, insertion, and deletion.

---

## Articulating Answers to Technical Questions

When answering technical interview questions, use this framework:

### The STAR Method (Modified for Coding)

#### 1. **Understand the Problem** (30 seconds)
- Restate the problem in your own words
- Ask clarifying questions
- Identify inputs and expected outputs

**Example:**
> "So we need to find all pairs of numbers in an array that sum to 10, and we shouldn't print duplicate pairs. The input is an array of integers with no duplicates, and we should output each pair once."

#### 2. **Explain Your Approach** (1-2 minutes)
- Start with the simple/brute force solution
- Explain why it's not optimal
- Propose your optimized solution
- Explain the data structure choice

**Example:**
> "The brute force approach would be using nested loops to check every pair, which is O(n²). That's slow for large arrays.
>
> Instead, I'll use a HashSet to achieve O(n) time. As I iterate through the array once, for each number I'll calculate its complement (10 - number) and check if that complement already exists in my set. If yes, we found a pair! Then I add the current number to the set for future checks.
>
> I chose a HashSet because it gives me O(1) lookup time to check if the complement exists, and O(1) insertion to add numbers I've seen."

#### 3. **Discuss Time and Space Complexity** (30 seconds)
- State Big O for time
- State Big O for space
- Explain why

**Example:**
> "This solution is O(n) time because we loop through the array once, and each set operation is O(1). The space complexity is also O(n) because in the worst case, we might store all n numbers in the set if no pairs are found."

#### 4. **Consider Edge Cases** (30 seconds)
- Empty inputs
- Single element
- All duplicates
- Very large inputs

**Example:**
> "Edge cases to consider: if the array is empty, we just return nothing. If there's only one number, no pairs are possible. Also, we should consider negative numbers - the solution still works since we're just doing arithmetic."

#### 5. **Code It** (3-5 minutes)
- Write clean, readable code
- Add comments for clarity
- Use meaningful variable names

#### 6. **Test It** (1-2 minutes)
- Walk through with a simple example
- Check edge cases
- Verify output

**Example:**
> "Let's trace through [1, 2, 3, 4, 6, 7, 9]:
> - See 1: complement is 9, not in set yet, add 1
> - See 2: complement is 8, not in set yet, add 2
> - ...continue...
> - See 9: complement is 1, and 1 IS in the set! Print '9 1'
>
> Perfect, that works!"

### Key Phrases to Use

**When explaining your choice:**
- "I chose [data structure] because..."
- "This gives us O(1) lookup time, which is important because..."
- "The trade-off here is [space] for [time], which is worth it because..."

**When discussing complexity:**
- "The time complexity is O(n) because we iterate through the array once..."
- "Each operation inside the loop is O(1) because..."
- "The space complexity is O(n) in the worst case because..."

**When considering alternatives:**
- "Another approach would be [X], but that would be O(n²)..."
- "We could use [data structure], but that wouldn't give us the O(1) lookup we need..."

---

## Interview Questions

### Easy Questions

#### 1. Remove Duplicates
**Problem:** Given an array of integers, return a new array with all duplicates removed.

**Input:** `[1, 2, 3, 2, 4, 1, 5]`
**Output:** `[1, 2, 3, 4, 5]`

**Hints:**
- What data structure automatically handles uniqueness?
- How can you convert between a Set and an array?

---

#### 2. First Unique Character
**Problem:** Given a string, find the first character that appears only once. Return its index, or -1 if no such character exists.

**Input:** `"leetcode"`
**Output:** `0` (the 'l' at index 0 appears only once and is first)

**Hints:**
- You need to count how many times each character appears
- What data structure maps characters to counts?
- You'll need to pass through the string twice

---

#### 3. Contains Duplicate
**Problem:** Given an array of integers, return true if any value appears at least twice in the array.

**Input:** `[1, 2, 3, 1]`
**Output:** `true`

**Hints:**
- Sets automatically reject duplicates
- What happens if you try to add a duplicate to a set?
- Can you detect when Add() fails or when Contains() returns true?

---

### Medium Questions

#### 4. Two Sum
**Problem:** Given an array of integers and a target number, return indices of the two numbers that add up to the target. Assume exactly one solution exists.

**Input:** `nums = [2, 7, 11, 15], target = 9`
**Output:** `[0, 1]` (because nums[0] + nums[1] = 2 + 7 = 9)

**Hints:**
- Similar to the DisplaySumPairs problem, but you need to return indices
- Store the number AND its index
- What data structure can map values to their indices?

---

#### 5. Group Anagrams
**Problem:** Given an array of strings, group anagrams together.

**Input:** `["eat", "tea", "tan", "ate", "nat", "bat"]`
**Output:** `[["eat","tea","ate"], ["tan","nat"], ["bat"]]`

**Hints:**
- Anagrams have the same letters when sorted: "eat" → "aet", "tea" → "aet"
- Use the sorted string as a key
- Map the sorted string to a list of original strings
- What data structure maps keys to lists?

---

#### 6. Longest Substring Without Repeating Characters
**Problem:** Given a string, find the length of the longest substring without repeating characters.

**Input:** `"abcabcbb"`
**Output:** `3` (the substring is "abc")

**Hints:**
- Use a sliding window technique
- Keep a set of characters in the current window
- When you find a duplicate, shrink the window from the left

---

#### 7. Word Pattern
**Problem:** Given a pattern and a string, determine if the string follows the same pattern.

**Input:** `pattern = "abba", str = "dog cat cat dog"`
**Output:** `true`

**Input:** `pattern = "abba", str = "dog cat cat fish"`
**Output:** `false`

**Hints:**
- You need two maps: pattern character → word, and word → pattern character
- Both mappings must be consistent
- Check both directions to avoid cases like "ab" → "dog dog"

---

### Hard Questions

#### 8. Substring with Concatenation of All Words
**Problem:** Given a string and an array of words (all same length), find all starting indices of substrings that are concatenation of each word exactly once.

**Input:** `s = "barfoothefoobarman", words = ["foo", "bar"]`
**Output:** `[0, 9]` ("barfoo" starts at 0, "foobar" starts at 9)

**Hints:**
- Use a map to count how many times each word should appear
- Use a sliding window of size (word_length × num_words)
- Use another map to count words in the current window

---

#### 9. LRU Cache
**Problem:** Design and implement a Least Recently Used (LRU) cache with O(1) get and put operations.

**Operations:**
- `get(key)` - Get the value (will always be positive) of the key if exists, else return -1
- `put(key, value)` - Set or insert the value if key is not present. If cache is at capacity, remove the least recently used item first

**Hints:**
- Need a Dictionary for O(1) access by key
- Need a way to track order of use (most recent to least recent)
- Consider using a Dictionary + Doubly Linked List
- Or use Dictionary + track access time

---

#### 10. Design Twitter Feed
**Problem:** Design a simplified Twitter where users can post tweets, follow/unfollow users, and see the 10 most recent tweets in their feed.

**Operations:**
- `postTweet(userId, tweetId)` - Post a tweet
- `getNewsFeed(userId)` - Get 10 most recent tweets from user and people they follow
- `follow(followerId, followeeId)` - Follower follows followee
- `unfollow(followerId, followeeId)` - Follower unfollows followee

**Hints:**
- Map userId → Set of people they follow
- Map userId → List of their tweets (with timestamp)
- For feed, merge tweets from user + all followed users
- Sort by timestamp and take top 10

---

## Practice Strategy

### Week 1-2: Master the Basics
- Implement basic Set and Map operations
- Solve Easy problems
- Focus on understanding WHY you use each data structure

### Week 3-4: Build Complexity
- Solve Medium problems
- Practice explaining your solutions out loud
- Time yourself on each problem

### Week 5-6: Interview Ready
- Tackle Hard problems
- Do mock interviews with friends
- Focus on communication while coding

### Remember:
- **Communication > Perfect Code** - Explaining your thought process is more important than getting it perfect the first time
- **Ask Questions** - Clarifying requirements shows good judgment
- **Think Out Loud** - Let the interviewer follow your thinking
- **Test Your Code** - Walk through examples to catch bugs
- **Stay Calm** - If you get stuck, explain what you're thinking and ask for a hint

---

## Additional Resources

### C# Documentation
- [HashSet<T> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)
- [Dictionary<TKey,TValue> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)

### Practice Platforms
- LeetCode (filter by "Hash Table" tag)
- HackerRank (Hash Maps section)
- CodeSignal
- AlgoExpert

### Key Takeaways
1. **Sets** store unique values with O(1) lookup
2. **Maps/Dictionaries** store key-value pairs with O(1) lookup by key
3. Use Sets for membership checking and uniqueness
4. Use Maps for counting, accumulating, and associating data
5. Both provide massive performance improvements over nested loops
6. Communication is key in interviews - explain your thinking!

---

**Good luck with your interviews! 🚀**
