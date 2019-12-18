using System;
using System.Collections.Generic;
class Huffman
{
    private Node HT;
    private Dictionary<char,string> D = new Dictionary<char, string>();
    // Huffman tree to create codes and decode text
    // Dictionary to encode text
    public char[] possibleCharacters = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',' '};
    public Huffman ( string S ) //constructor, takes single string
    {
        Build(AnalyzeText(S)); //analyzes the text then build the binary tree based on the frequencies
        CreateCodes(HT); //recursive function
    }

    // 15 marks
    // Return the frequency of each character in the given text (invoked by Huffman)
    private int[] AnalyzeTextOLD ( string S ) //use hash table to lower big O
    {
        int[] ret = new int[possibleCharacters.Length];
        for (int x = 0; x<possibleCharacters.Length; x++)
        {
            int lettercount = 0;
            foreach (char c in S){if (c == possibleCharacters[x]) lettercount++;}
            ret[x] = (lettercount);
        }
        return ret;
    }
    private int[] AnalyzeText (string S)
    {
        int [] ret = new int[possibleCharacters.Length]; //  initialize return array length of possible character choices
        int ascii; //  ascii value holder
        foreach (char c in S) // run through all characters in string
        {
            ascii = Convert.ToInt32(c); // convert char to ascii code and save as ascii
            if (ascii > 90){ascii-=6;} // if ascii code is greater than 90, subtract 6
                                       // this is because values 91-97 are not used ([,\,],^,_,`)
            if (ascii == 32){ascii = 52;} // if ascii code is 32 (space) set to space index in return array
            else {ascii -= 65;} // otherwise, subtract ascii value of A (lowest value in possible characters)
            ret[ascii]+=1; // add one occurrence to ascii letter index of return array
        }
        return ret;
    }
    // 20 marks
    // Build a Huffman tree based on the character frequencies greater than 0 (invoked by Huffman)
    private void Build (int[] F )
    {
        PriorityQueue.PriorityQueue<Node> PQ = new PriorityQueue.PriorityQueue<Node>(F.Length);
        for (int x = 0; x < F.Length; x++) // loop to create the leaf nodes
        {
            if (F[x]>0) // checks to make sure there is at least one occurrence
            {
                Node temp = new Node(possibleCharacters[x], F[x], null, null); // leaf nodes have no left and right nodes
                PQ.Add(temp); //add leaf nodes to priority queue
            }
        }
        while (PQ.Size()>2)
        {
            Node temp = new Node(); //new empty node
            int freq = PQ.Front().Frequency; //store left side frequency
            temp.Left = PQ.Front(); // store left side node
            PQ.Remove(); //remove left side node from priority queue

            temp.Frequency = freq + PQ.Front().Frequency; //store left side freq plus right side freq
            temp.Right = PQ.Front(); //store right side node
            PQ.Remove(); //remove right side node from priority queue

            PQ.Add(temp); //add new sub tree to priority queue
        }
        if (PQ.Size() == 2)
        {
            HT = new Node(); //initialize head node as empty
            int freq = PQ.Front().Frequency; //store left node frequency
            HT.Left = PQ.Front(); // store left node
            PQ.Remove(); // remove node from priority queue

            HT.Frequency = freq + PQ.Front().Frequency; //store both frequencies in head node
            HT.Right = PQ.Front(); //store right node
            PQ.MakeEmpty(); //remove node from priority queue (make sure its empty)
        }
        else //Special case, only one node in the priority queue
        {
            HT = new Node(); // head node is set to only have one leaf node and freq is that of the leaf node
            HT.Frequency = PQ.Front().Frequency; // set head node freq as leaf node freq
            HT.Left = PQ.Front(); // if there is only one node, it becomes the head node
            PQ.MakeEmpty(); //empty priority queue
        }
    }
    // 20 marks
    // Create the code of 0s and 1s for each character by traversing the Huffman tree (invoked by Huffman)
    private void CreateCodes (Node n, string prefix = "")
    {
        if (n.Character!='0')
        {
            D.Add(n.Character, prefix); //char value 0 means null in this case
            // REMOVE THIS AT SOME POINT
            //System.Console.WriteLine("Character: {0} | Code: {1}", n.Character, prefix);
        }
        else
        {
            CreateCodes(n.Left, prefix + "0"); //left adds a 0
            if (n.Right!=null){ // check if there is a right node (special case with only one entry)
                CreateCodes(n.Right, prefix + "1"); //right adds a 1
            }
        }

    }
    // 10 marks
    // Encode the given text and return a string of 0s and 1s
    public string Encode (string S)
    {

        string ret = ""; // initialize ret string
        foreach (char c in S){ret += D[c];} // for each character in string as dictionary key, add its dictionary value to return string
        return ret; // return return string
    }
    // 10 marks
    // Decode the given string of 0s and 1s and return the original text
    public string DecodeOld ( string S ) //make new reverse dictionary to search by value
    {
        Node navnode = HT;
        string ret = "";
        foreach (char c in S)
        {
            if (navnode.Character!='0')
            {
                ret+=navnode.Character.ToString();
                navnode = HT;
            }
            if (c == '0'){navnode = navnode.Left;}
            else if (c == '1'){navnode = navnode.Right;}

        }
        if (navnode.Character!='0'){ret+=navnode.Character.ToString();}
        return ret;
    }
    public string Decode (string S)
    {
        Dictionary<string,char> R = new Dictionary<string, char>(); // new empty dictionary for reversed dictionary D
        foreach(KeyValuePair<char, string> entry in D) // loop through key value entries in D
        {
            R.Add(entry.Value, entry.Key);  // add entries with key and value reversed
        }
        string ret = ""; // return string initialize (blank)
        string code = ""; // code holding string intialize (blank)
        foreach (char c in S) // for each of the characters (0,1) in the encoded string
        {
            code += c.ToString(); // add character (0,1) to code string
            if (R.ContainsKey(code)) // check to see if code string is a key in the reverse dictionary R
            {
                ret += R[code]; // add reverse dictionary R value at key (code) to return string (decoded string)
                code = ""; // reset the code to blank for new characters to be added
            }
        }
        return ret; // return finished decoded string
    }
}
