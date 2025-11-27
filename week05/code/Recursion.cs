using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution
    /// in terms of recursive call on a smaller problem and
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    ///
    /// TIME COMPLEXITY: O(n) - We make n recursive calls
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1

        // BASE CASE: If n is 0 or negative, return 0
        // This stops the recursion
        if (n <= 0)
        {
            return 0;
        }

        // RECURSIVE CASE:
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1
    /// and the length of the letters list).
    ///
    /// TIME COMPLEXITY: O(n!/(n-k)!) where n=letters.Length, k=size
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2

        // BASE CASE: We've built a word of the desired size
        // Add it to results and stop recursing
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // RECURSIVE CASE: Try adding each available letter to the word
        // step 1: For each letter in 'letters':
        for (int i = 0; i < letters.Length; i++)
        {
            // step 2: Remove the current letter from available letters
            // (all letters except index i)
            string remainingLetters = letters.Remove(i, 1);
            // step 3: Add current letter to word and recurse
            PermutationsChoose(results, remainingLetters, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.
    /// We want to count how many ways there are to climb
    /// the stairs.  If the person could only climb one
    /// stair at a time, then the total would be just one.
    /// However, if the person could choose to climb either
    /// one, two, or three stairs at a time (in any order),
    /// then the total possibilities become much more
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step,
    /// - take a double step from the third to last step,
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) +
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to
    /// the function for you to complete this task.
    ///
    /// TIME COMPLEXITY: O(n) with memoization (without would be O(3^n)!)
    /// SPACE COMPLEXITY: O(n) for dictionary + call stack
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // TODO Start Problem 3

        // Step 1: Initialize memoization dictionary on first call
        // This dictionary will store previously calculated results
        // Key = number of stairs, Value = number of ways to climb
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base Cases - these are the simplest problems we can solve directly
        if (s == 0)
            return 0;  // No stairs = no ways
        if (s == 1)
            return 1;  // 1 stair = 1 way (take 1 step)
        if (s == 2)
            return 2;  // 2 stairs = 2 ways (1+1 or 2)
        if (s == 3)
            return 4;  // 3 stairs = 4 ways (1+1+1, 1+2, 2+1, 3)

        // Step 2: Check if we already calculated this value (MEMOIZATION)
        // This is the KEY to making this fast
        // Without this, we'd recalculate the same values thousands of times
        if (remember.ContainsKey(s))
        {
            return remember[s];  // Return cached result
        }

        // Step 3: Solve using recursion with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Step 4: Save result in dictionary before returning (MEMOIZATION)
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example,
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    ///
    /// TIME COMPLEXITY: O(2^w) where w is the number of wildcards (each wildcard doubles possibilities)
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4

        // BASE CASE: No more wildcards (*) in the pattern
        // step 1: Find the index of the first wildcard
        int wildcardIndex = pattern.IndexOf('*');
        // step 2: If there are no wildcards, add the pattern to results and return
        if (wildcardIndex == -1)
        {
            // step 3: Add the pattern to results
            results.Add(pattern);
            // step 4: Return
            return;
        }

        // RECURSIVE CASE: Found a wildcard, replace it with both 0 and 1

        // step 1: Replace the wildcard with 0 and recurse
        string patternWith0 = pattern[..wildcardIndex] + "0" + pattern[(wildcardIndex + 1)..];
        // step 2: Recurse with the pattern with 0
        WildcardBinary(patternWith0, results);

        // step 3: Replace the wildcard with 1 and recurse
        string patternWith1 = pattern[..wildcardIndex] + "1" + pattern[(wildcardIndex + 1)..];
        // step 4: Recurse with the pattern with 1
        WildcardBinary(patternWith1, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    ///
    /// TIME COMPLEXITY: O(4^n) worst case where n is number of open squares (each square can try 4 directions)
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }

        // currPath.Add((1,2)); // Use this syntax to add to the current path

        // TODO Start Problem 5

        // Step 1: Check if current position is valid
        // IsValidMove checks: not outside boundary, not a wall (0), not already visited
        if (!maze.IsValidMove(currPath, x, y))
        {
            return;  // Invalid move, backtrack
        }

        // Step 2: Add current position to the path
        // We're committing to this position for now (backtracking pattern)
        currPath.Add((x, y));

        // Step 3: BASE CASE - Check if we reached the end
        if (maze.IsEnd(x, y))
        {
            // Found a complete path Add it to results
            // Make a copy of currPath because we'll modify it during backtracking
            results.Add(currPath.AsString());

            // Remove current position and return (backtrack to try other paths)
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Step 4: RECURSIVE CASE - Try all four directions (right, down, left, up)
        // This is the "explore all possibilities" approach
        //
        // Maze values: 0=Wall, 1=Path, 2=End
        // We can move right, down, left, or up from current position
        // Each direction that is valid will be explored recursively

        // Try moving RIGHT (x+1, y)
        SolveMaze(results, maze, x + 1, y, currPath);

        // Try moving DOWN (x, y+1)
        SolveMaze(results, maze, x, y + 1, currPath);

        // Try moving LEFT (x-1, y)
        SolveMaze(results, maze, x - 1, y, currPath);

        // Try moving UP (x, y-1)
        SolveMaze(results, maze, x, y - 1, currPath);

        // Step 5: BACKTRACK - Remove current position from path
        // After trying all directions from this square, remove it
        currPath.RemoveAt(currPath.Count - 1);

        // This is the BACKTRACKING pattern:
        // 1. Make a choice (add position to path)
        // 2. Explore consequences (try all directions)
        // 3. Undo the choice (remove position from path)
    }
}