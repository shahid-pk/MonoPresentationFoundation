namespace System.Windows {
	public sealed class RoutedEvent {

		internal RoutedEvent (string name,
		                      Type handlerType,
		                      Type ownerType
		                      /*,RoutingStrategy routingStrategy*/)
		{
			this.name = name;
			this.handlerType = handlerType;
			this.ownerType = ownerType;
			//this.routingStrategy = routingStrategy;
		}

		public RoutedEvent AddOwner (Type ownerType)
		{
			// XXX more here
			return this;
		}

		public override string ToString ()
		{
			return string.Format("{0}.{1}", OwnerType.Name, name);
		}

		public Type HandlerType {
			get { return handlerType; }
		}

		public string Name {
			get { return name; }
		}

		public Type OwnerType {
			get { return ownerType; }
		}

	/*	public RoutingStrategy RoutingStrategy {
			get { return routingStrategy; }
		}
    */
		string name;
		Type handlerType;
		Type ownerType;
		//RoutingStrategy routingStrategy;
	}
}
