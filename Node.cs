using System;
using System.Collections.Generic;
class Node : IComparable
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node (char character = '0', int frequency = 0, Node left = null, Node right = null)
    {
        Character = character;
        Frequency = frequency;
        Left = left;
        Right = right;
    }
    // 5 marks
    public int CompareTo (object obj)
    {   
        Node temp = (Node)obj;
        if (Frequency<temp.Frequency){return 1;}
        else if(Frequency==temp.Frequency){return 0;}
        else{return -1;}
    }
}