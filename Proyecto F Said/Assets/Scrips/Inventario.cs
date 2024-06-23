using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node Head { get; set; }
    private Node Tail { get; set; }
    private int length = 0;

    public void Enqueue(T value)
    {
        Node newNode = new Node(value);

        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            Tail = newNode;
        }

        length++;
    }

    public T Dequeue()
    {
        if (Head == null)
        {
            throw new System.NullReferenceException("Queue is empty");
        }
        else
        {
            Node dequeuedNode = Head;
            Head = Head.Next;
            length--;

            if (Head == null)
            {
                Tail = null;
            }

            return dequeuedNode.Value;
        }
    }

    public int Count()
    {
        return length;
    }

    public List<T> GetAllValues()
    {
        List<T> values = new List<T>();
        Node current = Head;
        while (current != null)
        {
            values.Add(current.Value);
            current = current.Next;
        }
        return values;
    }
}