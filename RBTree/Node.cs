using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree
{
    class Node
    {


        public Color colour;
        public Node left;
        public Node right;
        public Node parent;
        public int data;

        public Node(int data) { this.data = data; }
        public Node(Color colour) { this.colour = colour; }
        public Node(int data, Color colour)
        {
            this.data = data; this.colour = colour;
        }
    }

}

