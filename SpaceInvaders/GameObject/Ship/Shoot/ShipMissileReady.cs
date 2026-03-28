//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipMissileReady : ShipMissileState
    {

        public override bool ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();
            pMissile.SetPos(pShip.x, pShip.y + 20);

            pShip.SetState(ShipMan.MissileState.Flying);
            return true;
        }

    }
}

// --- End of File ---
