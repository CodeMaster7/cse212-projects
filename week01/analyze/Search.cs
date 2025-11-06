using System.Diagnostics;

public static class Search {
    public static void Run() {
        Console.WriteLine("{0,15}{1,15}{2,15}{3,15}{4,15}", "n", "sort1-count", "sort2-count", "sort1-time",
            "sort2-time");
        Console.WriteLine("{0,15}{0,15}{0,15}{0,15}{0,15}", "----------");

        for (int n = 0; n <= 25000; n += 1000) {
            var testData = Enumerable.Range(0, n).ToArray();
            int count1 = SearchSorted1(testData, n);
            int count2 = SearchSorted2(testData, n, 0, testData.Length - 1);
            double time1 = Time(() => SearchSorted1(testData, n), 100);
            double time2 = Time(() => SearchSorted2(testData, n, 0, testData.Length - 1), 100);
            Console.WriteLine("{0,15}{1,15}{2,15}{3,15:0.00000}{4,15:0.00000}", n, count1, count2, time1, time2);
        }
    }

    private static double Time(Action executeAlgorithm, int times) {
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < times; ++i) {
            executeAlgorithm();
        }

        sw.Stop();
        return sw.Elapsed.TotalMilliseconds / times;
    }

    /// <summary>
    /// Search for 'target' in the list 'data'. When its found (or not found) the variable count which represents
    /// the work done in the function is returned.
    /// </summary>
    /// <param name="data">The array of numbers</param>
    /// <param name="target">The number we're looking for</param>
    ///
    /// BIG O NOTATION: O(n) - Linear Time
    /// Why:
    /// - Single loop: O(n) — The foreach loop iterates through elements sequentially
    /// - Worst case: O(n) — In the worst case, we check all n elements before finding the target or determining it's not present
    /// - Best case: O(1) — If target is the first element, we find it immediately
    /// - Average case: O(n) — On average, we check about n/2 elements
    /// Total: O(n) — Linear time because we may need to check every element
    ///
    /// Note: This is a LINEAR SEARCH (also called sequential search). Even though the array is sorted,
    /// this function doesn't take advantage of that fact - it checks elements one by one.
    /// For sorted arrays, binary search (SearchSorted2) is much more efficient!
    private static int SearchSorted1(int[] data, int target) {
        var count = 0;
        // This loop: O(n) - may iterate through all elements in worst case
        foreach (var item in data) {
            count += 1;
            if (item == target)
                return count; // Found it - could be anywhere from 1 to n iterations
        }

        return count; // Didn't find it - checked all n elements
    }

    /// <summary>
    /// Search for 'target' in the list 'data'. When its found (or not found) the variable count which represents
    /// the work done in the function is returned.
    /// </summary>
    /// <param name="data">The array of numbers</param>
    /// <param name="target">The number we're looking for</param>
    /// <param name="start">The index of the starting section of the data to look in</param>
    /// <param name="end">The index of the ending section of the data to look in</param>
    ///
    /// BIG O NOTATION: O(log n) - Logarithmic Time
    /// Why:
    /// - Divide and conquer: O(log n) — Each recursive call eliminates half the remaining search space
    /// - Recursive calls: Each call checks the middle element, then searches either the upper or lower half
    /// - Search space reduction: n → n/2 → n/4 → n/8 → ... → 1 (approximately log₂(n) steps)
    /// - Example: For n=1000, you eliminate 500 → 250 → 125 → 62 → 31 → 15 → 7 → 3 → 1 (about 10 steps)
    /// Total: O(log n) — Logarithmic time because we cut the problem in half each time
    ///
    /// Note: This is BINARY SEARCH! It requires the array to be sorted and takes advantage of that fact.
    /// This is MUCH more efficient than linear search for large arrays. For n=1,000,000 elements,
    /// binary search needs only ~20 comparisons, while linear search needs up to 1,000,000!
    private static int SearchSorted2(int[] data, int target, int start, int end) {
        if (end < start)
            return 1; // All done - base case, checked 1 element
        var middle = (end + start) / 2;  // Calculate middle index
        if (data[middle] == target)
            return 1; // Found it - checked 1 element
        if (data[middle] < target) // Search in the upper half after index middle
            // Recursive call: searches right half (middle+1 to end)
            // Each call eliminates half the remaining elements
            return 1 + SearchSorted2(data, target, middle + 1, end);
        // Search in the lower half before index middle
        // Recursive call: searches left half (start to middle-1)
        // Each call eliminates half the remaining elements
        return 1 + SearchSorted2(data, target, start, middle - 1);
    }
}