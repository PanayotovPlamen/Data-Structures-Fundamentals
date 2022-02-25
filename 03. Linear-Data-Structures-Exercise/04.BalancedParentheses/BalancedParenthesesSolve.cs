namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            Stack<char> result = new Stack<char>();

            var isBalanced = true;

            foreach (char item in parentheses)
            {
                if (result.Count == 0)
                {
                    result.Push(item);
                }
                else
                {
                    char lastItem = result.Peek();

                    switch (lastItem)
                    {
                        case '(': 
                            {
                                switch (item)
                                {
                                    case ')':
                                        var removedItem = result.Pop();
                                        break;
                                    case '}':
                                        isBalanced = false;
                                        break;
                                    case ']':
                                        isBalanced = false;
                                        break;
                                    case '(':
                                        result.Push(item);
                                        break;
                                    case '{':
                                        result.Push(item);
                                        break;
                                    case '[':
                                        result.Push(item);
                                        break;
                                }
                            }
                            break;
                        case '{':
                            {
                                switch (item)
                                {
                                    case ')':
                                        isBalanced = false;
                                        break;
                                    case '}':
                                        var removedItem = result.Pop();
                                        break;
                                    case ']':
                                        isBalanced = false;
                                        break;
                                    case '(':
                                        result.Push(item);
                                        break;
                                    case '{':
                                        result.Push(item);
                                        break;
                                    case '[':
                                        result.Push(item);
                                        break;
                                }
                            }
                            break;
                        case '[':
                            {
                                switch (item)
                                {
                                    case ')':
                                        isBalanced = false;
                                        break;
                                    case '}':
                                        isBalanced = false;
                                        break;
                                    case ']':
                                        var removedItem = result.Pop();
                                        break;
                                    case '(':
                                        result.Push(item);
                                        break;
                                    case '{':
                                        result.Push(item);
                                        break;
                                    case '[':
                                        result.Push(item);
                                        break;
                                }
                            }
                            break;
                    }
                   
                }
                
            }

            if (isBalanced && result.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }            

        }
    }
}
