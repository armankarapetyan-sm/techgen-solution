namespace TechGenBasics.ArraysAlgorithms
{
    /// <summary>
    /// Bubble sort from worst to best — same O(n²) worst case, but best-case improves with each level.
    /// </summary>
    public static class Problem03_BubbleSort
    {
        /// <summary>
        /// Level 1 — Worst implementation.
        ///
        /// Time complexity:
        ///   Best case (already sorted):  O(n²) — still runs every pass and every comparison.
        ///   Average case:              O(n²)
        ///   Worst case (reverse sorted): O(n²)
        ///
        /// Why O(n²)?
        ///   Outer loop: always (n-1) passes.
        ///   Inner loop: always (n-1) comparisons per pass (bound never shrinks).
        ///   Total comparisons ≈ (n-1) × (n-1) → grows like n².
        ///
        /// Space: O(1) — sorts in place.
        /// </summary>
        public static void BubbleSortWorst(int[] values)
        {
            if (values == null || values.Length < 2)
            {
                return;
            }

            for (int pass = 0; pass < values.Length - 1; pass++)
            {
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i] > values[i + 1])
                    {
                        Swap(values, i, i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Level 2 — Basic bubble sort (shrinking inner bound).
        ///
        /// Time complexity:
        ///   Best case (already sorted):  O(n²) — still runs all (n-1) passes (no early exit).
        ///   Average case:              O(n²)
        ///   Worst case (reverse sorted): O(n²)
        ///
        /// Why O(n²)?
        ///   Pass 0 compares (n-1) pairs, pass 1 compares (n-2), …, pass (n-2) compares 1.
        ///   Total: (n-1) + (n-2) + … + 1 = n(n-1)/2 → O(n²).
        ///   Better constant factor than Worst (fewer comparisons), but same big-O class.
        ///
        /// Space: O(1).
        /// </summary>
        public static void BubbleSortBasic(int[] values)
        {
            if (values == null || values.Length < 2)
            {
                return;
            }

            for (int pass = 0; pass < values.Length - 1; pass++)
            {
                for (int i = 0; i < values.Length - 1 - pass; i++)
                {
                    if (values[i] > values[i + 1])
                    {
                        Swap(values, i, i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Level 3 — Early exit when a pass makes no swaps.
        ///
        /// Time complexity:
        ///   Best case (already sorted):  O(n) — one pass, (n-1) comparisons, then stop.
        ///   Average case:              O(n²)
        ///   Worst case (reverse sorted): O(n²) — same as basic; every pass still swaps.
        ///
        /// Why best case becomes O(n)?
        ///   Sorted array → first pass finds nothing to swap → break immediately.
        ///   Only one outer pass × (n-1) inner steps → linear.
        ///
        /// Why worst case stays O(n²)?
        ///   Reverse sorted needs swaps on every comparison for early passes → all passes run.
        ///
        /// Space: O(1).
        /// </summary>
        public static void BubbleSortWithEarlyExit(int[] values)
        {
            if (values == null || values.Length < 2)
            {
                return;
            }

            for (int pass = 0; pass < values.Length - 1; pass++)
            {
                bool swapped = false;

                for (int i = 0; i < values.Length - 1 - pass; i++)
                {
                    if (values[i] > values[i + 1])
                    {
                        Swap(values, i, i + 1);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }
        
        private static void Swap(int[] values, int first, int second)
        {
            int temp = values[first];
            values[first] = values[second];
            values[second] = temp;
        }
    }
}