//
// DependencyPropertyChangedEventArgs.cs
//
// Author:
//   Chris Toshok (toshok@ximian.com)
//
// (C) 2007 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace System.Windows {
	public struct DependencyPropertyChangedEventArgs {

		DependencyProperty property;
		object oldValue;
		object newValue;

		public DependencyPropertyChangedEventArgs (DependencyProperty property, object oldValue, object newValue)
		{
			this.property = property;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		public object NewValue {
			get { return newValue; }
		}

		public object OldValue {
			get { return oldValue; }
		}

		public DependencyProperty Property {
			get { return property; }
		}

		public override bool Equals (object obj)
		{
			if (!(obj is DependencyPropertyChangedEventArgs))
				return false;

			return Equals ((DependencyPropertyChangedEventArgs)obj);
		}

		public bool Equals (DependencyPropertyChangedEventArgs args)
		{
			return (property == args.Property &&
				newValue == args.NewValue &&
				oldValue == args.OldValue);
		}

		public static bool operator != (DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
		{
			throw new NotImplementedException ();
		}

		public static bool operator == (DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
		{
			throw new NotImplementedException ();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException ();
		}

	}
}