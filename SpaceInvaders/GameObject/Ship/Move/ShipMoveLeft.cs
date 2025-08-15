//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipMoveLeft : ShipMoveState
    {

        public override void MoveRight(Ship pShip)
        {
         
        }
        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            pShip.SetState(ShipMan.MoveState.MoveBoth);
        }

    }
}

// --- End of File ---
