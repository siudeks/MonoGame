// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Represents a collection of effects associated with a model.
    /// </summary>
	public sealed class ModelEffectCollection : ReadOnlyCollection<Effect>
	{
		internal ModelEffectCollection(IList<Effect> list)
			: base(list)
		{

		}

	    internal ModelEffectCollection() : base(new List<Effect>())
	    {
	    }

        /// <summary>
        /// ModelMeshPart needs to be able to add to ModelMesh's effects list
        /// </summary>
        /// <param name="item"></param>
        internal void Add(Effect item)
		{
			Items.Add (item);
		}
		internal void Remove(Effect item)
		{
			Items.Remove (item);
		}

        /// <summary>
        /// Returns a ModelEffectCollection.Enumerator that can iterate through a ModelEffectCollection.
        /// </summary>
        /// <returns></returns>
	    public new ModelEffectCollection.Enumerator GetEnumerator()
		{
			return new ModelEffectCollection.Enumerator((List<Effect>)Items);
		}

        /// <summary>
        /// Provides the ability to iterate through the bones in an ModelEffectCollection.
        /// </summary>
	    public struct Enumerator : IEnumerator<Effect>, IDisposable, IEnumerator
	    {
			List<Effect>.Enumerator enumerator;
            bool disposed;

			internal Enumerator(List<Effect> list)
			{
				enumerator = list.GetEnumerator();
                disposed = false;
			}

            /// <summary>
            /// Gets the current element in the ModelEffectCollection.
            /// </summary>
            public Effect Current { get { return enumerator.Current; } }

            /// <summary>
            /// Immediately releases the unmanaged resources used by this object.
            /// </summary>
	        public void Dispose()
            {
                if (!disposed)
                {
                    enumerator.Dispose();
                    disposed = true;
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the ModelEffectCollection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if
            /// the enumerator has passed the end of the collection.
            /// </returns>
	        public bool MoveNext() { return enumerator.MoveNext(); }

	        #region IEnumerator Members

	        object IEnumerator.Current
	        {
	            get { return Current; }
	        }

	        void IEnumerator.Reset()
	        {
				IEnumerator resetEnumerator = enumerator;
				resetEnumerator.Reset ();
				enumerator = (List<Effect>.Enumerator)resetEnumerator;
	        }

	        #endregion
	    }
	}
}
