using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    // Classes implementing the indexed linked list should inherit from this class.
    
    class IndexedLinkedList
    {
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public int Count { get; private set; }

        /// <summary>
        /// Adds a specified Node to the end of the IndexedLinkedList.
        /// </summary>
        /// <param name="newNode">
        /// The Node object to be appended.
        /// </param>
        public void AddLast(Node newNode)
        {
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
        public void AddFirst(Node newNode)
        {
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
        public Node this[int index]
        {
            get
            {
                if (!IsValidIndex(index))
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    Node currentNode = Head;

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
                    Node currentNode = Head;

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
        public void Insert(Node newNode, int index)
        {
            // Allow use of other methods for ease of access.
            if (index == Count - 1)
            {
                AddLast(newNode);
            }
            else if (index == 0)
            {
                AddFirst(newNode);
            }
            else
            {
                // Leverage the indexer property.
                Node currentNode = this[index];

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
                Node currentNode = this[index];

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
        private Node Pop()
        {
            Node returnedHead = Head;
            RemoveAt(0);
            return returnedHead;
        }
    }
}
