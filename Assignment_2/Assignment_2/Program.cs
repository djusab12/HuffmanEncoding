//Assignment 2: SHAREEF ODDA (0615519) and DAUD ABDULLATIF JUSAB (0671537)

using System;
using System.Collections.Generic;

//-----------------------------------------------------------------------------

namespace PriorityQueue
{



    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty
        int Size();        // Return the number of items in an instance
    }

    //-----------------------------------------------------------------------------

    public interface IPriorityQueue<T> : IContainer<T> where T : IComparable
    {
        void Add(T item);  // Add an item to a priority queue
        void Remove();     // Remove the item with the highest priority
        T Front();         // Return the item with the highest priority
    }

    //-------------------------------------------------------------------------

    // Priority Queue
    // Implementation:  Binary heap

    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable
    {
        private int capacity;  // Maximum number of items in a priority queue
        private T[] A;         // Array of items
        private int count;     // Number of items in a priority queue

        // Constructor 1
        // Create a priority with maximum capacity of size
        // Time complexity:  O(1)

        public PriorityQueue(int size)
        {
            capacity = size;
            A = new T[size + 1];  // Indexing begins at 1
            count = 0;
        }

        // Constructor 2
        // Create a priority from an array of items
        // Time complexity:  O(n)

        public PriorityQueue(T[] inputArray)
        {
            int i;

            count = capacity = inputArray.Length;
            A = new T[capacity + 1];

            for (i = 0; i < capacity; i++)
            {
                A[i + 1] = inputArray[i];
            }

            BuildHeap();
        }

        // PercolateUp
        // Percolate up an item from position i in a priority queue
        // Time complexity:  O(log n)

        private void PercolateUp(int i)
        {
            int child = i, parent;

            // As long as child is not the root (i.e. has a parent)
            while (child > 1)
            {
                parent = child / 2;
                if (A[child].CompareTo(A[parent]) > 0)
                // If child has a higher priority than parent
                {
                    // Swap parent and child
                    T item = A[child];
                    A[child] = A[parent];
                    A[parent] = item;
                    child = parent;  // Move up child index to parent index
                }
                else
                    // Item is in its proper position
                    return;
            }
        }

        // Add
        // Add an item into the priority queue
        // Time complexity:  O(log n)

        public void Add(T item)
        {
            if (count < capacity)
            {
                A[++count] = item;  // Place item at the next available position
                PercolateUp(count);
            }
        }

        // PercolateDown
        // Percolate down from position i in a priority queue
        // Time complexity:  O(log n)

        private void PercolateDown(int i)
        {
            int parent = i, child;

            // while parent has at least one child
            while (2 * parent <= count)
            {
                // Select the child with the highest priority
                child = 2 * parent;    // Left child index
                if (child < count)  // Right child also exists
                    if (A[child + 1].CompareTo(A[child]) > 0)
                        // Right child has a higher priority than left child
                        child++;

                // If child has a higher priority than parent
                if (A[child].CompareTo(A[parent]) > 0)
                {
                    // Swap parent and child
                    T item = A[child];
                    A[child] = A[parent];
                    A[parent] = item;
                    parent = child;  // Move down parent index to child index
                }
                else
                    // Item is in its proper place
                    return;
            }
        }

        // Remove
        // Remove an item from the priority queue
        // Time complexity:  O(log n)

        public void Remove()
        {
            if (!Empty())
            {
                // Remove item with highest priority (root) and
                // replace it with the last item
                A[1] = A[count--];

                // Percolate down the new root item
                PercolateDown(1);
            }
        }

        // Front
        // Return the item with the highest priority
        // Time complexity:  O(1)

        public T Front()
        {
            if (!Empty())
            {
                return A[1];  // Return the root item (highest priority)
            }
            else
                return default(T);
        }

        // BuildHeap
        // Create a binary heap from a given list
        // Time complexity:  O(n)

        private void BuildHeap()
        {
            int i;

            // Percolate down from the last parent to the root (first parent)
            for (i = count / 2; i >= 1; i--)
            {
                PercolateDown(i);
            }
        }

        // HeapSort
        // Sort and return inputArray
        // Time complexity:  O(n log n)

        public void HeapSort(T[] inputArray)
        {
            int i;

            capacity = count = inputArray.Length;

            // Copy input array to A (indexed from 1)
            for (i = capacity - 1; i >= 0; i--)
            {
                A[i + 1] = inputArray[i];
            }

            // Create a binary heap
            BuildHeap();

            // Remove the next item and place it into the input (output) array
            for (i = 0; i < capacity; i++)
            {
                inputArray[i] = Front();
                Remove();
            }
        }

        // MakeEmpty
        // Reset a priority queue to empty
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            count = 0;
        }

        // Empty
        // Return true if the priority is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Return the number of items in the priority queue
        // Time complexity:  O(1)

        public int Size()
        {
            return count;
        }
    }
}

