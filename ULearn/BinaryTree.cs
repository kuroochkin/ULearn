using System.Collections;

namespace BinaryTree;

public class Node<T>
{
	public T Left { get; set; }
	public T Right { get; set; }

	public Node<T> Search()
	{

	}
}

public class BinaryTree<T> : IEnumerable<T>
{
	public Node<T> Value { get; set; }

	public List<T> Values;

	public BinaryTree()
	{
		Values = new List<T>();
	}

	public void Add(T value)
	{
		Values.Add(value);
	}

	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	Node search(x : Node, k : T):
   if x == null or k == x.key
      return x
   if k<x.key
      return search(x.left, k)
   else
      return search(x.right, k)
}
