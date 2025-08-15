//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShootObserver : InputObserver
    {
        public ShootObserver(IrrKlang.ISoundEngine pSndEngine, IrrKlang.ISoundSource pSnd0)
        {
            Debug.Assert(pSndEngine != null);
            this.pSndEngine = pSndEngine;

            Debug.Assert(pSnd0 != null);
            this.pSnd0 = pSnd0;
        }
        public override void Notify()
        {
            //Debug.WriteLine("Shoot Observer");
            Ship pShip = ShipMan.GetShip();
            pShip.ShootMissile();

            pSndEngine.SoundVolume = 0.2f;
            pSndEngine.Play2D(pSnd0, false, false, false);
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShootObserver;
        }

        private IrrKlang.ISoundEngine pSndEngine;
        private IrrKlang.ISoundSource pSnd0;
    }
}

// --- End of File ---
