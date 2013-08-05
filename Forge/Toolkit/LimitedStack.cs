using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit
{
    public class LimitedStack<T>
    {
        public readonly int Limit;
        private readonly List<T> _stack;

        public LimitedStack(int limit = 32)
        {
            Limit = limit;
            _stack = new List<T>(limit);
        }

        public void Push(T item)
        {
            if (_stack.Count == Limit) _stack.RemoveAt(0);
            _stack.Add(item);
        }

        public void Clear()
        {
            _stack.Clear();
        }

        public T Peek()
        { 
            return _stack[_stack.Count - 1];
        }

        public T Pop()
        {
            _stack.RemoveAt(_stack.Count - 1);
            return _stack[_stack.Count - 1];
        }

        public int Count
        {
            get { return _stack.Count; }
        }
    }
}
