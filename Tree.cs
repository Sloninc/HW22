using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW22
{
    public class Tree<T>  : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T>? _head;

        private int _count;
        public int Count { get { return _count; } }
        public void Add(T value)
        {
            // Первый случай: дерево пустое     

            if (_head == null)
            {
                _head = new Node<T>(value);
            }

            // Второй случай: дерево не пустое, поэтому применяем рекурсивный алгоритм 
            //                для поиска места добавления узла        

            else
            {
                AddHelper(_head, value);
            }
            _count++;
        }

        private void AddHelper(Node<T> node, T value)
        {
            // Первый случай: значение добавляемого узла меньше чем значение текущего. 

            if (value.CompareTo(node.Value) < 0)
            {
                // если левый потомок отсутствует - добавляем его          

                if (node.Left == null)
                {
                    node.Left = new Node<T>(value);
                }
                else
                {
                    // повторная итерация               
                    AddHelper(node.Left, value);
                }
            }
            // Второй случай: значение добавляемого узла равно или больше текущего значения      
            else
            {
                // Если правый потомок отсутствует - добавлем его.          

                if (node.Right == null)
                {
                    node.Right = new Node<T>(value);
                }
                else
                {
                    // повторная итерация

                    AddHelper(node.Right, value);
                }
            }
        }

        public void Traverse()
        {
            Traverse(_head);
        }

        private IEnumerator<T> Traverse(Node<T> node)
        {
            //для получения значений используем "конечный автомат" со стеком возврата
            Stack<Node<T>> wayBack = new Stack<Node<T>>(); 
            Node<T> current = node;
            Node<T> prev = node;
            while (true)
            {
                if (current.Right != prev)
                {
                    if (current.Left != null && current.Left != prev)
                    {
                        wayBack.Push(current);
                        current = current.Left;
                        continue;
                    }
                    yield return current.Value;
                    if (current.Right != null)
                    {
                        wayBack.Push(current);
                        current = current.Right;
                        continue;
                    }
                }
                if (wayBack.Count == 0)
                    break;
                prev = current;
                current = wayBack.Pop();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Traverse(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
