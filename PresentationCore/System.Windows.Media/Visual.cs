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
// Authors:
//	Shahid khan (shahid.mrd@hotmail.com)
//

using System;
using System.Collections.Generic;
using System.Windows;
//using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace System.Windows.Media {

	public abstract class Visual : DependencyObject {

		private List<Visual> visualChildren;
		private Visual visualParent;

		protected Visual ()
		{
			visualParent = null;
		}
		 
		private void SetVisualParent(Visual child)
		{
			Visual oldParent = child.visualParent;
			child.visualParent = this;
			child.OnVisualParentChanged (oldParent);
		}

		protected void AddVisualChild (Visual child)
		{
			OnVisualChildrenChanged (child, null);
			child.visualParent = this.SetVisualParent (child);
		}

		public DependencyObject FindCommonVisualAncestor (DependencyObject otherVisual)
		{
			Visual visual = otherVisual as Visual;
			Visual thisVisual = this;
		    DependencyObject commonAncestor = null;

			if (visual == null)
				throw new ArgumentNullException ();

			if (visual.visualParent != null && thisVisual.visualParent != null) {

				do {

					if (visual.visualParent.Equals (thisVisual.visualParent)) {
						commonAncestor = visual.visualParent;
						break;

					} else {
						visual = visual.visualParent;
						if (visual == null) {
							visual = otherVisual as Visual;
							thisVisual = thisVisual.visualParent;
						}
					} 

				} while(visual.visualParent != null && thisVisual.visualParent != null);

			}
			return commonAncestor;
		}

		protected virtual Visual GetVisualChild (int index)
		{
			return visualChildren [index];
		}

	/*	protected virtual GeometryHitTestResult HitTestCore (GeometryHitTestParameters hitTestParameters)
		{
			throw new NotImplementedException ();
		}

		protected virtual HitTestResult HitTestCore (PointHitTestParameters hitTestParameters)
		{
			throw new NotImplementedException ();
		}
	*/	

		public bool IsAncestorOf (DependencyObject descendant)
		{
			if (!(descendant is Visual))
				throw new ArgumentException (string.Format ("'{0}' is not a Visual ", descendant.GetType()));

			if (this.visualChildren.Contains(descendant as Visual))
			    return true;
	        else
			    return false;
		}

	
		public bool IsDescendantOf (DependencyObject ancestor)
		{
			if (!(ancestor is Visual))
				throw new ArgumentException (string.Format ("'{0}' is not a Visusl", ancestor.GetType()));

			if (this.visualParent.Equals (ancestor))
				return true;
			else
				return false;
		}

		protected internal virtual void OnVisualChildrenChanged (DependencyObject visualAdded,
		                                                         DependencyObject visualRemoved)
		{
			if(visualAdded != null)
				visualChildren.Add (visualAdded as Visual);
			if (visualRemoved != null)
				visualChildren.Remove (visualRemoved as Visual);
		}

		protected internal virtual void OnVisualParentChanged (DependencyObject oldParent)
		{
			if (oldParent != null) {
				int index = VisualChildrenCount - 1;
				(oldParent as Visual).OnVisualChildrenChanged (null,GetVisualChild(index));
			}
		}

		public Point PointFromScreen (Point point)
		{
			throw new NotImplementedException ();
		}

		public Point PointToScreen (Point point)
		{
			throw new NotImplementedException ();
		}

		protected void RemoveVisualChild (Visual child)
		{
			OnVisualChildrenChanged (null, child);
		}
		/*
		public GeneralTransform TransformToAncestor (Visual ancestor)
		{
			throw new NotImplementedException ();
		}

		public GeneralTransform TransformToDescendant (Visual descendant)
		{
			throw new NotImplementedException ();
		}

		public GeneralTransform TransformToVisual (Visual visual)
		{
			throw new NotImplementedException ();
		}

		[Obsolete ("BitmapEffects are inefficient and will be deprecated in a future release. Use UIElement.Effect and ShaderEffects instead.")]
		protected internal BitmapEffect VisualBitmapEffect {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}

		#if waiting
		[Obsolete ("BitmapEffects are inefficient and will be deprecated in a future release. Use UIElement.Effect and ShaderEffects instead.")]
		protected internal BitmapEffectInput VisualBitmapEffectInput {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
		#endif

		protected internal BitmapScalingMode VisualBitmapScalingMode {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
        */
		protected virtual int VisualChildrenCount {
			get {
				return visualChildren.Count;
			}
		}
		/*
		protected internal Geometry VisualClip {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}

		protected internal EdgeMode VisualEdgeMode {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
        */
		protected internal Vector VisualOffset {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}

		protected internal double VisualOpacity {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
		/*
		protected internal Brush VisualOpacityMask {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
        */
		protected DependencyObject VisualParent {
			get {
				return visualParent;
			}
		}
        /*
		protected internal Transform VisualTransform {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}

		protected internal DoubleCollection VisualXSnappingGuidelines {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}

		protected internal DoubleCollection VisualYSnappingGuidelines {
			get {
				throw new NotImplementedException ();
			}
			protected set {
				throw new NotImplementedException ();
			}
		}
		*/
	}
}