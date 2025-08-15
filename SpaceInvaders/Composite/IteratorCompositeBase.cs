//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class IteratorCompositeBase
    {
        abstract public Component Next();
        abstract public bool IsDone();
        abstract public Component First();
        abstract public Component Curr();
    }
    
}

// --- End of File ---
