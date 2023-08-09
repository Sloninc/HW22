using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW22
{
    /// <summary>
    /// Класс двоичного дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>  : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T>? _head;

        private int _count;
        public int Count { get { return _count; } }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }
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

        // Метод Find возвращает первый найденный узел.
        // Если значение не найдено, метод возвращает null.
        
        public T Find(Func<T,int> predicate)
        {
            // Поиск значения в дереве.     

            Node<T>? current = _head;


            while (current != null)
            {
                int result = predicate(current.Value);
                if (result > 0)
                {
                    // Если искомое значение меньше значение текущего узла - переходим к левому потомку.             

                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если искомое значение больше значение текущего узла - переходим к правому потомку.


                    current = current.Right;
                }
                else
                {
                    // Искомый элемент найден             
                    break;
                }
            }
            if (current == null)
                return default(T);
            return current.Value;
        }
        public void Traverse()
        {
            //try
            //{
            Traverse(_head);
            //}
            //catch
            //{
            //    throw new NullReferenceException();
            //}
        }

        private IEnumerator<T> Traverse(Node<T> node)
        {
            if(node==null)
                yield break;
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
