using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedLinkedList
{
    // Node objects should inherit from this class.
    
    class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node()
        {
            Next = null;
            Previous = null;
        }
    }
}
