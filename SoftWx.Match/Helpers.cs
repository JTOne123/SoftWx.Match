﻿using System.Runtime.CompilerServices;

namespace SoftWx.Match {
    internal static class Helpers {
        /// <summary>Determines the proper return value of an edit distance function when one or
        /// both strings are null.</summary>
        public static int NullDistanceResults(string string1, string string2, double maxDistance) {
            if (string1 == null) return (string2 == null) ? 0 : (string2.Length <= maxDistance) ? string2.Length : -1;
            return (string1.Length <= maxDistance) ? string1.Length : -1;
        }

        /// <summary>Determines the proper return value of an similarity function when one or
        /// both strings are null.</summary>
        public static int NullSimilarityResults(string string1, string string2, double minSimilarity) {
            return (string1 == null && string2 == null) ? 1 : (0 <= minSimilarity) ? 0 : -1;
        }

        /// <summary>Calculates starting position and lengths of two strings such that common
        /// prefix and suffix substrings are excluded.</summary>
        /// <remarks>Expects string1.Length to be less than or equal to string2.Length</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrefixSuffixPrep(string string1, string string2, out int len1, out int len2, out int start) {
            len2 = string2.Length;
            len1 = string1.Length; // this is also the minimun length of the two strings
            // suffix common to both strings can be ignored
            while (len1 != 0 && string1[len1 - 1] == string2[len2 - 1]) {
                len2--; len1--;
            }
            // prefix common to both strings can be ignored
            start = 0;
            while (start != len1 && string1[start] == string2[start]) start++;
            len2 -= start; // length of the part excluding common prefix and suffix
            len1 -= start;
        }

        /// <summary>Calculate a similarity measure from an edit distance.</summary>
        /// <param name="length">The length of the longer of the two strings the edit distance is from.</param>
        /// <param name="distance">The edit distance between two strings.</param>
        /// <returns>A similarity value from 0 to 1.0 (1 - (length / distance)).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceToSimilarity(int length, int distance) {
            return (distance < 0) ? -1 : 1 - (distance / (double)length);
        }
    }
}
