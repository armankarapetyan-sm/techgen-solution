using System;

namespace TechGenBasics.ArraysAlgorithms
{
    /// <summary>
    /// Fundamental array operations: scan for min/max, sum all elements, reverse in place.
    /// All methods use a single forward or two-pointer pass — no nested loops.
    /// </summary>
    public static class Problem01_BasicArrayOperations
    {
        /// <summary>
        /// Finds the minimum and maximum values in one pass and returns them as a value tuple.
        ///
        /// Time complexity:
        ///   Best / average / worst case: O(n) — each element is visited exactly once.
        ///
        /// Why O(n)?
        ///   One loop from index 1 to n-1; at most two comparisons per element (min and max).
        ///   Total work grows linearly with array length.
        ///
        /// Space: O(1) — only two int variables (min, max) besides the input array.
        /// </summary>
        public static (int Min, int Max) GetMinMax(int[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Array must not be empty.", nameof(values));
            }

            int min = values[0];
            int max = values[0];

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }

                if (values[i] > max)
                {
                    max = values[i];
                }
            }

            return (min, max);
        }

        /// <summary>
        /// Adds every element in the array and returns the total.
        ///
        /// Time complexity:
        ///   Best / average / worst case: O(n) — must read every element to compute the sum.
        ///
        /// Why O(n)?
        ///   Single loop over all n indices; one addition per element.
        ///
        /// Space: O(1) — one accumulator variable (total).
        /// </summary>
        public static int Sum(int[] values)
        {
            if (values == null || values.Length == 0)
            {
                return 0;
            }

            int total = 0;
            for (int i = 0; i < values.Length; i++)
            {
                total = total + values[i];
            }

            return total;
        }

        /// <summary>
        /// Reverses the array in place using two pointers (left and right) that swap and move inward.
        ///
        /// Time complexity:
        ///   Best / average / worst case: O(n) — n/2 swap operations for an array of length n.
        ///
        /// Why O(n)?
        ///   Each swap handles two positions; left and right meet in the middle after n/2 iterations.
        ///   Linear in array size, with a constant factor of 1/2.
        ///
        /// Space: O(1) — only left, right, and one temp variable; no extra array allocated.
        /// </summary>
        public static void ReverseInPlace(int[] values)
        {
            if (values == null)
            {
                return;
            }

            int left = 0;
            int right = values.Length - 1;
            while (left < right)
            {
                int temp = values[left];
                values[left] = values[right];
                values[right] = temp;
                left++;
                right--;
            }
        }

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void Reverse(int[] values)
        {
            int left = 0;
            int right = values.Length - 1;

            while (left < right)
            {
                Swap(ref values[left], ref values[right]);

                left++;
                right--;
            }
        }
    }
}