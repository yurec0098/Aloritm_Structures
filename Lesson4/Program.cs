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

			var node = TreeNode.BuildTree2(25);
			node.PrintTree();

			var node2 = node.GetNodeByValue(25);
			var node3 = node.GetNodeByValue(35);
			var node4 = node.GetNodeByValue(45);

			//Console.WriteLine(string.Join(", ", TreeHelper.GetTreeInLine(node).Select(x => x.Node.Value)));

			Console.WriteLine();
			node.PrintNode_BFS();
			node.PrintNode_DFS();
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
			Console.WriteLine();
		}
	}


	public class TreeNode : ITree
	{
		public int Value { get; set; }
		public TreeNode LeftChild { get; set; }
		public TreeNode RightChild { get; set; }
		public TreeNode Parent { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is TreeNode node)
				return node.Value == Value;
			return false;
		}


		public TreeNode GetRoot()
		{
			TreeNode root = this;
			while (root.Parent != null)
				root = root.Parent;
			return root;
		}

		public void AddItem(int value)
		{
			if (GetRoot() is TreeNode root)
				AddItem(root, value);
		}
		private void AddItem(TreeNode node, int value)
		{
			if(node.Value >= value)
			{
				if (node.RightChild != null)
					AddItem(node.RightChild, value);
				else
					node.RightChild = new TreeNode() { Value = value, Parent = node };
			}
			else
			{
				if (node.LeftChild != null)
					AddItem(node.LeftChild, value);
				else
					node.LeftChild = new TreeNode() { Value = value, Parent = node };
			}
		}


		public void RemoveItem(int value)
		{
			if (GetNodeByValue(value) is TreeNode node)
			{
				var root = node.Parent;
				if (root.LeftChild == node)
					root.LeftChild = null;
				if (root.RightChild == node)
					root.RightChild = null;
			}
		}

		public TreeNode GetNodeByValue(int value)
		{
			TreeNode node = GetRoot();
			while (node != null && node.Value != value)
			{
				if (node.Value > value)
					node = node.RightChild;
				else if (node.Value < value)
					node = node.LeftChild;
			}
			return node;
		}

		public void PrintTree()
		{
			if (GetRoot() is TreeNode root)
				PrintNode(root);
		}
		private void PrintNode(TreeNode node, string separator = "")
		{
			if(node != null)
			{
				PrintNode(node.LeftChild, $"{separator}  ");
				Console.WriteLine($"{separator}{node.Value}");
				PrintNode(node.RightChild, $"{separator}  ");
			}
		}


		public void PrintNode_DFS()
		{
			var result = new List<int>();
			var stack = new Stack<TreeNode>();
			stack.Push(this);

			while (stack.Count > 0)
			{
				var item = stack.Pop();
				result.Add(item.Value);

				if (item.LeftChild != null)
					stack.Push(item.LeftChild);
				if (item.RightChild != null)
					stack.Push(item.RightChild);
			}

			Console.Write("DFS: ");
			Console.WriteLine(string.Join(", ", result));
		}
		public void PrintNode_BFS()
		{
			var result = new List<int>();
			var queue = new Queue<TreeNode>();
			queue.Enqueue(this);

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();
				result.Add(item.Value);

				if (item.LeftChild != null)
					queue.Enqueue(item.LeftChild);
				if (item.RightChild != null)
					queue.Enqueue(item.RightChild);
			}

			Console.Write("BFS: ");
			Console.WriteLine(string.Join(", ", result));
		}


		static Random random = new Random();
		public static TreeNode BuildTree(int n)
		{
			TreeNode newNode = null;
			if (n == 0)
				return null;
			else
			{
				var nl = n / 2;
				var nr = n - nl - 1;
				newNode = new TreeNode();
				newNode.Value = random.Next(1, 100);
				newNode.LeftChild = BuildTree(nl);
				newNode.RightChild = BuildTree(nr);
			}
			return newNode;
		}
		public static TreeNode BuildTree2(int n)
		{
			TreeNode newNode = null;
			if (n == 0)
				return null;
			else
			{
				newNode = new TreeNode() { Value = random.Next(1, 100) };

				for (int i = 1; i < n; i++)
					newNode.AddItem(random.Next(1, 100));
			}
			return newNode;
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
