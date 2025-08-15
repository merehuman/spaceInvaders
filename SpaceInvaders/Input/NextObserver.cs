//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class NextObserver : InputObserver
    {
        public override void Notify()
        {
            Font pFont = FontMan.Find(Font.Name.TimedCharacter);
            FontMan.Remove(pFont);
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.NextObserver;
        }
    }
}

// --- End of File ---
