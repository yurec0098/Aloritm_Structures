using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace Lesson3
{
	class Program
	{
		static void Main(string[] args)
		{
			//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
			BenchmarkRunner.Run<BechmarkClass>();
		}
	}

	[MemoryDiagnoser]
	[RankColumn]
	public class BechmarkClass
	{
		PointClass[] points = new PointClass[]
		{
			new PointClass { X = 10, Y = 13 },
			new PointClass { X = 99, Y = 83 },
			new PointClass { X = 18, Y = 35 },
			new PointClass { X = 38, Y = 43 },
			new PointClass { X = 55, Y = 11 },
			new PointClass { X = 48, Y = 72 }
		};
		PointStruct[] points2 = new PointStruct[]
		{
			new PointStruct { X = 10, Y = 13 },
			new PointStruct { X = 99, Y = 83 },
			new PointStruct { X = 18, Y = 35 },
			new PointStruct { X = 38, Y = 43 },
			new PointStruct { X = 55, Y = 11 },
			new PointStruct { X = 48, Y = 72 }
		};

		[Benchmark]
		public void TestPointDistance_Class()
		{
			for (int i = 0; i < 6; i = i + 2)
				PointDistance(points[i], points[i+1]);
		}
		[Benchmark]
		public void TestPointDistance_Struct()
		{
			for (int i = 0; i < 6; i = i + 2)
				PointDistance(points2[i], points2[i+1]);
		}
		[Benchmark]
		public void TestPointDistanceDouble_Struct()
		{
			for (int i = 0; i < 6; i = i + 2)
				PointDistanceDouble(points2[i], points2[i+1]);
		}
		[Benchmark]
		public void TestPointDistanceShort_Struct()
		{
			for (int i = 0; i < 6; i = i + 2)
				PointDistanceShort(points2[i], points2[i+1]);
		}

		public static float PointDistance(PointClass pointOne, PointClass pointTwo)
		{
			float x = pointOne.X - pointTwo.X;
			float y = pointOne.Y - pointTwo.Y;
			return MathF.Sqrt((x * x) + (y * y));
		}

		public static float PointDistance(PointStruct pointOne, PointStruct pointTwo)
		{
			float x = pointOne.X - pointTwo.X;
			float y = pointOne.Y - pointTwo.Y;
			return MathF.Sqrt((x * x) + (y * y));
		}

		public static double PointDistanceDouble(PointStruct pointOne, PointStruct pointTwo)
		{
			double x = pointOne.X - pointTwo.X;
			double y = pointOne.Y - pointTwo.Y;
			return Math.Sqrt((x * x) + (y * y));
		}

		public static float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
		{
			float x = pointOne.X - pointTwo.X;
			float y = pointOne.Y - pointTwo.Y;
			return (x * x) + (y * y);
		}
	}

	public class PointClass
	{
		public int X;
		public int Y;
	}

	public struct PointStruct
	{
		public int X;
		public int Y;
	}
}
