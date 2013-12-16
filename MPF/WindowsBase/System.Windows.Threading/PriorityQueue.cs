using System;
using System.Collections.Generic;
namespace System.Windows.Threading
{
	internal class PriorityQueue<T>
	{

		private Queue<T> itemQueue;
		private Dictionary<DispatcherPriority, List<T>> priorityDictionary;
		private List<DispatcherPriority> priorityList;
		private List<T> itemList;

		public PriorityQueue()
		{
			itemQueue = new Queue<T> ();
			priorityDictionary = new Dictionary<DispatcherPriority, List<T>>();
			priorityList = new List<DispatcherPriority>();

		}

		public void Enqueue(DispatcherPriority priority,T item)
		{
			QueuePriorityDictionary(priority,item);

		}

		private void QueuePriorityDictionary(DispatcherPriority priority,T item)
		{
			if (priorityList.Contains(priority))
			{
				itemList = priorityDictionary[priority];
				itemList.Add(item);
				itemList = null;
			}
			else
			{
				priorityList.Add(priority);
				itemList = new List<T>();
				itemList.Add(item);
				priorityDictionary[priority] = itemList;
				itemList = null;
			}
		}

		private void PriorityQueueFill()
		{
			priorityList.Sort();
			for (int i = 0; i < priorityList.Count; i++)
			{
				itemList = priorityDictionary[priorityList[i]];
				for (int j = 0; j < itemList.Count; j++)
					itemQueue.Enqueue(itemList[j]);
			}
		}

		public T Dequeue()
		{
			PriorityQueueFill();
			if (itemQueue.Count > 0)
			{
				T item = itemQueue.Dequeue();
				return item;
			}
			else
				return default(T);
		}
	}
}

