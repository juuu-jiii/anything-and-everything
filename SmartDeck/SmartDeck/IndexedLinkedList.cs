using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    // This class represents the collection of Node objects within the Indexed Linked List.
    
    class IndexedLinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; set; }
        public int Count { get; set; }

        /// <summary>
        /// Adds a specified Node to the end of the IndexedLinkedList.
        /// </summary>
        /// <param name="newNode">
        /// The Node object to be appended.
        /// </param>
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            
            // LL is empty. Make newNode the new Head and Tail.
            if (Count == 0)
            {
                Head = newNode;
            }
            // Else append. Make newNode the new Tail.
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
            }

            Tail = newNode;
            Count++;
        }

        /// <summary>
        /// Adds a new Node object to the 
        /// </summary>
        /// <param name="newNode"></param>
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);

            newNode.Next = Head;
            Head.Previous = newNode;
            Head = newNode;

            Count++;
        }

        /// <summary>
        /// Helper method to determine whether a specified index is valid.
        /// </summary>
        /// <param name="index">
        /// The index whose validity is to be checked for.
        /// </param>
        /// <returns>
        /// Returns true if the index is valid, and false otherwise.
        /// </returns>
        public bool IsValidIndex(int index)
        {
            if (index < 0)
            {
                return false;
            }
            else if (index >= Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Indexer property for the IndexedLinkedList.
        /// </summary>
        /// <param name="index">
        /// The index to retrieve or alter.
        /// </param>
        public Node<T> this[int index]
        {
            get
            {
                if (!IsValidIndex(index))
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    Node<T> currentNode = Head;

                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }

                    return currentNode;
                }
            }
            set
            {
                if (!IsValidIndex(index))
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    Node<T> currentNode = Head;

                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }

                    currentNode = value;
                }
            }
        }

        /// <summary>
        /// Inserts a specified Node object into the IndexedLinkedList at a specified index.
        /// </summary>
        /// <param name="newNode">
        /// The Node obect to be inserted.
        /// </param>
        /// <param name="index">
        /// The index to insert the Node object at.
        /// </param>
        public void Insert(T data, int index)
        {
            // Allow use of other methods for ease of access.
            if (index == Count - 1)
            {
                AddLast(data);
            }
            else if (index == 0)
            {
                AddFirst(data);
            }
            else
            {
                // Leverage the indexer property.
                Node<T> currentNode = this[index];

                Node<T> newNode = new Node<T>(data);

                newNode.Next = currentNode;
                newNode.Previous = currentNode.Previous;
                currentNode.Previous = newNode;
                newNode.Previous.Next = newNode;

                Count++;
            }
        }

        /// <summary>
        /// Removes a Node object at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index to remove from.
        /// </param>
        public void RemoveAt(int index)
        {
            // Case 1: first element is removed.
            if (index == 0)
            {
                Head = Head.Next;
                Head.Previous = null;
            }
            // Case 2: last element is removed.
            else if (index == Count - 1)
            {
                Tail = Tail.Previous;
                Tail.Next = null;
            }
            // Case 3: element from the middle is removed.
            else
            {
                // Leverage the indexer property.
                Node<T> currentNode = this[index];

                currentNode.Previous.Next = currentNode.Next;
                currentNode.Next.Previous = currentNode.Previous;
                currentNode.Next = null;
                currentNode.Previous = null;
            }

            Count--;
        }

        /// <summary>
        /// Removes and returns the current Head.
        /// </summary>
        /// <returns>
        /// Returns the Head as a Node object.
        /// </returns>
        private Node<T> Pop()
        {
            Node<T> returnedHead = Head;
            RemoveAt(0);
            return returnedHead;
        }
    }
}
