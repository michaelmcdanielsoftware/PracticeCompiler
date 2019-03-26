using System;
using System.IO;

namespace PracticeCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = File.OpenText(args[0]);
            Tokenizer tokenizer = new Tokenizer(sr);
            Token t = tokenizer.ReadToken();
            while (t.type != Token.Type.EOF)
            {
                Console.WriteLine(t.type + "\t" + t.raw);
                t = tokenizer.ReadToken();
            }
            Console.ReadKey();
        }
    }
}
