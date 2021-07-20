using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson6
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}

	public class Node //Вершина
	{
		public char Value { get; set; } = 'A';
		public List<Edge> Edges { get; set; } = new List<Edge>(); //исходящие связи
	}

	public class Edge //Ребро
	{
		public int Weight { get; set; } //вес связи
		public Node Node { get; set; } // узел, на который связь ссылается

		public Edge() { }
		public Edge(int weight, Node node)
		{
			Weight = weight;
			Node = node;
		}
	}

	public class Graph
	{
		public List<Node> Nodes { get; set; } = new List<Node>();

		//	https://prnt.sc/1dcxdrc
		static public Graph Init()
		{
			var graf = new Graf();

			for (int i = 0; i < 8; i++)
				graf.Nodes.Add(new Node() { Value = (char)(i + 65)});

			if (graf.GetNodeByValue('A') is Node A)
			{
				A.Edges.Add(new Edge(3, graf.GetNodeByValue('B')));
				A.Edges.Add(new Edge(2, graf.GetNodeByValue('C')));
			}
			if (graf.GetNodeByValue('B') is Node B)
			{
				B.Edges.Add(new Edge( 3, graf.GetNodeByValue('A')));
				B.Edges.Add(new Edge( 9, graf.GetNodeByValue('C')));
				B.Edges.Add(new Edge(10, graf.GetNodeByValue('D')));
			}
			if (graf.GetNodeByValue('C') is Node C)
			{
				C.Edges.Add(new Edge(2, graf.GetNodeByValue('A')));
				C.Edges.Add(new Edge(9, graf.GetNodeByValue('B')));
				C.Edges.Add(new Edge(8, graf.GetNodeByValue('D')));
				C.Edges.Add(new Edge(5, graf.GetNodeByValue('E')));
			}
			if (graf.GetNodeByValue('D') is Node D)
			{
				D.Edges.Add(new Edge(10, graf.GetNodeByValue('B')));
				D.Edges.Add(new Edge(8, graf.GetNodeByValue('C')));
				D.Edges.Add(new Edge(3, graf.GetNodeByValue('E')));
				D.Edges.Add(new Edge(1, graf.GetNodeByValue('F')));
			}
			if (graf.GetNodeByValue('E') is Node E)
			{
				E.Edges.Add(new Edge(5, graf.GetNodeByValue('C')));
				E.Edges.Add(new Edge(3, graf.GetNodeByValue('D')));
				E.Edges.Add(new Edge(3, graf.GetNodeByValue('F')));
			}
			if (graf.GetNodeByValue('F') is Node F)
			{
				F.Edges.Add(new Edge(1, graf.GetNodeByValue('D')));
				F.Edges.Add(new Edge(3, graf.GetNodeByValue('E')));
			}
			
			return graf;
		}

		public Node GetNodeByValue(char val) =>
			Nodes.FirstOrDefault(x => x.Value == val);

		public List<Node> GetMinPath(char start, char end)
		{
			var list = new List<Node>();
			var edges = Nodes.Select(x => new Edge(x.Value == start ? 0 : int.MaxValue, x)).ToList();

			foreach(var n in Nodes)
			{
				// start node...
			}

			return list;
		}
	}
}
