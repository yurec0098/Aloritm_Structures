using GeekBrainsTests;
using System;
using System.Diagnostics;
using System.Linq;

namespace Lesson2
{
	class Program
	{
		static void Main(string[] args)
		{
			MyLinkedList myLinkedList = new MyLinkedList();
			for (int i = 100000; i >= 0; i--)
				myLinkedList.AddNode(i);

			var stopWatch = Stopwatch.StartNew();
			var array = myLinkedList.ToArray();
			array = array.OrderBy(x => x.Value).ToArray();
			stopWatch.Stop();
			Console.WriteLine($"LinkedList.ToArray() = {stopWatch.ElapsedTicks}");

			Node findNode;
			Node BS_Node;

			var findTicks = 0L;
			var binarySearchTicks = 0L;

			var ramdom = new Random();
			for (int i = 1; i < 250; i++)
			{
				int searchValue = array[ramdom.Next(1, array.Length) -1].Value;

				stopWatch.Restart();
				findNode = myLinkedList.FindNode(searchValue);
				stopWatch.Stop();
				findTicks = stopWatch.ElapsedTicks;

				stopWatch.Restart();
				BS_Node = MyLinkedList.BinarySearch(array, searchValue);
				stopWatch.Stop();
				binarySearchTicks = stopWatch.ElapsedTicks;

				if (findNode != BS_Node)	//	Сравнение по ссылке
					Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Find seconds = {findTicks, 5},  BinarySearch = {binarySearchTicks}");
				Console.ResetColor();
			}
		}
	}

	public class MyLinkedList : ILinkedList
	{
		public Node StartNode { get; set; }
		public Node EndNode { get; set; }

		int Count { get; set; }

		public int GetCount()
		{
			if (StartNode == null && EndNode == null)
				return 0;
			
			int counter = 1;
			var tmpNode = StartNode;
			while (tmpNode.NextNode != StartNode)
			{
				counter++;
				tmpNode = tmpNode.NextNode;
			}

			if (Count != counter)
				Count = counter;

			return Count;
		}

		public void AddNode(int value)
		{
			var newNode = new Node(value);
			if (StartNode == null && EndNode == null)
			{
				StartNode = newNode;
				EndNode = newNode;
			}
			else
			{
				EndNode = new Node(value, EndNode);
			}
			Count++;
		}

		public void AddNodeAfter(Node node, int value)
		{
			if (node == null)
				AddNode(value);
			else
			{
				new Node(value, node);
				EndNode = StartNode.PrevNode;
				Count++;
			}
		}

		public Node FindNode(int searchValue)
		{
			Node tmpNode = EndNode;
			bool isFind = false;

			do
			{
				tmpNode = tmpNode.NextNode;
				if (tmpNode.Value == searchValue)
				{
					isFind = true;
					break;
				}
			}
			while (tmpNode.NextNode != StartNode);

		//	Выбор возврата от требований зависит
		//	return isFind ? tmpNode : null;
			return isFind ? tmpNode : new Node(searchValue);
		}

		public void RemoveNode(int index)
		{
			if(Count < index)
			{
				var tmpNode = StartNode;
				for (int i = 1; i < index; i++)
					tmpNode = tmpNode.NextNode;

				RemoveNode(tmpNode);
			}
			else
				throw new IndexOutOfRangeException();
		}

		public void RemoveNode(Node node)
		{
			if(node != null)
			{
				node.PrevNode.NextNode = node.NextNode;
				node.NextNode.PrevNode = node.PrevNode;
				//	явного удаления не будет пока GC не решит его сам удалить
				//	не уверен, поможет ли это для его удаления
				node.PrevNode = null;
				node.NextNode = null;

				Count--;
			}
		}

		public Node[] ToArray()
		{
			var arr = new Node[GetCount()];

			var tmpNode = StartNode;
			for (int i = 0; i < Count; i++)
			{
				arr[i] = tmpNode;
				tmpNode = tmpNode.NextNode;
			}

			return arr;
		}

		public static Node BinarySearch(Node[] inputArray, int searchValue)
		{
			int min = 0;
			int max = inputArray.Length - 1;
			while (min <= max)
			{
				int mid = (min + max) / 2;		//	O(logN)
				if (searchValue == inputArray[mid].Value)
					return inputArray[mid];
				else if (searchValue < inputArray[mid].Value)
					max = mid - 1;
				else
					min = mid + 1;
			}

			return new Node(searchValue);
		}
	}
}
