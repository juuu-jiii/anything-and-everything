using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDeck
{
    // Node objects should inherit from this class.
    // Modify fields as necessary.
    
    class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }
}
