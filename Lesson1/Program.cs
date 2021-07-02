using System;

namespace Lesson1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}

		static bool IsPrimeNumber(int number)
		{
			int d = 0;
			for (int i = 2; i < number; i++)
				if (number % i == 0)
					d++;

			if (d == 0)
				return true;

			return false;
		}
	}
}
