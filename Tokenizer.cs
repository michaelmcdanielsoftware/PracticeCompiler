using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace PracticeCompiler
{
    class Tokenizer : TokenizerBase
    {
        public Tokenizer(StreamReader inputStream) : base(inputStream) { }
        public Token ReadToken()
        {
            ClearSpace();
            if (Peek() == '-' || Peek() == '+')
            {
                char c = Peek();
                Append();
                if (char.IsDigit(Peek()))
                    return ReadNumber();
                else
                    return LabelString(c == '+' ? Token.Type.PLUS : Token.Type.MINUS);
            }
            if(Peek() >= '0' && Peek() <= '9')
            {
                return ReadNumber();
            } else if (char.IsLetter(Peek()) || Peek() == '_')
            {
                return ReadKeywordOrIdentifier();
            }
            switch(Peek())
            {
                case '{':  return LabelSymbol(Token.Type.LCURLY);
                case '}':  return LabelSymbol(Token.Type.RCURLY);
                case '(':  return LabelSymbol(Token.Type.LPAREN);
                case ')':  return LabelSymbol(Token.Type.RPAREN);
                //case '+':  return LabelSymbol(Token.Type.PLUS);
                //case '-':  return LabelSymbol(Token.Type.MINUS);
                case '*':  return LabelSymbol(Token.Type.TIMES);
                case '/':  return LabelSymbol(Token.Type.DIVIDE);
                case '=':  return LabelSymbol(Token.Type.EQUALS);
                case '\0': return LabelSymbol(Token.Type.EOF);
            }
            Debug.Assert(false, "Unexpected character \"" + Peek() + "\" (" + (int)(Peek()) + ")");
            return LabelString(Token.Type.ERROR);
        }
        private Token ReadNumber()
        {
            bool digit = false;
            while(Peek() >= '0' && Peek() <= '9')
            {
                digit = true;
                Append();
            }
            return LabelString(digit ? Token.Type.NUMBER : Token.Type.ERROR);
        }
        private Token ReadKeywordOrIdentifier()
        {
            if (char.IsLetter(Peek()) || Peek() == '_')
                Append();
            while (char.IsLetter(Peek()) || char.IsDigit(Peek()) || Peek() == '_')
                Append();
            string str = ReadString();
            return new Token(IsKeyword(str) ? Token.Type.KEYWORD : Token.Type.IDENTIFIER, str, Row, Col);
        }
        private Boolean IsKeyword(string str) { return false; }
        private Token LabelSymbol(Token.Type type)
        {
            Append();
            return LabelString(type);
        }
        private Token LabelString(Token.Type type)
        {
            return new Token(type, ReadString(), Row, Col);
        }
    }
}
