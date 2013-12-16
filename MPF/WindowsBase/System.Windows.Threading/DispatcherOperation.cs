using System;

namespace System.Windows.Threading
{
	public class DispatcherOperation
	{
		public Dispatcher Dispatcher { get; internal set; }
		public DispatcherPriority Priority { get; set; }
	}
}

