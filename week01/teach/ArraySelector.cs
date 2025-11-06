public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        // Step 1: Create a dynamic list to store our combined results
        List<int> result = new List<int>();

        // Step 2: Create separate index counters for each source array
        int index1 = 0;
        int index2 = 0;

        // Step 3: Loop through each value in the selector array
        for (int i = 0; i < select.Length; i++)
        {
            // Step 4: Check what the selector value is
            if (select[i] == 1)
            {
                // If selector is 1, take from list1 and add it to our result list
                result.Add(list1[index1]);
                index1++;
            }
            else if (select[i] == 2)
            {
                // If selector is 2, take from list2 and add it to our result list
                result.Add(list2[index2]);
                index2++;
            }
        }

        // Step 5: Convert our List<int> to int[] array and return it
        return result.ToArray();
    }
}