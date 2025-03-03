using System.Collections.Generic;
using System;

namespace Utils
{
    /// <summary>
    /// A static meesaging System using interfaces.
    /// After doing some benchmarking, this system is faster than using the most of the other available event system.
    /// The only drawback is that the list of subscribers are static, potentially creating a memory leak if the subscribers are not unsubscribed.
    /// </summary>
    /// <typeparam name="T">The message type you want to share with subscribers</typeparam>
    public static class MessagingSystem<T>
    {
        private static readonly List<ISubscriber<T>> _subscribers = new List<ISubscriber<T>>();

        public static void Publish(T data)
        {
            for (int i = 0; i < _subscribers.Count; i++)
            {
                if (_subscribers[i] != null)
                {
                    _subscribers[i].OnEvent(data);
                }
                else
                {
                    _subscribers.RemoveAt(i);
                    Console.WriteLine($"A Subscriber to the event type {data.GetType()} has been found null.");
                }
            }
        }

        public static void Subscribe(ISubscriber<T> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public static void Unsubscribe(ISubscriber<T> subscriber)
        {
            _subscribers.Remove(subscriber);
        }
    }

    public interface ISubscriber<T>
    {
        void OnEvent(T evt);
    }

    public struct MessageStructExample
    {
        public int Data { get; }

        public MessageStructExample(int data)
        {
            Data = data;
        }
    }

    public sealed class SubscriberExample : ISubscriber<MessageStructExample>
    {
        public SubscriberExample()
        {
            MessagingSystem<MessageStructExample>.Subscribe(this);
        }

        ~SubscriberExample()
        {
            MessagingSystem<MessageStructExample>.Unsubscribe(this);
        }

        public void RaiseEventExample()
        {
            MessagingSystem<MessageStructExample>.Publish(new MessageStructExample(42));
        }

        public void OnEvent(MessageStructExample evt)
        {
            Console.WriteLine($"Received message with data: {evt.Data}");
        }
    }
}