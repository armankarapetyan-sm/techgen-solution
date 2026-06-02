using System;

namespace TechGenBasics.ArraysAlgorithms
{
    /// <summary>
    /// Search and count operations on arrays: linear scan, occurrence counting, and binary search.
    /// Linear methods work on any array; binary search requires a sorted array.
    /// </summary>
    public static class Problem02_SearchAndCount
    {
        /// <summary>
        /// Scans the array from left to right and returns the index of the first match, or -1 if not found.
        ///
        /// Time complexity:
        ///   Best case (target is at index 0):     O(1) — one comparison.
        ///   Average case:                         O(n) — target is somewhere in the middle on average.
        ///   Worst case (target missing or last):  O(n) — every element checked.
        ///
        /// Why O(n) worst case?
        ///   Unsorted array — no shortcut; must inspect elements one by one until found or exhausted.
        ///
        /// Space: O(1) — only loop index i; no extra storage.
        /// </summary>
        public static int LinearSearch(int[] values, int target)
        {
            if (values == null)
            {
                return -1;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Counts how many times target appears in the array.
        ///
        /// Time complexity:
        ///   Best / average / worst case: O(n) — every element must be examined to count all matches.
        ///
        /// Why O(n)?
        ///   Unlike search, we cannot stop at the first match; the full array is always scanned.
        ///
        /// Space: O(1) — one counter variable.
        /// </summary>
        public static int CountOccurrences(int[] values, int target)
        {
            if (values == null)
            {
                return 0;
            }

            int count = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == target)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Finds target in a sorted array by repeatedly halving the search range.
        ///
        /// Precondition: sorted must be sorted in ascending order.
        ///
        /// Time complexity:
        ///   Best case (target at middle):  O(1) — found on first mid calculation.
        ///   Average / worst case:          O(log n) — range halves each iteration.
        ///
        /// Why O(log n)?
        ///   Each step discards half the remaining elements.
        ///   After k steps, at most n / 2^k elements remain → k ≈ log₂(n) steps.
        ///
        /// Space: O(1) — only left, right, and mid indices; iterative, no recursion stack.
        /// </summary>
        public static int BinarySearch(int[] sorted, int target)
        {
            if (sorted == null || sorted.Length == 0)
            {
                return -1;
            }

            int left = 0;
            int right = sorted.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (sorted[mid] == target)
                {
                    return mid;
                }

                if (sorted[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}
