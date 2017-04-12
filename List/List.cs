namespace List
{
    public class List<T>
    {
        private class Node
        {
            public Node next;
            public Node prev;
            public T data;
        }

        private Node head = null;
        private Node tail = null;
        private int size = 0;

        public void PushBack(T t)
        {
            Node newNode = new Node();
            newNode.next = tail;
            newNode.prev = null;
            newNode.data = t;

            if (tail != null)
            {
                // List is not empty
                tail.prev = newNode;
            }
            else
            {
                // List is empty, therefore head == null too
                head = newNode;
            }
            tail = newNode;
            ++size;
        }

        public void PushFront(T t)
        {
            Node newNode = new Node();
            newNode.next = null;
            newNode.prev = head;
            newNode.data = t;

            if (head != null)
            {
                // List is not empty
                head.next = newNode;
            }
            else
            {
                // List is empty, therefore tail == null too
                tail = newNode;
            }
            head = newNode;
            ++size;
        }

        public T PopBack()
        {
            if (tail != null)
            {
                T temp = tail.data;
                if (head == tail)
                {
                    // pop last elem
                    head = tail = null;
                }
                else
                {
                    tail = tail.next;
                }
                --size;
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
            if (head != null)
            {
                T temp = head.data;
                if (head == tail)
                {
                    // pop last elem
                    head = tail = null;
                }
                else
                {
                    head = head.prev;
                }
                --size;
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
            if (tail != null)
            {
                return tail.data;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public T Front()
        {
            if (head != null)
            {
                return head.data;
            }
            else
            {
                // List is empty
                throw new System.InvalidOperationException("List is empty!");
            }
        }

        public int Size()
        {
            return size;
        }
    }
}