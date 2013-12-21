//
// NamespaceMapEntry.cs
//
// Author:
//   Iain McCoy (iain@mccoy.id.au)
//
// (C) 2005 Iain McCoy
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

namespace System.Windows.Serialization {
	public class NamespaceMapEntry {
		private string xmlNamespace, clrNamespace, assemblyName;
		public NamespaceMapEntry() {
		}

		public NamespaceMapEntry(string xmlNamespace, string assemblyName, string clrNamespace)
		{
			this.xmlNamespace = xmlNamespace;
			this.clrNamespace = clrNamespace;
			this.assemblyName = assemblyName;
		}

		public string XmlNamespace {
			get { return xmlNamespace; }
			set { xmlNamespace = value; }
		}
		public string ClrNamespace {
			get { return clrNamespace; }
			set { clrNamespace = value; }
		}
		public string AssemblyName {
			get { return assemblyName; }
			set { assemblyName = value; }
		}
	}
}
