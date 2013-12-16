using System;

namespace System.Windows.Threading
{
	public enum DispatcherPriority
	{
		ApplicationIdle=2,
		Background=4,
		ContextIdle=3,
		DataBind=8,	
		Inactive=0,	
		Input=5,	
		Invalid= -1,	
		Loaded=6,	
		Normal=9,	
		Render=7,	
		Send=10,	
		SystemIdle=1
	};

	public sealed class Dispatcher
	{
		#region private members
		private System.Threading.Thread thread;
		private static Dispatcher dispatcher;
		#endregion 


		private Dispatcher ()
		{
			thread = System.Threading.Thread.CurrentThread;
		}
		DispatcherEventArgs args = new DispatcherEventArgs();
		#region public properties

		public System.Threading.Thread Thread { get{ return thread; } }

		public static Dispatcher CurrentDispatcher 
		{ get 
			{ 
				if (dispatcher != null)
					return dispatcher;
				else {
					dispatcher = new Dispatcher ();
					return dispatcher;
				}
			}
		 }

		public bool HasShutdownFinished {
			get;
			private set;
		 }
		#endregion

		#region Events
		public event EventHandler ShutdownStarted;
		public event EventHandler ShutdownFinished;
		#endregion
	}
}

