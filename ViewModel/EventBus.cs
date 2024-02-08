using System;
using System.Collections.Generic;
using System.Linq;
using ViewModel.Core;

namespace ViewModel
{
    public static class MyEventBus
    {
        private static readonly Dictionary<Type, List<IGlobalSubscriber>> Subscribers = new();

        public static void RaiseEvent<T>(Action<T> action) where T : IGlobalSubscriber
        {
            var subs = Subscribers[typeof(T)];
            foreach (var sub in subs)
            {
                action.Invoke((T)sub);
            }
        }
        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            var subscriberTypes = GetTypes(subscriber);
            foreach (var t in subscriberTypes)
            {
                if (!Subscribers.ContainsKey(t)) Subscribers[t] = new List<IGlobalSubscriber>();
                Subscribers[t].Add(subscriber);
            }
        }
        public static void Subscribe<T>(IGlobalSubscriber subscriber) where T: IGlobalSubscriber
        {
            var t = typeof(T);
                if (!Subscribers.ContainsKey(t)) Subscribers[t] = new List<IGlobalSubscriber>();
                Subscribers[t].Add(subscriber);
            
        }
        public static void UnSubscribe<T>(IGlobalSubscriber subscriber) where T: IGlobalSubscriber
        {
            var t = typeof(T);
            if (!Subscribers.ContainsKey(t)) Subscribers[t] = new List<IGlobalSubscriber>();
            Subscribers[t].Remove(subscriber);
            
        }

        public static void UnSubscribe(IGlobalSubscriber subscriber)
        {
            var subscriberTypes = GetTypes(subscriber);
            foreach (var t in subscriberTypes.Where(t => Subscribers.ContainsKey(t)))
            {
                Subscribers[t].Remove(subscriber);
            }
        }

        private static List<Type> GetTypes(IGlobalSubscriber subscriber)
        {
            var type = subscriber.GetType();
            var subscriberTypes = type.GetInterfaces()
                .Where(t=>t
                    .IsAssignableFrom(typeof(IGlobalSubscriber)) &&
                          t != typeof(IGlobalSubscriber)).ToList(); 
            return subscriberTypes;
        }
    }
}