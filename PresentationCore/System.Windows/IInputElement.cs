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
//
// Authors:
//	Shahid khan shahid.mrd@hotmail.com)
//

using System;

namespace System.Windows
{
	public interface IInputElement
	{
		bool Focusable { get; set; }
		bool IsEnabled { get; }

		bool IsKeyboardFocus { get; }
		bool IsKeyboardFocusWithin { get; }

		bool IsMouseCaptured { get; }
		bool IsMouseDirectlyOver { get; }
		bool IsMouseOver { get; }

		bool IsStylusCaptured { get; }
		bool IsStylusDirectlyOver { get; }
		bool IsStylusOver { get; }

		void AddHandler (RoutedEvent routedEvent, Delegate handler);
		bool CaptureMouse ();
		bool CaptureStylus ();
		bool Focus ();
	//	void RaiseEvent (RoutedEventArgs e);
		void ReleaseMouseCapture ();
		void ReleaseStylusCapture ();
		void RemoveHandler (RoutedEvent routedEvent, Delegate handler);
	}
}

