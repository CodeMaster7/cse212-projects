/// <summary>
/// These 3 functions will (in different ways) calculate the standard
/// deviation from a list of numbers.  The standard deviation
/// is defined as the square root of the variance.  The variance is
/// defined as the average of the squared differences from the mean.
/// </summary>
public static class StandardDeviation {
    public static void Run() {
        var numbers = new[] { 600, 470, 170, 430, 300 };
        Console.WriteLine(StandardDeviation1(numbers)); // Should be 147.322
        Console.WriteLine(StandardDeviation2(numbers)); // Should be 147.322
        Console.WriteLine(StandardDeviation3(numbers)); // Should be 147.322
    }

    // BIG O NOTATION: O(n) - Linear Time
    // Why: Two separate loops through the array, but they're sequential, not nested
    // Loop 1: O(n) - iterate through all numbers to calculate sum
    // Loop 2: O(n) - iterate through all numbers to calculate squared differences
    // Total: O(n) + O(n) = O(2n) = O(n) (we drop constants in Big O)
    private static double StandardDeviation1(int[] numbers) {
        var total = 0.0;
        var count = 0;
        // First loop: O(n) - touches each element once
        foreach (var number in numbers) {
            total += number;
            count += 1;
        }

        var avg = total / count;
        var sumSquaredDifferences = 0.0;
        // Second loop: O(n) - touches each element once
        foreach (var number in numbers) {
            sumSquaredDifferences += Math.Pow(number - avg, 2);
        }

        var variance = sumSquaredDifferences / count;
        return Math.Sqrt(variance);
    }

    // BIG O NOTATION: O(n²) - Quadratic Time
    // Why: NESTED LOOPS - outer loop runs n times, inner loop runs n times for EACH outer iteration
    // Outer loop: O(n) - iterate through all numbers
    // Inner loop: O(n) - for EACH number in outer loop, recalculate average by looping through ALL numbers
    // Total: O(n) × O(n) = O(n²)
    // PROBLEM: This recalculates the average n times (once for each number), even though the average never changes!
    // This is INEFFICIENT - StandardDeviation1 does the same thing in O(n) time.
    private static double StandardDeviation2(int[] numbers) {
        var sumSquaredDifferences = 0.0;
        var countNumbers = 0;
        // Outer loop: O(n) - runs n times
        foreach (var number in numbers) {
            var total = 0;
            var count = 0;
            // Inner loop: O(n) - runs n times FOR EACH iteration of outer loop
            // This means: n × n = n² total operations!
            foreach (var value in numbers) {
                total += value;
                count += 1;
            }

            var avg = total / count;
            sumSquaredDifferences += Math.Pow(number - avg, 2);
            countNumbers += 1;
        }

        var variance = sumSquaredDifferences / countNumbers;
        return Math.Sqrt(variance);
    }

    // BIG O NOTATION: O(n) - Linear Time
    // Why: Two operations that each touch the array once
    // numbers.Sum(): O(n) - LINQ Sum() loops through all elements once
    // foreach loop: O(n) - iterate through all numbers once
    // Total: O(n) + O(n) = O(2n) = O(n) (we drop constants in Big O)
    // This is essentially the same as StandardDeviation1, just using LINQ Sum() instead of manual loop
    private static double StandardDeviation3(int[] numbers) {
        var count = numbers.Length;
        // numbers.Sum() is O(n) - internally loops through array once
        var avg = (double)numbers.Sum() / count;
        var sumSquaredDifferences = 0.0;
        // This loop is O(n) - touches each element once
        foreach (var number in numbers) {
            sumSquaredDifferences += Math.Pow(number - avg, 2);
        }

        var variance = sumSquaredDifferences / count;
        return Math.Sqrt(variance);
    }
}