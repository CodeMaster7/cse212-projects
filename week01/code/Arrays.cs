public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // ============================================
        // STEP 1: UNDERSTAND THE PROBLEM
        // ============================================
        // Input:
        //   - number: the starting number (e.g., 7)
        //   - length: how many multiples to create (e.g., 5)
        // Output:
        //   - Array of doubles: {number, number*2, number*3, ..., number*length}
        //   - Example: MultiplesOf(7, 5) → {7, 14, 21, 28, 35}
        // Rules:
        //   - Create an array of the specified length
        //   - First element is 'number' itself
        //   - Each element in the array is number multiplied by (index + 1)

        // ============================================
        // STEP 2: PLAN AND DESIGN THE SOLUTION
        // ============================================
        // Plan:
        //   1. Create an array of size 'length' to hold the results
        //   2. Use a loop to fill the array from index 0 to length-1
        //   3. For each index i:
        //      - Calculate: number * (i + 1)
        //      - Store it in array[i]
        //   4. Return the completed array

        // Example of how the function works for MultiplesOf(7, 5):
        //   i=0: array[0] = 7 * 1 = 7
        //   i=1: array[1] = 7 * 2 = 14
        //   i=2: array[2] = 7 * 3 = 21
        //   i=3: array[3] = 7 * 4 = 28
        //   i=4: array[4] = 7 * 5 = 35
        //   Result: {7, 14, 21, 28, 35}

        // ============================================
        // STEP 3: IMPLEMENT THE SOLUTION
        // ============================================
        // TODO: Write the implementation code below

        // Create array to store results
        double[] result = new double[length];
        // Loop through each index from 0 to length-1
        for (int i = 0; i < length; i++)
        {
            //   Calculate multiple: number * (index + 1) and store in array[index]
            result[i] = number * (i + 1);
        }
        // Return the completed array
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // ============================================
        // STEP 1: UNDERSTAND THE PROBLEM
        // ============================================
        // Input:
        //   - data: List<int> to be rotated (e.g., {1, 2, 3, 4, 5, 6, 7, 8, 9})
        //   - amount: How many positions to rotate right (e.g., 3 or 5)
        // Output:
        //   - The original list is updated directly
        //   - Example 1: {1,2,3,4,5,6,7,8,9} with amount=5 → {5,6,7,8,9,1,2,3,4}
        //   - Example 2: {1,2,3,4,5,6,7,8,9} with amount=3 → {7,8,9,1,2,3,4,5,6}
        // Rules:
        //   - Rotate RIGHT means move elements from the END to the BEGINNING
        //   - The last 'amount' elements become the first 'amount' elements
        //   - Remaining elements shift to the right
        //   - amount is between 1 and data.Count (inclusive)
        //   - Modify the existing list (don't create a new one)

        // ============================================
        // STEP 2: PLAN AND DESIGN THE SOLUTION
        // ============================================
        // Plan:
        //   Strategy: Remove the last 'amount' elements one by one and insert them at the beginning
        //
        //   This approach uses a simple loop:
        //   1. Loop 'amount' times (from 0 to amount-1)
        //   2. In each iteration:
        //      a. Get the last element: data[data.Count - 1]
        //      b. Remove it from the end: data.RemoveAt(data.Count - 1)
        //      c. Insert it at the beginning: data.Insert(0, element)
        //
        //   Why this works:
        //   - We need to move the last 'amount' elements to the front
        //   - By removing each last element and inserting it at position 0,
        //     we naturally reverse their order (last becomes first, second-to-last becomes second, etc.)
        //   - This reverses the order correctly: if we have [1,2,3,4,5,6,7,8,9] and amount=3,
        //     we want [7,8,9] at the front, so removing 9,8,7 and inserting at 0 gives us [7,8,9,...]

        // Example of how the function works for {1,2,3,4,5,6,7,8,9} with amount=3:
        //   Original: [1, 2, 3, 4, 5, 6, 7, 8, 9]
        //   Elements to move: last 3 → [7, 8, 9]
        //
        //   Iteration 1:
        //     - Get last element: 9 (at index 8)
        //     - Remove it: data.RemoveAt(8) → [1, 2, 3, 4, 5, 6, 7, 8]
        //     - Insert at 0: data.Insert(0, 9) → [9, 1, 2, 3, 4, 5, 6, 7, 8]
        //
        //   Iteration 2:
        //     - Get last element: 8 (at index 8)
        //     - Remove it: data.RemoveAt(8) → [9, 1, 2, 3, 4, 5, 6, 7]
        //     - Insert at 0: data.Insert(0, 8) → [8, 9, 1, 2, 3, 4, 5, 6, 7]
        //
        //   Iteration 3:
        //     - Get last element: 7 (at index 8)
        //     - Remove it: data.RemoveAt(8) → [8, 9, 1, 2, 3, 4, 5, 6]
        //     - Insert at 0: data.Insert(0, 7) → [7, 8, 9, 1, 2, 3, 4, 5, 6]
        //
        //   Result: {7, 8, 9, 1, 2, 3, 4, 5, 6}

        // ============================================
        // STEP 3: IMPLEMENT THE SOLUTION
        // ============================================

        // TODO: Write the implementation code below
        // Implementation steps:
        //   1. Create a for loop that runs 'amount' times:
        for (int i = 0; i < amount; i++)
        {
            //      a. Store the last element in a variable:
            int lastElement = data[data.Count - 1];
            //      b. Remove the last element:
            data.RemoveAt(data.Count - 1);
            //      c. Insert the stored element at the beginning:
            data.Insert(0, lastElement);
        }
    }
}