namespace Huffman
{

    using PriorityQueue;

    class Node : IComparable
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node(char character, int frequency, Node left, Node right)
        {
            Character = character;
            Frequency = frequency;
            Left = left;
            Right = right;

        }
       
        public int CompareTo(Object obj)   
        {
            Node Z = obj as Node;   //Downcasting
            if (Z.Frequency > Frequency)
            {
                return 1;  //returns 1 if the passed frequency is greater
            }
            else if (Z.Frequency < Frequency)
            {
                return -1;   //returns -1 if the passed frequency is smaller
            }
            else
            {
                return 0;     //returns 0 if both frequencies are equal
            }

        }
    }

    class Huffman
    {

        public Node HT; // Huffman tree to create codes and decode text
        private Dictionary<char, string> D = new Dictionary<char, string>(); // Dictionary to encode text
       
        private Dictionary<char, int> B;   //Dictionary to store each character with it's frequency
        public string code = "";    //String to store the encoded bits
        public string decoded = "";   //String to store the text after being decoded



        public Huffman(string S)   //constructor
        {
            B = AnalyzeText(S);   //calling the analyzetext method and passing the string as the parameter. Also stores whatever is returned into B
            Build(B);    //calling the build method and passing B(the return from the analyzetext method) as the parameter
            CreateCodes(HT ,code);   //calling the CreateCodes method 
        }
        
        // Return the frequency of each character in the given text (invoked by Huffman)
        private Dictionary<char, int> AnalyzeText(string S)
        {
            
            Dictionary<char, int> E = new Dictionary<char, int>(); //decided to assign it in the corstructor instead of here // Constructor

            foreach (char p in S)   //looping through each character in string passed
            {
                if (E.ContainsKey(p))   //checks if the dictionary already contains the character
                {
                    E[p]++;     //increases the frequency of the character by 1
                }
                else     // goes through if the dictionary does not contain the character already
                {
                    E.Add(p, 1);   //adds the new character with its frequency equal to 1
                }
            }
            return E;   //returns the final dictionary containing the characters with their frequencies
        }


        
        // Build a Huffman tree based on the character frequencies greater than 0 (invoked by Huffman)
        private void Build(Dictionary<char, int> F)
        {
            PriorityQueue<Node> PQ;
            //PQ = new PriorityQueue<Node>(F.Count);

            //pass in node array to priority queue
            Node[] nodeTemp = new Node[F.Count];
            int count = 0;  //Initiaizing the count to 0
            
            foreach (KeyValuePair<char, int> l in F)  //looping through each keyvalue pair in the dictionary F
            {
                nodeTemp[count] = new Node(l.Key, l.Value, null, null);  //creating a new node array that stores character with their respective frequencies.

                count++;  //increments the count by 1
            }
            PQ = new PriorityQueue<Node>(nodeTemp); //a new priority queue that stores 

            while(PQ.Size()!=1)   //go through the loop if the size of the priorityqueue is not 1
            {
                Node P = PQ.Front();  //referencing to the first node in the priority queue

                PQ.Remove();    //removing the first item in the priority queue

                    Node Q = PQ.Front();   //referencing to the new front item after the the one removed

                    PQ.Remove();    //removing the second item too

                HT = new Node('/', P.Frequency + Q.Frequency, P, Q);   //creating a new node that hold the total frequencies of the two nodes and a link to the left and right nodes.

                PQ.Add(HT);  //adding the new small tree created back to the original tree
            }
 
            HT = PQ.Front();  //Referencing the HT to the root of the priority queue


            


        }

        
        // Create the code of 0s and 1s for each character by traversing the Huffman tree (invoked by Huffman)
        // Store the codes in Dictionary D using the char as the key
        private void CreateCodes(Node HT,string code)
         {
            if (HT.Left == null && HT.Right == null)   //Checks if we are the leaf node 
            {
                D.Add(HT.Character, code);    //adds the char with its generated code into D
            }

            else   //if we are not at the leaf node then the following if statements will be executed
            {
                if (HT.Left != null)   //checks if the parent has a left child node
                {
                  
                    CreateCodes(HT.Left, code+"0");    //rescursively calls CreateCodes method by passing the left child and adding a 0 to the code
                }

                if (HT.Right != null)    //checks if the parent has a right child node
                {
                    
                    CreateCodes(HT.Right, code+"1");    //rescursively calls CreateCodes method by passing the right child and adding a 1 to the code
                }
            }
            

        }
        
        // Encode the given text and return a string of 0s and 1s
         public string Encode(string S)
         {
            string Bits = "";   
           
            
            if(S.Length == 1)   //checks if the there is only one character in the string
            {
                Bits="0";   //assigns bits to be '0'
            }

            else if(D.Count==1)   //checks if the dictionary contains 1 node
            {
                foreach (char p in S)   //iterates for each character in the string
                {
                    Bits += "0";   //assigning 0 for each of the characters
                }
            }
            
            else   //if the length of the string is more than 1
            {
                foreach(char l in S)    //looping through each character in S
                {
                    if(D.ContainsKey(l))    //checks if that character exists in the dictionary
                    {
                        Bits = Bits + D[l];    //adds the bits of the character to the encoded string
                    }
                   
                }
            }
            
            return Bits;    //returns the encoded bits 
         }
         
         // Decode the given string of 0s and 1s and return the original text
         public string Decode(string S)
         {
            string text = "";  //initializing the text to null
            Node Temp = HT;     //initializing the Temp node to the root
            
            if (S.Length == 1 )    //checks if only one character is passed
            {
                text = Temp.Character.ToString();    //puts the character at the Temp node into text
            }

            else if(D.Count == 1) //checks if the dictionary contains 1 node
            {
                foreach(char t in S) //iterates for each character in the string
                {
                    if(t=='0')  //checks if the character is zero
                    {
                        text= text + Temp.Character.ToString(); //combines the actual input together
                    }
                }
            }

            else
            {
              foreach(char l in S)    //checks for each bit in the string
              {
                if(l=='0')    //checks if the current bit in the string is 0
                {
                        Temp = Temp.Left;   //moves the Temp node to the left child
                        if(Temp.Left==null&& Temp.Right==null)   //checks if we are at the leaf node
                        {
                            text = text + Temp.Character.ToString();   //adds the character into the text
                            Temp = HT;   //returns back the Temp node to the root node
                        }
                }
                else
                {
                        Temp = Temp.Right;   //moves the Temp node to the right child
                        if (Temp.Left == null && Temp.Right == null)
                        {
                            text = text + Temp.Character.ToString();
                            Temp = HT;
                        }
                }       
              }

            }
            return text;    //returns the decoded text back.
         }

        static void Main(string[] args)
            {

            string message;
            Console.WriteLine("Please enter a text message that you would like to encode");
            message = Console.ReadLine();
            Console.Write("the input is: " + message);
        //Huffman F = new Huffman(message);
        //    F.code = F.Encode(message);
        //    Console.WriteLine("\nThe message after encoding is -> {0}\n", F.code);
        //    F.decoded = F.Decode(F.code);
        //    Console.WriteLine("After decoding, the message is -> {0}\n", F.decoded);

        //    Console.ReadLine();
               
            }
        }
    }