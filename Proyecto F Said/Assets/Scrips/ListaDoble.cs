using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoubleLinkedList<T>
{
    public class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    private Node head;
    private int length = 0;

    public void InsertAtEnd(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node lastNode = GetLastNode();
            lastNode.Next = newNode;
            newNode.Previous = lastNode;
        }
        length++;
    }

    public void DeleteAtEnd()
    {
        if (head == null)
        {
            Debug.LogError("List is empty");
            return;
        }

        Node lastNode = GetLastNode();
        Node newLastNode = lastNode.Previous;
        if (newLastNode != null)
        {
            newLastNode.Next = null;
        }
        lastNode.Previous = null;
        length--;

        if (lastNode == head)
        {
            head = null;
        }
    }

    private Node GetLastNode()
    {
        Node lastNode = head;
        while (lastNode.Next != null)
        {
            lastNode = lastNode.Next;
        }
        return lastNode;
    }

    public int Length()
    {
        return length;
    }

    public Node GetNodeAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Position out of bounds");
            return null;
        }

        Node nodePosition = head;
        int iterator = 0;
        while (iterator < position)
        {
            nodePosition = nodePosition.Next;
            iterator++;
        }
        return nodePosition;
    }
}