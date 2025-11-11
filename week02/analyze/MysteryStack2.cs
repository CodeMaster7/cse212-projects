/// <summary>
/// Reverse Polish Notation (RPN) Calculator using a Stack
///
/// Why a Stack is Useful Here:
/// - When we encounter an operator (+, -, *, /), we need the TWO MOST RECENT numbers
/// - A stack's LIFO (Last-In-First-Out) property gives us exactly that - the last items added
/// - We push numbers onto the stack as we encounter them
/// - When we see an operator, we pop the last two numbers, calculate, and push the result back
/// - This matches perfectly with how RPN expressions are evaluated
///
/// Example: "5 3 2 * +"
///   1. Push 5 → stack = [5]
///   2. Push 3 → stack = [5, 3]
///   3. Push 2 → stack = [5, 3, 2]
///   4. See "*" → Pop 2, Pop 3 → Calculate 3*2=6 → Push 6 → stack = [5, 6]
///   5. See "+" → Pop 6, Pop 5 → Calculate 5+6=11 → Push 11 → stack = [11]
///   6. Return 11
///
/// The stack's LIFO nature is perfect because we always need the most recent operands,
/// not arbitrary positions like you'd need with a list or array.
/// </summary>
public static class MysteryStack2 {
    private static bool IsFloat(string text) {
        return float.TryParse(text, out _);
    }

    public static float Run(string text) {
        var stack = new Stack<float>();
        foreach (var item in text.Split(' ')) {
            if (item == "+" || item == "-" || item == "*" || item == "/") {
                if (stack.Count < 2)
                // Not enough numbers to perform the operation
                    throw new ApplicationException("Invalid Case 1!");

                var op2 = stack.Pop();
                var op1 = stack.Pop();
                float res;
                if (item == "+") {
                    res = op1 + op2;
                }
                else if (item == "-") {
                    res = op1 - op2;
                }
                else if (item == "*") {
                    res = op1 * op2;
                }
                else {
                    if (op2 == 0)
                    // Division by zero is not allowed
                        throw new ApplicationException("Invalid Case 2!");

                    res = op1 / op2;
                }

                stack.Push(res);
            }
            else if (IsFloat(item)) {
                stack.Push(float.Parse(item));
            }
            else if (item == "") {
            }
            else {
                // Invalid character
                throw new ApplicationException("Invalid Case 3!");
            }
        }

        if (stack.Count != 1)
        // Not enough numbers to perform the operation
            throw new ApplicationException("Invalid Case 4!");

        return stack.Pop();
    }
}