using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class SimplyLinkedList<T>
{
    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
            Next = null;
        }
    }

    Node Head;
    public int length = 0;

    public void InsertNodeAtStart(T value)
    {
        if (Head == null)
        {
            Node newNode = new Node(value);
            Head = newNode;
            length++;
        }
        else
        {
            Node newNode = new Node(value);
            newNode.Next = Head;
            Head = newNode;
            length++;
        }
    }

    public void InsertNodeAtEnd(T value)
    {
        if (Head == null)
        {
            InsertNodeAtStart(value);
        }
        else
        {
            Node last = Head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            Node newNode = new Node(value);
            last.Next = newNode;
            length++;
        }
    }

    public void InsertNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            InsertNodeAtStart(value);
        }
        else if (position == length - 1)
        {
            InsertNodeAtEnd(value);
        }
        else if (position >= length)
        {
            Debug.Log("No existe esa posición.");
        }
        else
        {
            Node previous = Head;
            int iterator = 0;
            while (iterator < position - 1)
            {
                previous = previous.Next;
                iterator++;
            }
            Node next = previous.Next;
            Node newNode = new Node(value);
            previous.Next = newNode;
            newNode.Next = next;
            length++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (Head == null)
        {
            Debug.Log("No se puede modificar: la lista está vacía.");
        }
        else
        {
            Head.Value = value;
        }
    }

    public void ModifyAtEnd(T value)
    {
        if (Head == null)
        {
            ModifyAtStart(value);
        }
        else
        {
            Node last = Head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            last.Value = value;
        }
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == length - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position >= length)
        {
            Debug.Log("No existe esa posición.");
        }
        else
        {
            Node nodePosition = Head;
            int iterator = 0;
            while (iterator < position)
            {
                nodePosition = nodePosition.Next;
                iterator++;
            }
            nodePosition.Value = value;
        }
    }

    public T ObtainNodeAtStart()
    {
        if (Head == null)
        {
            throw new System.Exception("La lista está vacía.");
        }
        else
        {
            return Head.Value;
        }
    }

    public T ObtainNodeAtEnd()
    {
        if (Head == null)
        {
            return ObtainNodeAtStart();
        }
        else
        {
            Node last = Head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            return last.Value;
        }
    }

    public T ObtainNodeAtPosition(int position)
    {
        if (position == 0)
        {
            return ObtainNodeAtStart();
        }
        else if (position == length - 1)
        {
            return ObtainNodeAtEnd();
        }
        else if (position >= length)
        {
            throw new System.Exception("No existe ese nodo en la lista.");
        }
        else
        {
            Node nodePosition = Head;
            int iterator = 0;
            while (iterator < position)
            {
                nodePosition = nodePosition.Next;
                iterator++;
            }
            return nodePosition.Value;
        }
    }

    public void DeleteAtStart()
    {
        if (Head == null)
        {
            throw new System.Exception("No se puede eliminar: la lista está vacía.");
        }
        else
        {
            Node newHead = Head.Next;
            Head.Next = null;
            Head = newHead;
            length--;
        }
    }

    public void DeleteAtEnd()
    {
        if (Head == null)
        {
            DeleteAtStart();
        }
        else
        {
            Node previousLastNode = Head;
            while (previousLastNode.Next.Next != null)
            {
                previousLastNode = previousLastNode.Next;
            }
            Node lastNode = previousLastNode.Next;
            lastNode = null;
            previousLastNode.Next = null;
            length--;
        }
    }

    public void DeleteNodeAtPosition(int position)
    {
        if (position == 0)
        {
            DeleteAtStart();
        }
        else if (position == length - 1)
        {
            DeleteAtEnd();
        }
        else if (position >= length)
        {
            Debug.Log("No existe esa posición.");
        }
        else
        {
            Node previous = Head;
            int iterator = 0;
            while (iterator < position - 1)
            {
                previous = previous.Next;
                iterator++;
            }
            Node next = previous.Next.Next;
            Node nodePosition = previous.Next;
            nodePosition.Next = null;
            nodePosition = null;
            previous.Next = null;
            previous.Next = next;
            length--;
        }
    }

    public void PrintAllNodes()
    {
        Node tmp = Head;
        StringBuilder sb = new StringBuilder();
        while (tmp != null)
        {
            sb.Append(tmp.Value).Append(", ");
            tmp = tmp.Next;
        }
        Debug.Log(sb.ToString());
    }


    public void PrintAllNodesToDebug()
    {
        Node tmp = Head;
        StringBuilder sb = new StringBuilder();
        while (tmp != null)
        {
            sb.Append(tmp.Value).Append(", ");
            tmp = tmp.Next;
        }
        Debug.Log(sb.ToString());
    }

    public bool FindValue(T value)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Value.Equals(value))
                return true;
            current = current.Next;
        }
        return false;
    }
}
