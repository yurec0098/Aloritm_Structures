using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lesson4
{
	class Program
	{
		static void Main(string[] args)
		{
			TestSearchTime();
		}

		static void TestSearchTime()
		{
			var searchStrs = new List<string>();

			var hs = new HashSet<string>();
			var arr = new string[100_000];

			for (int i = 0; i < 100_000; i++)
			{
				var guid = Guid.NewGuid().ToString();
				arr[i] = guid;
				hs.Add(guid);

				if (i % 1000 == 0)
					searchStrs.Add(guid);
			}

			#region Array
			Console.WriteLine("Search in array");
			var arrTicks = new List<long>();
			var sw = new Stopwatch();
			foreach (var str in searchStrs)
			{
				sw.Restart();
				if (arr.Contains(str))
				{
					sw.Stop();
					Console.WriteLine($"[{str}], ticks = {sw.ElapsedTicks}");
					arrTicks.Add(sw.ElapsedTicks);
				}
			}
			Console.WriteLine();
			#endregion

			#region HashSet
			Console.WriteLine("Search in HashSet");
			var hsTicks = new List<long>();
			foreach (var str in searchStrs)
			{
				sw.Restart();
				if (hs.Contains(str))
				{
					sw.Stop();
					Console.WriteLine($"[{str}], ticks = {sw.ElapsedTicks}");
					hsTicks.Add(sw.ElapsedTicks);
				}
			}
			Console.WriteLine();
			#endregion

			Console.WriteLine($"Search in Array average time {arrTicks.Min()} - {arrTicks.Max()} ({arrTicks.Sum() / arrTicks.Count})");
			Console.WriteLine($"Search in HashSet average time {hsTicks.Min()} - {hsTicks.Max()} ({hsTicks.Sum() / hsTicks.Count})");
		}
	}







	public class TreeNode
	{
		public int Value { get; set; }
		public TreeNode LeftChild { get; set; }
		public TreeNode RightChild { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is TreeNode node)
				return node.Value == Value;
			return false;
		}
	}

	public interface ITree
	{
		TreeNode GetRoot();
		void AddItem(int value);	// добавить узел
		void RemoveItem(int value);	// удалить узел по значению
		TreeNode GetNodeByValue(int value);	//получить узел дерева по значению
		void PrintTree();	//вывести дерево в консоль
	}

	public static class TreeHelper
	{
		public static NodeInfo[] GetTreeInLine(ITree tree)
		{
			var bufer = new Queue<NodeInfo>();
			var returnArray = new List<NodeInfo>();
			var root = new NodeInfo() { Node = tree.GetRoot() };
			bufer.Enqueue(root);

			while (bufer.Count != 0)
			{
				var element = bufer.Dequeue();
				returnArray.Add(element);

				var depth = element.Depth + 1;

				if (element.Node.LeftChild != null)
				{
					var left = new NodeInfo()
					{
						Node = element.Node.LeftChild,
						Depth = depth,
					};
					bufer.Enqueue(left);
				}
				if (element.Node.RightChild != null)
				{
					var right = new NodeInfo()
					{
						Node = element.Node.RightChild,
						Depth = depth,
					};
					bufer.Enqueue(right);
				}
			}

			return returnArray.ToArray();
		}
	}

	public class NodeInfo
	{
		public int Depth { get; set; }
		public TreeNode Node { get; set; }
	}
}
