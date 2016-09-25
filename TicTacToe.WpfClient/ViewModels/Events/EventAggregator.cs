using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels.Events
{
    class EventAggregator
    {
        private readonly Dictionary<Type, object> _eventSubscribersMap = new Dictionary<Type, object>();
        private readonly object _syncLock = new object();

        #region Constructors and Singleton

        private static readonly EventAggregator _instance;

        static EventAggregator()
        {
            _instance = new EventAggregator();
        }

        private EventAggregator()
        {
        }

        public static EventAggregator Instance
        {
            get { return _instance; }
        }

        #endregion

        public void Subscribe<TEvent>(Action<TEvent> subscriber)
        {
            var key = typeof (TEvent);

            lock (_syncLock)
            {
                if (!_eventSubscribersMap.ContainsKey(key))
                {  
                    _eventSubscribersMap.Add(key, subscriber);
                }
                else
                {
                    var action = (Action<TEvent>)_eventSubscribersMap[key];
                    action += subscriber;
                    _eventSubscribersMap[key] = action;
                }
            }
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var key = typeof (TEvent);

            if (!_eventSubscribersMap.ContainsKey(key))
                return;

            var action = (Action<TEvent>)_eventSubscribersMap[key];
            action(@event);
        }
    }
}
