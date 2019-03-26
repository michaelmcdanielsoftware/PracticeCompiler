using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace PracticeCompiler
{
    class TokenizerBase
    {
        private readonly StreamReader input;
        private char buffer;
        private int row, col;
        private List<char> current;
        public TokenizerBase(StreamReader inputStream)
        {
            input = inputStream;
            row = 0; col = 0;
            current = new List<char>();
            ReadChar(); //sets up buffer
        }
        public int Row => row;
        public int Col => col;
        protected char Peek() => buffer;
        protected char ReadChar()
        {
            char last = buffer;
            int raw = input.Read();
            buffer = (char)((raw == -1) ? 0 : raw); //use 0 as EOF, okay because 0 is also invalid
            row++;
            if (buffer == '\n') { row = 0; col++; }
            return last;
        }
        protected void Append()
        {   
            current.Add(ReadChar());
        }
        protected string ReadString()
        {
            String temp = String.Concat(current);
            current = new List<char>();
            return temp;
        }
        protected void ClearSpace()
        {
            while (char.IsWhiteSpace(Peek()))
                Append();
            ReadString();
        }
    }
}
