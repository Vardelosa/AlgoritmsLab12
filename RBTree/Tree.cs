using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree
{
    class Tree
    {
        private Node root;
        public Tree() { }
        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            Y.parent = X.parent;//link X's parent to Y
            if (X.parent == null)
            {
                root = Y;
            }
            else if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X;
            X.parent = Y;

        }
        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            X.parent = Y.parent;
            
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;
            Y.parent = X;
        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }
        public Node Find(int key)
        {
            bool isFound = false;
            Node temp = root;
            Node item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                if (key > temp.data)
                {
                    temp = temp.right;
                }
                if (key == temp.data)
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        public void Insert(int item)
        {
            Node newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.colour = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data < Y.data)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = Color.Red;//colour the new node red
            InsertFixUp(newItem);//call method to check for violations and fix
        }
        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}, {1}) \n", current.data, current.colour);
                InOrderDisplay(current.right);
            }
        }
        private void Traverse(Node node, ref string info, int n = 0)
        {
            if (node == null)
                return;

            Traverse(node.right, ref info, n + 5);

            string temp = "";
            for (int i = 0; i < n; ++i)
                temp += " ";
            info += temp + node.data +"/"+ node.colour+ "\n";

            Traverse(node.left, ref info, n + 5);
        }
        public override string ToString()
        {
            string info = "";
            Traverse(root, ref info);

            return info;
        }
        private void InsertFixUp(Node item)
        {
            //Checks Red-Black Tree properties
            while (item != root && item.parent.colour == Color.Red)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    Node Y = item.parent.parent.right;
                    if (Y != null && Y.colour == Color.Red)//Case 1: uncle is red
                    {
                        item.parent.colour = Color.Black;
                        Y.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        RightRotate(item.parent.parent);
                    }

                }
                else
                {
                    //mirror image of code above
                    Node X = null;

                    X = item.parent.parent.left;
                    if (X != null && X.colour == Color.Black)//Case 1
                    {
                        item.parent.colour = Color.Red;
                        X.colour = Color.Red;
                        item.parent.parent.colour = Color.Black;
                        item = item.parent.parent;
                    }
                    else //Case 2
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        LeftRotate(item.parent.parent);

                    }

                }
                root.colour = Color.Black;//re-colour the root black as necessary
            }
        }
        public void Delete(int key)
        {
            //first find the node in the tree to delete and assign to item pointer/reference
            Node item = Find(key);
            Node X = null;
            Node Y = null;
            Node Z = null;

            while (item != null)
            {
                if (item.data == key)
                {
                    Z = item;
                }
                if (item.data <= key)
                {
                    item = item.right;
                }
                else
                {
                    item = item.left;
                }

            }
            if (Z == null)
            {
                Console.WriteLine("Element with this key dont exist");
            }
            Y = Z;
            Color y_original_color = Y.colour;
            if (Z.left == null)
            {
                X = Z.right;
                rbTransplant(Z, Z.left);
            }
            else if (Z.right == null)
            {
                X = Z.left;
                rbTransplant(Z, Z.left);
            }
            else
            {
                Y = Minimum(Z.left);
                y_original_color = Y.colour;
                X = Y.right;
                if (Y.parent == Z)
                {
                    X.parent = Y;

                }
                else
                {
                    rbTransplant(Y, Y.right);
                    Y.right = Z.right;
                    Y.right.parent = Y;

                }
                rbTransplant(Z, Y);
                Y.left = Z.left;
                Y.left.parent = Y;
                Y.colour = Z.colour;
            }
            if(y_original_color == Color.Black)
            {
                DeleteFixUp(X);
            }
            

        }

        public void rbTransplant(Node u, Node v)
        {
            if (u.parent == null)
            {
                root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            if (v != null)
            {

                v.parent = u.parent;
            }
        }
        /// <summary>
        /// Checks the tree for any violations after deletion and performs a fix
        /// </summary>
        /// <param name="X"></param>
        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.colour == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black; //case 1
                        X.parent.colour = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == Color.Black && W.right.colour == Color.Black)
                    {
                        W.colour = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else
                    {
                        if (W.right.colour == Color.Black)
                        {
                            W.left.colour = Color.Black; //case 3
                            W.colour = Color.Red; //case 3
                            RightRotate(W); //case 3
                            W = X.parent.right; //case 3
                        }
                        W.colour = X.parent.colour; //case 4
                        X.parent.colour = Color.Black; //case 4
                        W.right.colour = Color.Black; //case 4
                        LeftRotate(X.parent); //case 4
                        X = root; //case 4
                    }
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black;
                        X.parent.colour = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == Color.Black && W.left.colour == Color.Black)
                    {
                        W.colour = Color.Black;
                        X = X.parent;
                    }
                    else
                    {
                        if (W.left.colour == Color.Black)
                        {
                            W.right.colour = Color.Black;
                            W.colour = Color.Red;
                            LeftRotate(W);
                            W = X.parent.left;
                        }
                        W.colour = X.parent.colour;
                        X.parent.colour = Color.Black;
                        W.left.colour = Color.Black;
                        RightRotate(X.parent);
                        X = root;
                    }
                }
            }
                X.colour = Color.Black;
        }
        private Node Minimum(Node X)
        {
            while (X.left != null)
            {
                X = X.left;
            }
            return X.parent.right;
        }
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }


        }
    }
}

