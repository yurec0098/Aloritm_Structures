namespace GeekBrainsTests
{
	public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }

        public Node(int value)
		{
            Value = value;
            NextNode = this;
            PrevNode = this;
		}
        public Node(int value, Node prev)
		{
            Value = value;
            PrevNode = prev;
            NextNode = prev.NextNode;

            prev.NextNode.PrevNode = this;
            prev.NextNode = this;
		}
    }

    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList
    {
        int GetCount();             // возвращает количество элементов в списке
        void AddNode(int value);    // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value);    // добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        Node FindNode(int searchValue); // ищет элемент по его значению
    }

}
