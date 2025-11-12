public static class UniqueLetters {
    public static void Run() {
        var test1 = "abcdefghjiklmnopqrstuvwxyz"; // Expect True because all letters unique
        Console.WriteLine(AreUniqueLetters(test1));

        var test2 = "abcdefghjiklanopqrstuvwxyz"; // Expect False because 'a' is repeated
        Console.WriteLine(AreUniqueLetters(test2));

        var test3 = "";
        Console.WriteLine(AreUniqueLetters(test3)); // Expect True because its an empty string
    }

    /// <summary>Determine if there are any duplicate letters in the text provided</summary>
    /// <param name="text">Text to check for duplicate letters</param>
    /// <returns>true if all letters are unique, otherwise false</returns>
    private static bool AreUniqueLetters(string text) {
        // Create a HashSet to store letters we've seen
        // A set only stores unique values and has O(1) lookup time
        var seenLetters = new HashSet<char>();

        // Loop through each character once - O(n)
        foreach (var letter in text) {
            // Check if we've already seen this letter - O(1)
            if (seenLetters.Contains(letter)) {
                // Found a duplicate! Return false immediately
                return false;
            }

            // Haven't seen this letter yet, add it to our set - O(1)
            seenLetters.Add(letter);
        }

        // Made it through the whole string without finding duplicates
        return true;
    }
}