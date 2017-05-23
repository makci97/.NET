using System.Collections;

namespace List
{
    public class List<T>: IEnumerable
    {
        public class Node
        {
            public Node Next;
            public Node Prev;
            public T Data;
        }

        private Node _head;
        private Node _tail;
        private int _size;

        public List()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public void PushBack(T t)
        {
            Node newNode = new Node();
            newNode.Next = _tail;
            newNode.Prev = null;
            newNode.Data = t;

            if (_tail != null)
            {
                // List is not empty
                _tail.Prev = newNode;
            }
            else
            {
                // List is empty, therefore head == null too
                _head = newNode;
            }
            _tail = newNode;
            ++_size;
        }

        public void PushFront(T t)
        {
            Node newNode = new Node();
            newNode.Next = null;
            newNode.Prev = _head;
            newNode.Data = t;

            if (_head != null)
            {
                // List is not empty
                _head.Next = newNode;
            }
            else
            {
                // List is empty, therefore tail == null too
                _tail = newNode;
            }
            _head = newNode;
            ++_size;
        }

        public T PopBack()
        {
            if (_tail != null)
            {
                T temp = _tail.Data;
                if (_head == _tail)
                {
                    // pop last elem
                    _head = _tail = null;
                }
                else
                {
                    _tail = _tail.Next;
                }
                --_size;
                return temp;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public T PopFront()
        {
            if (_head != null)
            {
                T temp = _head.Data;
                if (_head == _tail)
                {
                    // pop last elem
                    _head = _tail = null;
                }
                else
                {
                    _head = _head.Prev;
                }
                --_size;
                return temp;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public T Back()
        {
            if (_tail != null)
            {
                return _tail.Data;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public T Front()
        {
            if (_head != null)
            {
                return _head.Data;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public int Size()
        {
            return _size;
        }


        public class ListEnum : IEnumerator
        {
            private List<T> list;
            private Node _cur;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            private int _position;

            public int Position
            {
                get { return _position; }
                set { _position = value; }
            }

            public ListEnum(List<T> l)
            {
                list = l;
                _cur = list._head;
                Position = -1;
            }

            public bool MoveNext()
            {
                if (Position == -1)
                {
                    _cur = list._head;
                }
                else
                {
                    _cur = _cur.Prev;
                }
                Position++;
                return (_cur != null);
            }

            public void Reset()
            {
                Position = -1;
                _cur = list._head;
            }

            object IEnumerator.Current => Current;

            public T Current
            {
                get
                {
                    try
                    {
                        return _cur.Data;
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }
                }
            }
        }


        public ListEnum GetEnumerator()
        {
            return new ListEnum(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

    }
}