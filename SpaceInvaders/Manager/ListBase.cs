//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public abstract class ListBase
    {
        abstract public void AddToFront(NodeBase pNode);
        virtual public void AddWithPriority(NodeBase pNode, float priority)
        {
            Debug.Assert(false);
        }
        abstract public void Remove(NodeBase pNode);
        abstract public NodeBase RemoveFromFront();
        abstract public Iterator GetIterator();
    }
}

// --- End of File ---
