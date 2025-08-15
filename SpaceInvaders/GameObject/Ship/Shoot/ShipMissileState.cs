//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ShipMissileState
    {
        // Transitions to correct state
        public abstract void ShootMissile(Ship pShip);
    }
}

// --- End of File ---
