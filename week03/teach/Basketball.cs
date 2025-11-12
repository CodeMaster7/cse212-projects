/*
 * CSE 212 Lesson 6C
 *
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 *
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 *
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        // Dictionary to map player ID to their total career points
        // Key = player ID (string), Value = total points (int)
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            // Check if this player already exists in our dictionary - O(1)
            if (players.ContainsKey(playerId)) {
                // Player exists, add this season's points to their total
                players[playerId] += points;
            }
            else {
                // New player, create entry with their first season's points
                players[playerId] = points;
            }
        }

        // Convert dictionary to array of key-value pairs so we can sort it
        var playerArray = players.ToArray();

        // Sort by points (Value) in descending order (highest first)
        // OrderByDescending looks at each pair and sorts by the Value (points)
        var sortedPlayers = playerArray.OrderByDescending(player => player.Value).ToArray();

        // Display top 10 players
        Console.WriteLine("\nTop 10 Players by Career Points:");
        Console.WriteLine("================================");
        for (int i = 0; i < 10 && i < sortedPlayers.Length; i++) {
            var player = sortedPlayers[i];
            // Format: rank. PlayerID: points
            Console.WriteLine($"{i + 1}. {player.Key}: {player.Value:N0} points");
        }
    }
}