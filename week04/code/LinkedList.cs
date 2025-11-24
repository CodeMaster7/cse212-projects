using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    ///
    /// TIME COMPLEXITY: O(1) - Constant Time
    /// Why? Because we have a direct pointer to the tail (_tail variable).
    /// We don't need to loop through the list to find the end.
    /// We just adjust a few pointers (newNode.Prev, _tail.Next, _tail) and we're done.
    /// No matter if the list has 10 nodes or 10,000 nodes, it takes the same amount of steps.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1
        // Step 1: Create new node with the value we want to insert
        Node newNode = new(value);

        // Step 2: Check if list is empty (both head and tail are null)
        // If empty, this new node becomes both the head AND tail
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // Step 3: If list is NOT empty, add to the end
        else
        {
            // Connect new node's PREV to current tail (so new node knows what's before it)
            newNode.Prev = _tail;

            // Connect current tail's NEXT to new node (so old tail points forward to new node)
            _tail.Next = newNode;

            // Update tail pointer to the new node (this is now the last node)
            _tail = newNode;
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    ///
    /// TIME COMPLEXITY: O(1) - Constant Time
    /// Why? We have a direct pointer to the tail (_tail variable).
    /// We don't need to traverse the list to find the last node.
    /// We just update _tail.Prev.Next and move _tail backwards by one node.
    /// These are simple pointer assignments that take the same time regardless of list size.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2
        // Step 1: Check if list has only one item (head and tail point to same node)
        // OR if list is empty (both are null) - in both cases, just clear everything
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // Step 2: If list has more than one item, remove the tail
        else if (_tail is not null)
        {
            // Disconnect the second-to-last node from the tail
            // (make the second-to-last node's NEXT point to null instead of tail)
            _tail.Prev!.Next = null;

            // Move tail pointer backwards to the second-to-last node
            // (this node is now the new tail)
            _tail = _tail.Prev;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    ///
    /// TIME COMPLEXITY: O(n) - Linear Time
    /// Why? We must SEARCH through the list to find the node with 'value'.
    /// In the worst case, the value is at the END of the list (or not in the list at all).
    /// So we have to visit EVERY node: if n = list size, we check n nodes.
    /// The actual removal (adjusting pointers) is O(1), but the search makes the whole operation O(n).
    ///
    /// Best case: O(1) if value is at the head (first node)
    /// Worst case: O(n) if value is at the tail or not found
    /// Average case: O(n) - we might find it in the middle, but still grows with list size
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3
        // Step 1: Start searching from the head of the list
        Node? curr = _head;

        // Step 2: Loop through each node until we find the value or reach the end
        while (curr is not null)
        {
            // Step 3: Check if current node has the value we're looking for
            if (curr.Data == value)
            {
                // CASE A: Removing the ONLY node in the list
                // (node is both head AND tail)
                if (curr == _head && curr == _tail)
                {
                    _head = null;
                    _tail = null;
                }
                // CASE B: Removing the HEAD (first node, but there are more nodes after it)
                else if (curr == _head)
                {
                    // Disconnect second node from current head
                    _head.Next!.Prev = null;
                    // Move head pointer to second node
                    _head = _head.Next;
                }
                // CASE C: Removing the TAIL (last node, but there are nodes before it)
                else if (curr == _tail)
                {
                    // Disconnect second-to-last node from tail
                    _tail.Prev!.Next = null;
                    // Move tail pointer to second-to-last node
                    _tail = _tail.Prev;
                }
                // CASE D: Removing a MIDDLE node (has nodes before AND after it)
                else
                {
                    // Connect the node BEFORE curr to the node AFTER curr
                    // (essentially "skip over" curr, disconnecting it)
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }

                // Step 4: Exit after removing the FIRST occurrence
                return;
            }

            // Step 5: Move to next node to continue searching
            curr = curr.Next;
        }
        // If we get here, value was not found in the list (nothing to remove)
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    ///
    /// TIME COMPLEXITY: O(n) - Linear Time
    /// Why? We MUST visit EVERY single node in the list, no shortcuts.
    /// Even if we find the value early, we keep going because we need to replace ALL occurrences.
    /// If the list has n nodes, we check all n nodes exactly once.
    /// The replacement itself (curr.Data = newValue) is O(1), but doing it n times makes it O(n).
    ///
    /// Example: List with 100 nodes → we check all 100 nodes = O(100) = O(n)
    /// Example: List with 1,000 nodes → we check all 1,000 nodes = O(1000) = O(n)
    /// Notice: 10x more nodes = 10x more operations (linear growth)
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4
        // Step 1: Start at the beginning of the list
        Node? curr = _head;

        // Step 2: Loop through ENTIRE list (don't stop at first match!)
        while (curr is not null)
        {
            // Step 3: If current node has the old value, replace it with new value
            if (curr.Data == oldValue)
            {
                // Replace the data in this node
                // (We DON'T remove/add nodes, just change the value)
                curr.Data = newValue;
            }

            // Step 4: Move to next node (keep searching for more matches)
            curr = curr.Next;
        }
        // Note: Unlike Remove(), we DON'T return early - we replace ALL occurrences
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    ///
    /// TIME COMPLEXITY: O(n) - Linear Time (when fully enumerated)
    /// Why? This method uses 'yield return' which makes it a GENERATOR/ITERATOR.
    /// It doesn't do all the work upfront - it returns one value at a time on demand.
    ///
    /// - Calling Reverse() itself is O(1) - just sets up the iterator
    /// - But when you LOOP through it (foreach, ToList(), etc.), you visit every node
    /// - If you iterate through all n nodes, that's O(n) total
    ///
    /// Example: foreach (var item in linkedList.Reverse()) → O(n) because we visit all nodes
    /// Example: linkedList.Reverse().First() → O(1) because we only get the first (tail) value
    ///
    /// This is SPACE efficient: O(1) extra space (no new list created, just traversing existing nodes)
    /// Compare to creating a reversed copy: that would be O(n) space!
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        // Step 1: Start at the END of the list (tail) since we're going backwards
        var curr = _tail;

        // Step 2: Loop from tail to head (backwards traversal)
        while (curr is not null)
        {
            // Step 3: Yield (provide) each node's data to the caller
            // (yield means "give this value to whoever is iterating, then pause until they ask for next")
            yield return curr.Data;

            // Step 4: Move to PREVIOUS node (going backwards!)
            // This is the key difference from GetEnumerator() which uses curr.Next
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}