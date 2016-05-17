using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class Program
    {

        static int timesCalled;
        static void Main(string[] args)
        {
            TestWordSegment();
            //TestConvertToInt();

            //TestNodeWithEqualNode();
           
            Console.ReadKey();
        }

        private static void TestWordSegment()
        {
            WordSegment.dict = new HashSet<string>() { "at", "a" };

            string input = "ata";

            Console.WriteLine($"Dict: {string.Join(",", WordSegment.dict)}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Able to find in dict? {WordSegment.TryToSegment(input)}");

            WordSegment.dict.Add("tap");
            WordSegment.dict.Add("tape");
            WordSegment.dict.Add("eat");
            WordSegment.dict.Add("peat");
            WordSegment.dict.Add("pea");

            input = "atap";

            Console.WriteLine($"Dict: {string.Join(",", WordSegment.dict)}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Able to find in dict? {WordSegment.TryToSegment(input)}");


            input = "atapepeate";

            Console.WriteLine($"Dict: {string.Join(",", WordSegment.dict)}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Able to find in dict? {WordSegment.TryToSegment(input)}");


            input = "tape";

            Console.WriteLine($"Dict: {string.Join(",", WordSegment.dict)}");
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Able to find in dict? {WordSegment.TryToSegment(input)}");
        }

        private static void TestMinsweeper()
        {

            Minesweeper game = new Minesweeper(10);
            game.StartGame();
        }

        private static void TestNodeWithEqualNode()
        {
            NodeWithEqualNodes node = new NodeWithEqualNodes(5);
            NodeWithEqualNodes.Insert( node, 3);
            NodeWithEqualNodes.Insert( node, 2);
            NodeWithEqualNodes.Insert( node, 4);
            NodeWithEqualNodes.Insert( node, 1);
            NodeWithEqualNodes.Insert( node, 8);
            NodeWithEqualNodes.Insert( node, 7);
            NodeWithEqualNodes.Insert(node, 7);
            NodeWithEqualNodes.Insert(node, 7);
            NodeWithEqualNodes.Insert(node, 9);
            NodeWithEqualNodes.Insert(node, 10);

            Console.WriteLine("insert done");
            // Console.ReadKey();

            var parent = NodeWithEqualNodes.FindParentAndNodeOf(node, 7);
            NodeWithEqualNodes.Delete(ref node, 12);

            Console.WriteLine("delete done");
            //Console.ReadKey();


            NodeWithEqualNodes.Delete(ref node, 9);
            
        }

        private static void TestConvertToInt()
        {
            ConvertStringToInt convert = new ConvertStringToInt();

            string input = "123";
            Console.WriteLine($"input: {input}, output: {convert.ConvertToInt(input)}");

            input = "-123";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");
            
            input = "1.23";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");

            input = " 123";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");

            input = "-123 ";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");
            
            input = "123L";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");

            input = "12k3";
            Console.WriteLine($"input: ({input}), output: ({convert.ConvertToInt(input)})");
        }

        public static void TestReverseVowels()
        {
            ReverseWords rw = new ReverseWords();
            string sentence = "Hello";
            string rev = rw.ReverseVowels(sentence);

            Console.WriteLine($"Orig: {sentence}");
            Console.WriteLine($"Revd: {rev}");
        }

        public static void SetBSTNeighborTest()
        {
            NodeWithNeighbor node = new NodeWithNeighbor("d");
            node.Insert("f");
            node.Insert("e");
            node.Insert("g");
            node.Insert("b");
            node.Insert("a");
            node.Insert("c");

            var stack = new Stack<NodeWithNeighbor>();
            NodeWithNeighbor.SetNeighbor(node, stack);

        }

        public static void ReverseWordsTest()
        {
            ReverseWords rw = new ReverseWords();

            //char[] sentence = { 'H', 'e', 'l', 'l', 'o', ' ', 'h', 'o', 'w', ' ', 'a', 'r', 'e', ' ', 't', 'h', 'i', 'n', 'g', 's' };
            string sentence = "Hello how are you doing? I'm fine and you look good.";
            string rev = rw.ReverseWordsInSentence(sentence);

            Console.WriteLine($"Orig: {sentence}");
            Console.WriteLine($"Revd: {rev}");


            
        }

        public static void KthTest()
        { 
            int[] a = new int[] { 1, 3, 5, 7 };
            int[] b = new int[] { 2, 4, 8 };
            KthElement e = new KthElement();
            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 1, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 3 => 4, actual:       {e.GetKthElement(a, b, 3)}");
            Console.WriteLine($"GetK, 6 => 8, actual:       {e.GetKthElement(a, b, 6)}");
            Console.WriteLine($"GetK, 7 => error!, actual:  {e.GetKthElement(a, b, 7)}");
            Console.WriteLine($"GetK, -1 => error!, actual: {e.GetKthElement(a, b, -1)}");

            a = new int[] { 1 };
            b = new int[] { 2, 4, 8 };
            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 1, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 3 => 8, actual:       {e.GetKthElement(a, b, 3)}");
            Console.WriteLine($"GetK, 1 => 2, actual:       {e.GetKthElement(a, b, 1)}");

            a = new int[] { 2, 4, 8 };
            b = new int[] { 1 };
            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 1, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 3 => 8, actual:       {e.GetKthElement(a, b, 3)}");
            Console.WriteLine($"GetK, 1 => 2, actual:       {e.GetKthElement(a, b, 1)}");

            a = new int[] { 5 };
            b = new int[] { 2, 4, 8 };
            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 2, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 3 => 8, actual:       {e.GetKthElement(a, b, 3)}");
            Console.WriteLine($"GetK, 2 => 5, actual:       {e.GetKthElement(a, b, 2)}");
            Console.WriteLine($"GetK, 1 => 4, actual:       {e.GetKthElement(a, b, 1)}");
            
            a = new int[] { 55 };
            b = new int[] { 2, 4, 8 };
            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 2, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 3 => 55, actual:      {e.GetKthElement(a, b, 3)}");
            Console.WriteLine($"GetK, 2 => 5, actual:       {e.GetKthElement(a, b, 2)}");
            Console.WriteLine($"GetK, 1 => 4, actual:       {e.GetKthElement(a, b, 1)}");
            
            a = new int[] { 1, 2, 3, 4, 5 };
            b = new int[] { 2, 3, 4, 5, 7, 9 };

            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 1, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 1 => 2, actual:       {e.GetKthElement(a, b, 1)}");
            Console.WriteLine($"GetK, 2 => 2, actual:       {e.GetKthElement(a, b, 2)}");
            Console.WriteLine($"GetK, 9 => 7, actual:       {e.GetKthElement(a, b, 9)}");
            Console.WriteLine($"GetK, 10 => 9, actual:      {e.GetKthElement(a, b, 10)}");

            a = new int[] { 2, 3, 4, 5, 7, 9 };
            b = new int[] { 1, 2, 3, 4, 5 };            

            Console.WriteLine($"A: ({string.Join(",", a)})");
            Console.WriteLine($"B: ({string.Join(",", b)})");
            Console.WriteLine($"GetK, 0 => 1, actual:       {e.GetKthElement(a, b, 0)}");
            Console.WriteLine($"GetK, 1 => 2, actual:       {e.GetKthElement(a, b, 1)}");
            Console.WriteLine($"GetK, 2 => 2, actual:       {e.GetKthElement(a, b, 2)}");
            Console.WriteLine($"GetK, 9 => 7, actual:       {e.GetKthElement(a, b, 9)}");
            Console.WriteLine($"GetK, 10 => 9, actual:      {e.GetKthElement(a, b, 10)}");


        }

        static void RobotTest()
        {

            Robot robot = new Robot(5, 5);
            robot.init();
            robot.SetupPuzzle();
            robot.PrintPuzzle();
            //robot.SetupPuzzleInvalide();
            timesCalled = 0;
            Console.WriteLine("HasExit? " + HasExit(robot, 0, 0).ToString());
            Console.WriteLine($"TimesCalled {timesCalled}");
        }

        static bool HasExit(Robot robot, int myR, int myC)
        {
            timesCalled++;
            
            if (myR == robot.R || myC == robot.C || !robot.Matrix[myR, myC])
            {
                return false;
            }

            bool foundEnd = (myR == robot.R - 1 && myC == robot.C - 1);

            if (foundEnd || HasExit(robot, myR + 1, myC) || HasExit(robot, myR, myC + 1))
            {
                Console.WriteLine($"({myR},{myC})");
            }

            return false;
        }

        
    }

    public class Robot
    {
        public int R;
        public int C;
        public bool[,] Matrix;
        public Robot(int r, int c)
        {
            R = r;
            C = c;
            Matrix = new bool[R, C];
        }

        public void init()
        {
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    Matrix[i, j] = true;
                }
            }
        }
        public void PrintPuzzle()
        {
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    Console.Write($"{Matrix[i, j]}".PadLeft(6));
                }
                Console.WriteLine();
            }
        }
        public void SetupPuzzle()
        {
            Matrix[1, 1] = false;
            Matrix[1, 2] = false;

          //  Matrix[4, 2] = false;
           // Matrix[R - 1, 0] = false;
            Matrix[R - 2, C - 1] = false;
        }

        public void SetupPuzzleInvalide()
        {
            Matrix[0, 2] = false;
            Matrix[1, 2] = false;
            Matrix[2, 2] = false;
            Matrix[2, 1] = false;
            Matrix[2, 0] = false;
            Matrix[R - 2, C - 1] = false;
        }
    }

   
}
