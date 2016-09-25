using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class RoundRobinIterator<T>
    {
        private readonly IList<T> _items;
        private int _currentIndex = -1; // initial state

        public RoundRobinIterator(T firstItem, params T[] items)
        {
            var itemsList = new List<T> {firstItem};
            itemsList.AddRange(items);
            _items = itemsList;
        }

        public RoundRobinIterator(IList<T> items)
        {
            if (items == null) 
                throw new ArgumentNullException("items");

            if(items.Count == 0)
                throw new ArgumentException("items should contain at least one element");

            _items = items;
        }

        public T Next()
        {
            _currentIndex = _currentIndex == _items.Count - 1 ? 0 : ++_currentIndex;
            return _items[_currentIndex];
        }
    }
}
