using GeekBrainsTests;
using System;

namespace Lesson2
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}

	public class MyLinkedList : ILinkedList
	{
		public Node StartNode { get; set; }
		public Node EndNode { get; set; }

		int Count { get; set; }

		public int GetCount() => Count;

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
				newNode.NextNode = StartNode;
				newNode.PrevNode = EndNode;
				EndNode.NextNode = newNode;
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
			return isFind ? tmpNode : null;
		//	return isFind ? tmpNode : new Node(searchValue);
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
	}
}
