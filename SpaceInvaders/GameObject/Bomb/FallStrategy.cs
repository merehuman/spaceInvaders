//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class FallStrategy
    {
        abstract public void Fall(Bomb pBomb);
        abstract public void Reset(float posY);

    }
}

// --- End of File ---
