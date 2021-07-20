using System;
using System.Collections.Generic;

namespace Lesson7
{
	class Program
	{

		static void Main(string[] args)
		{
			int[] a = { 1, 5, 2, 8, 9, 7, 4 };
			int[] b = { 1, 5, 9, 2, 1, 6, 9, 7 };

			var result1 = GetNumbers(a, b);
			var result2 = GetNumbers(b, a);

			Console.WriteLine(string.Join(", ", result1));
			Console.WriteLine(string.Join(", ", result2));
		}

		private static List<int> GetNumbers(int[] first, int[] second)
		{
			var result = new List<int>();
			int lastIndex = 0;
			for (int n = 0; n < first.Length; n++)
			{
				for (int i = lastIndex; i < second.Length; i++)
				{
					if (first[n] == second[i])
					{
						result.Add(second[i]);
						lastIndex = i + 1;
						break;
					}
				}
			}
			return result;
		}
	}
}
