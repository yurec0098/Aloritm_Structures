using System;
using System.Collections.Generic;

namespace Lesson7
{
	class Program
	{
		static int[] a = { 1, 5, 2, 8, 9, 7, 4 };
		static int[] b = { 1, 5, 9, 2, 1, 6, 9, 7 };

		static void Main(string[] args)
		{
			var result = new List<int>();

			int lastIndexB = 0;
			for (int n = 0; n < a.Length; n++)
			{
				for (int i = lastIndexB; i < b.Length; i++)
				{
					if (a[n] == b[i])
					{
						result.Add(b[i]);
						lastIndexB = i + 1;
						break;
					}
				}
			}

			Console.WriteLine(string.Join(", ", result));
		}
	}
}
