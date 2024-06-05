using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private class Node
    {
        public T Value { get; set; }
        public int Priority { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(T value, int priority)
        {
            Value = value;
            Priority = priority;
            Next = null;
            Previous = null;
        }
    }

    private Node Head { get; set; }
    private Node Tail { get; set; }
    private int length = 0;

    public void PriorityEnqueue(T value, int priority)
    {
        Node newNode = new Node(value, priority);

        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else if (priority < Head.Priority)
        {
            newNode.Next = Head;
            Head.Previous = newNode;
            Head = newNode;
        }
        else
        {
            Node iterator = Head;
            while (iterator.Next != null && iterator.Next.Priority <= priority)
            {
                iterator = iterator.Next;
            }
            newNode.Next = iterator.Next;
            if (iterator.Next != null)
            {
                iterator.Next.Previous = newNode;
            }
            iterator.Next = newNode;
            newNode.Previous = iterator;
            if (newNode.Next == null)
            {
                Tail = newNode;
            }
        }

        length++;
    }

    public T PriorityDequeue()
    {
        if (Head == null)
        {
            throw new System.NullReferenceException("Queue is empty");
        }
        else
        {
            Node highestPriorityNode = Head;
            Head = Head.Next;
            if (Head != null)
            {
                Head.Previous = null;
            }
            else
            {
                Tail = null;
            }
            length--;
            return highestPriorityNode.Value;
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