//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{

    abstract public class ColObserver : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            SoundObserver,
            GridObserver,
            ShipReadyObserver,
            ShipRemoveMissileObserver,
            ShipRemoveObserver,
            ShipMoveObserver,
            BombObserver,
            RemoveBrickObserver,
            RemoveMissileObserver,
            RemoveBombObserver,
            RemoveInvaderObserver,
            RemoveUFOObserver,

            Uninitialized
        }
        public abstract void Notify();

        // WHY not add a Command pattern into our Observer!
        public virtual void Execute()
        {
            // default implementation
        }

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public ColSubject pSubject;
    }


}

// --- End of File ---
