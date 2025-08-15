//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract class InputObserver : SLink
    {
        public enum Name
        {
            MoveLeftObserver,
            MoveRightObserver,
            ShootObserver,
            NextObserver,
            Uninitialized
        }

        // define this in concrete
        abstract public void Notify();

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public InputSubject pSubject;
    }
}

// --- End of File ---
