using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeCompiler
{
    class Token
    {
        public enum Type
        {
            EOF,ERROR,NUMBER,IDENTIFIER,KEYWORD,
            LCURLY,RCURLY,LPAREN,RPAREN,
            PLUS,MINUS,TIMES,DIVIDE,EQUALS
        }
        public readonly Type type;
        public readonly int row, col;
        public readonly string raw;
        public Token(Token.Type type, string raw, int row, int col)
        {
            this.type = type;
            this.raw = raw;
            this.row = row;
            this.col = col;
        }
    }
}
