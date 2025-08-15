//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveMissileObserver : ColObserver
    {
        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }
        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            this.pMissile = m.pMissile;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            //this.pMissile = (Missile)this.pSubject.pObjA;

            if (this.pSubject.pObjA is Missile)
            {
                this.pMissile = (Missile)this.pSubject.pObjA;
            }
            else if (this.pSubject.pObjB is Missile)
            {
                this.pMissile = (Missile)this.pSubject.pObjB;
            }
            else
            {
                Debug.Assert(false);
            }
            
            Debug.Assert(this.pMissile != null);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;
                //   Delay
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveMissileObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pMissile;
    }
}

// --- End of File ---
