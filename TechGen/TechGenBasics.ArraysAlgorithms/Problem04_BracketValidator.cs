namespace TechGenBasics.ArraysAlgorithms;

public class Problem04_BracketValidator
{
    public static bool IsValid(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        int n = input.Length;

        if (n % 2 != 0)
        {
            return false;
        }

        char[] stack = new char[n];
        int top = 0;

        for (int i = 0; i < n; i++)
        {
            char c = input[i];

            if (c == '(' || c == '[' || c == '{') // {([*
            {
                stack[top++] = c;
                continue;
            }

            if (top == 0) return false;
            // if (c != ')' && c != ']' && c != '}') return false;

            char open = stack[--top];
            if (!IsPair(open, c)) return false;
        }

        return top == 0;
    }

    private static bool IsPair(char open, char close)
    {
        return
            (open == '(' && close == ')')
            || (open == '[' && close == ']')
            || (open == '{' && close == '}');
    }
}