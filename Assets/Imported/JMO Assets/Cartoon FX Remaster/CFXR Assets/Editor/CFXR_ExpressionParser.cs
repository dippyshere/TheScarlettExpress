#region

using System.Collections.Generic;
using System.IO;

#endregion

// Parse conditional expressions from CFXR_MaterialInspector to show/hide some parts of the UI easily

namespace CartoonFX
{
    public static class ExpressionParser
    {
        public delegate bool EvaluateFunction(string content);

        //--------------------------------------------------------------------------------------------------------------------------------
        // Main Function to use

        public static bool EvaluateExpression(string expression, EvaluateFunction evalFunction)
        {
            //Remove white spaces and double && ||
            string cleanExpr = "";
            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case ' ': break;
                    case '&':
                        cleanExpr += expression[i];
                        i++;
                        break;
                    case '|':
                        cleanExpr += expression[i];
                        i++;
                        break;
                    default:
                        cleanExpr += expression[i];
                        break;
                }
            }

            List<Token> tokens = new();
            StringReader reader = new(cleanExpr);
            Token t = null;
            do
            {
                t = new Token(reader);
                tokens.Add(t);
            } while (t.type != Token.TokenType.EXPR_END);

            List<Token> polishNotation = Token.TransformToPolishNotation(tokens);

            List<Token>.Enumerator enumerator = polishNotation.GetEnumerator();
            enumerator.MoveNext();
            Expression root = MakeExpression(ref enumerator, evalFunction);

            return root.Evaluate();
        }

        public static Expression MakeExpression(ref List<Token>.Enumerator polishNotationTokensEnumerator,
            EvaluateFunction _evalFunction)
        {
            if (polishNotationTokensEnumerator.Current.type == Token.TokenType.LITERAL)
            {
                Expression lit = new ExpressionLeaf(_evalFunction, polishNotationTokensEnumerator.Current.value);
                polishNotationTokensEnumerator.MoveNext();
                return lit;
            }

            switch (polishNotationTokensEnumerator.Current.value)
            {
                case "NOT":
                {
                    polishNotationTokensEnumerator.MoveNext();
                    Expression operand = MakeExpression(ref polishNotationTokensEnumerator, _evalFunction);
                    return new ExpressionNot(operand);
                }
                case "AND":
                {
                    polishNotationTokensEnumerator.MoveNext();
                    Expression left = MakeExpression(ref polishNotationTokensEnumerator, _evalFunction);
                    Expression right = MakeExpression(ref polishNotationTokensEnumerator, _evalFunction);
                    return new ExpressionAnd(left, right);
                }
                case "OR":
                {
                    polishNotationTokensEnumerator.MoveNext();
                    Expression left = MakeExpression(ref polishNotationTokensEnumerator, _evalFunction);
                    Expression right = MakeExpression(ref polishNotationTokensEnumerator, _evalFunction);
                    return new ExpressionOr(left, right);
                }
                default:
                    return null;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        // Expression Token

        public class Token
        {
            public enum TokenType
            {
                OPEN_PAREN,
                CLOSE_PAREN,
                UNARY_OP,
                BINARY_OP,
                LITERAL,
                EXPR_END
            }

            static readonly Dictionary<char, KeyValuePair<TokenType, string>> typesDict = new()
            {
                { '(', new KeyValuePair<TokenType, string>(TokenType.OPEN_PAREN, "(") },
                { ')', new KeyValuePair<TokenType, string>(TokenType.CLOSE_PAREN, ")") },
                { '!', new KeyValuePair<TokenType, string>(TokenType.UNARY_OP, "NOT") },
                { '&', new KeyValuePair<TokenType, string>(TokenType.BINARY_OP, "AND") },
                { '|', new KeyValuePair<TokenType, string>(TokenType.BINARY_OP, "OR") }
            };

            public TokenType type;
            public string value;

            public Token(StringReader s)
            {
                int c = s.Read();
                if (c == -1)
                {
                    type = TokenType.EXPR_END;
                    value = "";
                    return;
                }

                char ch = (char)c;

                //Special case: solve bug where !COND_FALSE_1 && COND_FALSE_2 would return True
                bool embeddedNot = ch == '!' && s.Peek() != '(';

                if (typesDict.ContainsKey(ch) && !embeddedNot)
                {
                    type = typesDict[ch].Key;
                    value = typesDict[ch].Value;
                }
                else
                {
                    string str = "";
                    str += ch;
                    while (s.Peek() != -1 && !typesDict.ContainsKey((char)s.Peek()))
                    {
                        str += (char)s.Read();
                    }

                    type = TokenType.LITERAL;
                    value = str;
                }
            }

            public static List<Token> TransformToPolishNotation(List<Token> infixTokenList)
            {
                Queue<Token> outputQueue = new();
                Stack<Token> stack = new();

                int index = 0;
                while (infixTokenList.Count > index)
                {
                    Token t = infixTokenList[index];

                    switch (t.type)
                    {
                        case TokenType.LITERAL:
                            outputQueue.Enqueue(t);
                            break;
                        case TokenType.BINARY_OP:
                        case TokenType.UNARY_OP:
                        case TokenType.OPEN_PAREN:
                            stack.Push(t);
                            break;
                        case TokenType.CLOSE_PAREN:
                            while (stack.Peek().type != TokenType.OPEN_PAREN)
                            {
                                outputQueue.Enqueue(stack.Pop());
                            }

                            stack.Pop();
                            if (stack.Count > 0 && stack.Peek().type == TokenType.UNARY_OP)
                            {
                                outputQueue.Enqueue(stack.Pop());
                            }

                            break;
                    }

                    index++;
                }

                while (stack.Count > 0)
                {
                    outputQueue.Enqueue(stack.Pop());
                }

                List<Token> list = new(outputQueue);
                list.Reverse();
                return list;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        // Boolean Expression Classes

        public abstract class Expression
        {
            public abstract bool Evaluate();
        }

        public class ExpressionLeaf : Expression
        {
            readonly string content;
            readonly EvaluateFunction evalFunction;

            public ExpressionLeaf(EvaluateFunction _evalFunction, string _content)
            {
                evalFunction = _evalFunction;
                content = _content;
            }

            public override bool Evaluate()
            {
                //embedded not, see special case in Token declaration
                if (content.StartsWith("!"))
                {
                    return !evalFunction(content.Substring(1));
                }

                return evalFunction(content);
            }
        }

        public class ExpressionAnd : Expression
        {
            readonly Expression left;
            readonly Expression right;

            public ExpressionAnd(Expression _left, Expression _right)
            {
                left = _left;
                right = _right;
            }

            public override bool Evaluate()
            {
                return left.Evaluate() && right.Evaluate();
            }
        }

        public class ExpressionOr : Expression
        {
            readonly Expression left;
            readonly Expression right;

            public ExpressionOr(Expression _left, Expression _right)
            {
                left = _left;
                right = _right;
            }

            public override bool Evaluate()
            {
                return left.Evaluate() || right.Evaluate();
            }
        }

        public class ExpressionNot : Expression
        {
            readonly Expression expr;

            public ExpressionNot(Expression _expr)
            {
                expr = _expr;
            }

            public override bool Evaluate()
            {
                return !expr.Evaluate();
            }
        }
    }
}