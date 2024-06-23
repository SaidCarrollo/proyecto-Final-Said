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
        public SimplyLinkedListNode ListNode { get; set; }
        public List<TreeNode> Children { get; set; }

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
        SimplyLinkedListNode newNode = InsertNodeAtEnd(value);
        TreeNode treeNode = new TreeNode(newNode);

        if (root == null)
        {
            root = treeNode;
        }
        else
        {
            TreeNode fatherNode = FindTreeNode(root, fatherValue);
            if (fatherNode != null)
            {
                fatherNode.Children.Add(treeNode);
            }
            else
            {
                Debug.LogWarning("Padre no encontrado en el árbol.");
            }
        }
    }

    private TreeNode FindTreeNode(TreeNode currentNode, T value)
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(currentNode);

        while (stack.Count > 0)
        {
            TreeNode node = stack.Pop();

            if (node.ListNode.Value.Equals(value))
            {
                return node;
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                stack.Push(node.Children[i]);
            }
        }

        return null;
    }

    public T FindFinal(TreeNode currentNode, string condition)
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(currentNode);

        while (stack.Count > 0)
        {
            TreeNode node = stack.Pop();

            if (node.ListNode.Value.Equals(condition))
            {
                return node.ListNode.Value;
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                stack.Push(node.Children[i]);
            }
        }

        return default(T);
    }

    public TreeNode GetRoot()
    {
        return root;
    }
}