using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree<T>
{
    public class SimplyLinkedListNode
    {
        public T Value { get; set; }
        public SimplyLinkedListNode Next { get; set; }

        public SimplyLinkedListNode(T value)
        {
            this.Value = value;
            Next = null;
        }
    }

    public class TreeNode
    {
        public SimplyLinkedListNode ListNode { get; set; } // Referencia al nodo de la lista enlazada
        public List<TreeNode> Children { get; set; } // Hijos del nodo en el árbol

        public TreeNode(SimplyLinkedListNode listNode)
        {
            ListNode = listNode;
            Children = new List<TreeNode>();
        }
    }

    private TreeNode root;
    private SimplyLinkedListNode head;
    private int length = 0;

    public Tree()
    {
        root = null;
        head = null;
    }

    public void InsertNodeAtStart(T value)
    {
        SimplyLinkedListNode newNode = new SimplyLinkedListNode(value);
        newNode.Next = head;
        head = newNode;
        length++;
    }

    public SimplyLinkedListNode InsertNodeAtEnd(T value)
    {
        SimplyLinkedListNode newNode = new SimplyLinkedListNode(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            SimplyLinkedListNode last = head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            last.Next = newNode;
        }
        length++;
        return newNode;
    }

    public void AddNode(T value, T fatherValue)
    {
        SimplyLinkedListNode newNode = InsertNodeAtEnd(value); // Inserta el nodo en la lista
        TreeNode treeNode = new TreeNode(newNode); // Crea un nuevo nodo para el árbol

        if (root == null)
        {
            root = treeNode;
        }
        else
        {
            TreeNode fatherNode = FindTreeNode(root, fatherValue); // Encuentra el padre en el árbol
            if (fatherNode != null)
            {
                fatherNode.Children.Add(treeNode); // Añade el nuevo nodo como hijo del padre
            }
            else
            {
                Debug.LogWarning("Padre no encontrado en el árbol.");
            }
        }
    }

    private TreeNode FindTreeNode(TreeNode currentNode, T value)
    {
        if (currentNode.ListNode.Value.Equals(value))
        {
            return currentNode;
        }
        foreach (TreeNode child in currentNode.Children)
        {
            TreeNode foundNode = FindTreeNode(child, value);
            if (foundNode != null)
                return foundNode;
        }
        return null;
    }
}