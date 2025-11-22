using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        // Create a HashSet to store all words we've seen - O(n) to create
        // HashSet provides O(1) lookup time which is key to achieving O(n) overall
        var wordSet = new HashSet<string>(words);

        // List to store the paired results
        var pairs = new List<string>();

        // HashSet to track words we've already paired
        // This prevents duplicates like having both "am & ma" and "ma & am"
        var processedWords = new HashSet<string>();

        // Loop through each word once - O(n)
        foreach (var word in words)
        {
            // Skip if we've already paired this word
            if (processedWords.Contains(word))
                continue;

            // Check if both letters are the same (like "aa")
            // These don't have pairs since there are no duplicates in the list
            if (word[0] == word[1])
                continue;

            // Create the reverse of the current word
            // For "am", reverse is "ma"
            var reverse = new string(new[] { word[1], word[0] });

            // Check if the reverse exists in our set - O(1) lookup
            if (wordSet.Contains(reverse))
            {
                // We found a pair! Add it to results
                pairs.Add($"{word} & {reverse}");

                // Mark both words as processed so we don't create duplicate pairs
                processedWords.Add(word);
                processedWords.Add(reverse);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            // The degree is in column 4 (index 3 since arrays are 0-based)
            // Make sure the line has at least 4 columns to avoid index errors
            if (fields.Length > 3)
            {
                var degree = fields[3];

                // Check if this degree already exists in our dictionary
                if (degrees.ContainsKey(degree))
                {
                    // If it exists, increment the count
                    degrees[degree]++;
                }
                else
                {
                    // If it's a new degree, add it with a count of 1
                    degrees[degree] = 1;
                }

                // Alternative shorter syntax using TryGetValue:
                // degrees[degree] = degrees.TryGetValue(degree, out int count) ? count + 1 : 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a
    /// new word.  A dictionary is used to solve the problem.
    ///
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    ///
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For
    /// example, 'Ab' and 'Ba' should be considered anagrams
    ///
    /// Reminder: You can access a letter by index in a string by
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // Remove spaces and convert to lowercase as per requirements
        // ToUpper() works too - we just need consistent casing
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If the lengths are different after removing spaces, they can't be anagrams
        // This is an early optimization to avoid unnecessary processing
        if (word1.Length != word2.Length)
            return false;

        // Create a dictionary to count the frequency of each letter in word1
        // Key = letter, Value = how many times it appears
        var letterCount = new Dictionary<char, int>();

        // Count all letters in word1
        foreach (var letter in word1)
        {
            if (letterCount.ContainsKey(letter))
            {
                letterCount[letter]++;
            }
            else
            {
                letterCount[letter] = 1;
            }
        }

        // Now check word2 - subtract each letter from our count
        foreach (var letter in word2)
        {
            // If the letter doesn't exist in our dictionary,
            // word2 has a letter that word1 doesn't have
            if (!letterCount.ContainsKey(letter))
            {
                return false;
            }

            // Decrease the count for this letter
            letterCount[letter]--;

            // If we go below zero, word2 has more of this letter than word1
            if (letterCount[letter] < 0)
            {
                return false;
            }
        }

        // At this point, all counts should be exactly 0
        // If any count is positive, word1 had more of that letter
        // But since we already checked lengths are equal, all counts must be 0
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    ///
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found
    /// at this website:
    ///
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    ///
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // Create a list to store our formatted strings
        var earthquakeSummaries = new List<string>();

        // Loop through each earthquake feature in the collection
        // The null-conditional operator (?.) safely handles if Features is null
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                // Extract the place and magnitude from the properties
                var place = feature.Properties.Place;
                var magnitude = feature.Properties.Mag;

                // Format the string exactly as specified:
                // "location - Mag magnitude"
                var summary = $"{place} - Mag {magnitude}";

                // Add to our list
                earthquakeSummaries.Add(summary);
            }
        }

        // Convert the list to an array and return
        return earthquakeSummaries.ToArray();
    }
}