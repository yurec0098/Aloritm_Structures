using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1
{
	class Program
	{
		static void Main(string[] args)
		{
			TestFibonacci();
			TestIsPrime();
		}

		#region Prime Number
		public class TestCase_PrimeNumber
		{
			static public uint[] PrimeNumbers = new uint[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101 };

			public uint Number { get; set; }
			public bool IsPrime { get; set; }
		}

		static void TestIsPrime()
		{
			var list = new List<TestCase_PrimeNumber>();
			for (uint i = 2; i <= 101; i++)
				list.Add(new TestCase_PrimeNumber() { Number = i, IsPrime = TestCase_PrimeNumber.PrimeNumbers.Contains(i)});

			Console.WriteLine("TestIsPrime");
			foreach (var testCase in list)
				TestIsPrime(testCase);

			Console.WriteLine("TestIsPrimeOptimize");
			foreach (var testCase in list)
				TestIsPrimeOptimize(testCase);
		}

		static void TestIsPrime(TestCase_PrimeNumber testCase)
		{
			if (IsPrimeNumber(testCase.Number) == testCase.IsPrime)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{(testCase.IsPrime ? "Prime " : "")}Number {testCase.Number,3} VALID TEST");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{(testCase.IsPrime ? "Prime " : "")}Number {testCase.Number,3} INVALID TEST");
				Console.ResetColor();
			}
		}
		static void TestIsPrimeOptimize(TestCase_PrimeNumber testCase)
		{
			if (IsPrimeNumberOptimize(testCase.Number) == testCase.IsPrime)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{(testCase.IsPrime ? "Prime " : "")}Number {testCase.Number,3} VALID TEST");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{(testCase.IsPrime ? "Prime " : "")}Number {testCase.Number,3} INVALID TEST");
				Console.ResetColor();
			}
		}

		//	по блок схеме
		static bool IsPrimeNumber(uint number)
		{
			uint d = 0;
			for (uint i = 2; i < number; i++)
				if (number % i == 0)
					d++;

			if (d == 0)
				return true;
			else
				return false;
		}
		//	исходя сути расчёта, убираем лишнее
		static bool IsPrimeNumberOptimize(uint number)
		{
			var length = number / 2;
			for (uint i = 2; i <= length; i++)
				if (number % i == 0)
					return false;
			
			return true;
		}
		#endregion

		//	O(n^3)
		public static int StrangeSum(int[] inputArray)
		{
			int sum = 0;
			for (int i = 0; i < inputArray.Length; i++)				//	O(n)
			{
				for (int j = 0; j < inputArray.Length; j++)			//	O(n)
				{
					for (int k = 0; k < inputArray.Length; k++)		//	O(n)
					{
						int y = 0;

						if (j != 0)
						{
							y = k / j;
						}

						sum += inputArray[i] + i + k + j + y;
					}
				}
			}

			return sum;
		}

		#region Fibonacci
		static void TestFibonacci()
		{
			for (int i = -10; i <= 10; i++)
				Console.WriteLine($"{i,3}: {FibonacciRecursion(i),3} | {FibonacciIteration(i),3}");
		}

		//	F(0) = 0,	F(1) = 1,	F(N) = F(N-2) + F(N-1)
		static decimal FibonacciRecursion(int n)
		{
			if (n == 0)
				return 0;
			if (n == 1)
				return 1;

			if(n >= 0)
				return FibonacciRecursion(n - 1) + FibonacciRecursion(n - 2);
			else
				return FibonacciRecursion(n + 2) - FibonacciRecursion(n + 1);
		}
		static decimal FibonacciIteration(int n)
		{
			if (n == 0)
				return 0;

			bool isNegative = false;
			if (n < 0)
			{
				n = -n;
				isNegative = true;
			}

			decimal result = 1;
			decimal f0 = 0;
			decimal f1 = 1;
			for (int i = 2; i <= n; i++)
			{
				result = f0 + f1;

				f0 = f1;
				f1 = result;
			}

			if (isNegative)
				return n % 2 == 0 ? -result : result;
			else
				return result;
		}
		#endregion
	}
}
