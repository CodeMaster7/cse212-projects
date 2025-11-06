public static class ComplexStack {
    /// <summary>
    /// Checks if brackets/parentheses in a string are properly balanced and matched.
    /// This function validates that:
    /// 1. Every opening bracket has a matching closing bracket
    /// 2. Brackets are properly nested (last opened, first closed)
    /// 3. All brackets are of the correct type (round, square, curly)
    /// </summary>
    /// <param name="line">The string containing brackets to check</param>
    /// <returns>True if all brackets are balanced and matched, False otherwise</returns>
    public static bool DoSomethingComplicated(string line) {
        // Create a stack to store opening brackets as we encounter them
        // The last opening bracket we see should match the first closing bracket we encounter
        var stack = new Stack<char>();

        // Loop through each character in the string
        foreach (var item in line) {
            // If we find an OPENING bracket: push it onto the stack
            // We'll match it later when we find its closing bracket
            if (item is '(' or '[' or '{') {
                stack.Push(item);
            }
            // If we find a closing round bracket ')'
            else if (item is ')') {
                // Check if stack is empty (more closing than opening brackets) OR
                // Check if the last opening bracket doesn't match '('
                if (stack.Count == 0 || stack.Pop() != '(')
                    return false; // Mismatch! Return false immediately
            }
            // If we find a closing square bracket ']'
            else if (item is ']') {
                // Check if stack is empty OR last opening bracket doesn't match '['
                if (stack.Count == 0 || stack.Pop() != '[')
                    return false; // Mismatch!
            }
            // If we find a closing curly bracket '}'
            else if (item is '}') {
                // Check if stack is empty OR last opening bracket doesn't match '{'
                if (stack.Count == 0 || stack.Pop() != '{')
                    return false; // Mismatch!
            }
        }

        // After processing all characters, check if stack is empty
        // Empty stack = all opening brackets had matching closing brackets
        // Non-empty stack = there are unmatched opening brackets remaining
        return stack.Count == 0;
    }
}