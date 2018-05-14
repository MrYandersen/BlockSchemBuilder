using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSchemBuilder
{
	static class Extencions
	{
		public static T[] Subsequence<T>(this IEnumerable<T> arr, int startIndex, int length)
		{
			return arr.Skip(startIndex).Take(length).ToArray();
		}

		//public static int IndexOfAny<T>(this T[] arr, T[] patterns, int startIndex = 0)
		//{
		//	int[] IDs = new int[patterns.Length];
		//	for (int i = 0; i < IDs.Length; i++)
		//	{
		//		IDs[i] = Array.Index
		//	}
		//}
	}
}
