using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BagAssignment
{
    #region Exceptions
    public class ElementNullException : Exception { };
    public class ElementNotFoundException : Exception { };
    public class ElementNotIntegerException : Exception { };
    #endregion

    public class Bag : IEnumerable<(int Element, int Frequency)>
    {
        #region Fields

        private List<(int, int)> _elements = new List<(int, int)>();
        private int _singleElementCount = 0;

        #endregion

        #region Constructors

        public Bag()
        {
        }

        public Bag(IEnumerable<int> elements)
        {
            foreach (var element in elements)
            {
                Insert(element);
            }
        }

        #endregion

        #region Public Methods

        public void Insert(int? element)
        {
            if (element != null)
            {
                if (!Int32.TryParse(element.ToString(), out int result))
                {
                    throw new ElementNotIntegerException();
                }

                var index = _elements.FindIndex(x => x.Item1 == result);

                if (index != -1)
                {
                    _elements[index] = (_elements[index].Item1, _elements[index].Item2 + 1);
                    if (_elements[index].Item2 == 2)
                    {
                        _singleElementCount--;
                    }
                }
                else
                {
                    _elements.Add(((int, int))(result, 1));
                    _singleElementCount++;
                }
            }
            else
            {
                throw new ElementNullException();
            }
        }

        public bool Remove(int? element)
        {
            if (element != null)
            {
                if (!Int32.TryParse(element.ToString(), out int result))
                {
                    throw new ElementNotIntegerException();
                }

                var index = _elements.FindIndex(x => x.Item1 == result);

                if (index == -1)
                {
                    throw new ElementNotFoundException();
                }

                if (_elements[index].Item2 == 1)
                {
                    _elements.RemoveAt(index);
                    _singleElementCount--;
                }
                else
                {
                    _elements[index] = (_elements[index].Item1, _elements[index].Item2 - 1);
                    if (_elements[index].Item2 == 1)
                    {
                        _singleElementCount++;
                    }
                }

                return true;
            }
            else
            {
                throw new ElementNullException();
            }
        }


        public int Frequency(int? element)
        {
            if (element != null)
            {
                if (!Int32.TryParse(element.ToString(), out int result))
                {
                    throw new ElementNotIntegerException();
                }

                var index = _elements.FindIndex(x => x.Item1 == result);

                return index != -1 ? _elements[index].Item2 : 0;
            }
            else
            {
                throw new ElementNullException();
            }
        }

        public int SingleElementCount()
        {
            return _singleElementCount;
        }

        public void Print()
        {
            foreach (var element in _elements)
            {
                Console.WriteLine($"{element.Item1}: {element.Item2}");
            }
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<(int Element, int Frequency)> GetEnumerator()
        {
            return _elements.Select(x => (x.Item1, x.Item2)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}