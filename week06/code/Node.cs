public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Problem 1: Insert with no duplicates

        // If value already exists, don't insert it (prevent duplicates)
        if (value == Data)
        {
            return;
        }

        if (value < Data)
        {
            // Value is smaller, so it belongs on the LEFT side
            // BST Property: all left children are less than parent
            if (Left is null)
                Left = new Node(value);  // Empty spot found, create new node
            else
                Left.Insert(value);       // Keep searching down the left subtree
        }
        else
        {
            // Value is larger, so it belongs on the RIGHT side
            // BST Property: all right children are greater than parent
            if (Right is null)
                Right = new Node(value);  // Empty spot found, create new node
            else
                Right.Insert(value);      // Keep searching down the right subtree
        }
    }

    public bool Contains(int value)
    {
        // TODO Problem 2: Search for a value in the BST

        // BASE CASE: Found the value! We're done searching.
        if (value == Data)
        {
            return true;
        }

        // RECURSIVE CASE: Use BST property to decide which direction to search
        // If the value is smaller than the current node, search the left subtree
        if (value < Data)
        {
            // Value is smaller, so it would be on the LEFT (if it exists)
            // If there's no left child, the value isn't in the tree
            return Left != null && Left.Contains(value);
        }
        // If the value is larger than the current node, search the right subtree
        else
        {
            // Value is larger, so it would be on the RIGHT (if it exists)
            // If there's no right child, the value isn't in the tree
            return Right != null && Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // TODO Problem 4: Calculate the height of the tree rooted at this node
        // Height = the longest path from this node to any leaf

        // Get the height of the left subtree
        // If no left child, that subtree has height 0
        // only call GetHeight if Left isn't null
        // if result is null, use 0 instead
        int leftHeight = Left?.GetHeight() ?? 0;

        // Get the height of the right subtree
        // Same logic as left side
        int rightHeight = Right?.GetHeight() ?? 0;

        // This node's height = 1 (for itself) + the taller of the two subtrees
        // We use Math.Max to pick the larger height
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}