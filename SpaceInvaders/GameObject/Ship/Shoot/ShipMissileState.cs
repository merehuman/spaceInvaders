//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ShipMissileState
    {
        /// <returns>True if a new missile was spawned (Ready → Flying).</returns>
        public abstract bool ShootMissile(Ship pShip);
    }
}

// --- End of File ---
