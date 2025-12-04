public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.  If the values in the
    /// sortedNumbers were inserted in order from left to right into the BST, then it
    /// would resemble a linked list (unbalanced). To get a balanced BST, the
    /// InsertMiddle function is called to find the middle item in the list to add
    /// first to the BST. The InsertMiddle function takes the whole list but also takes
    /// a range (first to last) to consider.  For the first call, the full range of 0 to
    /// Length-1 used.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// This function will attempt to insert the item in the middle of 'sortedNumbers' into
    /// the 'bst' tree. The middle is determined by using indices represented by 'first' and
    /// 'last'.
    /// For example, if the function was called on:
    ///
    /// sortedNumbers = new[]{10, 20, 30, 40, 50, 60};
    /// first = 0;
    /// last = 5;
    ///
    /// then the value 30 (index 2 which is the middle) would be added
    /// to the 'bst' (the insert function in the <see cref="BinarySearchTree"/> can be used
    /// to do this).
    ///
    /// Subsequent recursive calls are made to insert the middle from the values
    /// before 30 and the values after 30.  If done correctly, the order
    /// in which values are added (which results in a balanced bst) will be:
    ///
    /// 30, 10, 20, 50, 40, 60
    ///
    /// This function is intended to be called the first time by CreateTreeFromSortedList.
    ///
    /// The purpose for having the first and last parameters is so that we do
    /// not need to create new sub-lists when we make recursive calls.  Avoid
    /// using list slicing to create sub-lists to solve this problem.
    /// </summary>
    /// <param name="sortedNumbers">input numbers that are already sorted</param>
    /// <param name="first">the first index in the sortedNumbers to insert</param>
    /// <param name="last">the last index in the sortedNumbers to insert</param>
    /// <param name="bst">the BinarySearchTree in which to insert the values</param>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // TODO Problem 5: Build a balanced BST from a sorted array using divide-and-conquer

        // BASE CASE: Invalid range means no elements to process
        // This happens when we've processed all elements in this partition
        // Example: if first=3 and last=2, there are no elements between them
        if (first > last)
        {
            return;
        }

        // Find the middle index of the current range
        // By inserting the middle first, we ensure the tree stays balanced
        // Integer division automatically floors the result
        int middle = (first + last) / 2;

        // Insert the middle value into the BST
        // This becomes the root of this subtree
        bst.Insert(sortedNumbers[middle]);

        // RECURSIVE CASE 1: Process the LEFT half (all elements before middle)
        // These will become the left subtree
        // Range is from 'first' to one before 'middle'
        InsertMiddle(sortedNumbers, first, middle - 1, bst);

        // RECURSIVE CASE 2: Process the RIGHT half (all elements after middle)
        // These will become the right subtree
        // Range is from one after 'middle' to 'last'
        InsertMiddle(sortedNumbers, middle + 1, last, bst);

        // Why this works:
        // - By always inserting the middle element first, we create balanced subtrees
        // - Each level gets its "root" before its children
        // - Example: [10,20,30,40,50,60]
        //   1. Insert 30 (middle of full array)
        //   2. Left half [10,20]: insert 10, then 20
        //   3. Right half [40,50,60]: insert 50, then 40, then 60
        //   Result: balanced tree with height 3
    }
}