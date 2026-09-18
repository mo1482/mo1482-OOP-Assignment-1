# LeetCode 1679 — Max Number of K-Sum Pairs

## Required technique
1. Sort the array first.
2. Put one pointer at the beginning and one at the end.
3. If the pair sum equals `k`, count one operation and move both pointers.
4. If the sum is smaller than `k`, move the left pointer forward.
5. If the sum is larger than `k`, move the right pointer backward.

## Complexity
- Sorting: O(n log n)
- Two-pointer scan: O(n)
- Total: O(n log n)
- Extra working space: O(1) apart from the sorting implementation's internal behavior.

## Submission proof
After submitting `Solution.cs` to LeetCode, save a screenshot of the real **Accepted** result as:

This file must be an actual screenshot, not a text placeholder.

Accept : https://leetcode.com/problems/max-number-of-k-sum-pairs/?roomId=jUSJYN