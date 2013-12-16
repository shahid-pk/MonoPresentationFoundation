using System;

namespace System.Windows.Threading
{
	public class DispatcherEventArgs : EventArgs
	{
		public Dispatcher Dispatcher { get; internal set; }
	}
}

