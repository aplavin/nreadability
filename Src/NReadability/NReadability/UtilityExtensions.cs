/*
 * NReadability
 * http://code.google.com/p/nreadability/
 * 
 * Copyright 2010 Marek Stój
 * http://immortal.pl/
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace NReadability
{
    using System.IO;
    using System.Text;

    internal static class UtilityExtensions
    {
        #region Public methods

        public static bool IsCloseToZero(this float x)
        {
            return Math.Abs(x) < float.Epsilon;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }
        }

        /// <summary>
        /// Gets StreamReader for this <paramref name="stream"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">When stream doesn't support reading.</exception>
        public static StreamReader GetReader(this Stream stream, Encoding encoding = null)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (!stream.CanRead) throw new InvalidOperationException("Stream doesn't support reading");

            return encoding == null ?
                new StreamReader(stream) :
                new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Returns seekable MemoryStream which contains the same data as this <paramref name="stream"/> (from current position).
        /// This <paramref name="stream"/> position after this call will be at the end.
        /// </summary>
        public static MemoryStream ToSeekable(this Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        /// <summary>
        /// Gets the string between the first occurrence of <paramref name="left"/> and the first occurrence of <paramref name="right"/> after left's one.
        /// </summary>
        public static string GetBetween(this string s, string left, string right)
        {
            if (s == null) throw new ArgumentNullException("s");
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");

            int leftIndex = s.IndexOf(left);
            int startIndex = leftIndex + left.Length;
            if (leftIndex == -1)
                throw new InvalidOperationException("This string doesn't contain specified left string.");

            int rightIndex = s.IndexOf(right, startIndex);
            int endIndex = rightIndex;
            if (rightIndex == -1)
                throw new InvalidOperationException("This string doesn't contain specified right string after the first occurrence of specified left string.");

            return s.Substring(startIndex, endIndex - startIndex);
        }

        #endregion
    }
}
