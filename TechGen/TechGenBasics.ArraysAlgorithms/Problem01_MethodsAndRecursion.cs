namespace TechGenBasics.ArraysAlgorithms
{
    /// <summary>
    /// Problem 1 — Methods, recursion, and overloading (reference solution).
    /// </summary>
    public static class Problem01_MethodsAndRecursion
    {
        /// <summary>
        /// Recursively sums digits. Base case: single-digit number.
        /// Example: 12345 → 5 + SumDigitsRecursive(1234) → … → 15
        /// </summary>
        public static int SumDigitsRecursive(int n)
        {
            n = Math.Abs(n);

            // Base case — stop recursion
            if (n < 10)
            {
                return n;
            }

            // Recursive case — last digit + sum of remaining digits
            return (n % 10) + SumDigitsRecursive(n / 10);
        }

        /// <summary>
        /// Checks palindrome ignoring case and non-letter/digit characters.
        /// </summary>
        public static bool IsPalindromeRecursive(string text)
        {
            if (text == null)
            {
                return true;
            }

            return IsPalindromeHelper(text, 0, text.Length - 1);
        }

        /// <summary>
        /// Two pointers move inward; skip spaces/punctuation.
        /// </summary>
        private static bool IsPalindromeHelper(string text, int left, int right)
        {
            while (left < right)
            {
                if (!char.IsLetterOrDigit(text[left]))
                {
                    left++;
                    continue;
                }

                if (!char.IsLetterOrDigit(text[right]))
                {
                    right--;
                    continue;
                }

                if (char.ToLower(text[left]) != char.ToLower(text[right]))
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        static bool IsPalindrome(string text, int left = 0, int right = -1)
        {
            text = text.ToLower();

            if (right == -1)
                right = text.Length - 1;

            // Base case
            if (left >= right)
                return true;

            // Characters do not match
            if (text[left] != text[right])
                return false;

            // Recursive call
            return IsPalindrome(text, left + 1, right - 1);
        }

        static bool IsPalindrome(string text)
        {
            text = text.ToLower();

            int left = 0;
            int right = text.Length - 1;

            while (left < right)
            {
                if (text[left] != text[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }

        /// <summary>Overload 1 of 2 — maximum of two integers.</summary>
        public static int Max(int a, int b)
        {
            if (a > b)
            {
                return a;
            }

            return b;
        }

        /// <summary>Overload 2 of 2 — reuses the two-parameter Max.</summary>
        public static int Max(int a, int b, int c)
        {
            return Max(Max(a, b), c);
        }
    }
}