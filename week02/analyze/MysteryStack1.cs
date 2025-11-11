/// <summary>
/// String Reverser using a Stack
///
/// Why a Stack is Useful Here:
/// - This code reverses a string by leveraging the stack's LIFO (Last-In-First-Out) property
/// - When you push characters in order and then pop them all, they come out in reverse order
/// - The stack naturally reverses the sequence without needing to manually track indices
///
/// Example: "hello"
///   1. Push 'h' → stack = ['h']
///   2. Push 'e' → stack = ['h', 'e']
///   3. Push 'l' → stack = ['h', 'e', 'l']
///   4. Push 'l' → stack = ['h', 'e', 'l', 'l']
///   5. Push 'o' → stack = ['h', 'e', 'l', 'l', 'o']
///   6. Pop 'o' → result = "o"
///   7. Pop 'l' → result = "ol"
///   8. Pop 'l' → result = "oll"
///   9. Pop 'e' → result = "olle"
///   10. Pop 'h' → result = "olleh"
///
/// The stack's LIFO nature automatically reverses the order - the last character pushed
/// is the first one popped, giving us the reversed string without any index manipulation.
/// </summary>
public static class MysteryStack1 {
    public static string Run(string text) {
        var stack = new Stack<char>();
        foreach (var letter in text)
            stack.Push(letter);

        var result = "";
        while (stack.Count > 0)
            result += stack.Pop();

        return result;
    }
}