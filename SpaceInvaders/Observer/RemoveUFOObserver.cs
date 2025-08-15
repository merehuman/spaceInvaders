//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveUFOObserver : ColObserver
    {
        public RemoveUFOObserver()
        {
            this.pUFO = null;
        }
        public RemoveUFOObserver(RemoveUFOObserver b)
        {
            this.pUFO = b.pUFO;
        }
        public override void Notify()
        {
            if (this.pSubject.pObjA is UFO)
            {
                this.pUFO = (UFO)this.pSubject.pObjA;
            }
            else if (this.pSubject.pObjB is UFO)
            {
                this.pUFO = (UFO)this.pSubject.pObjB;
            }
            else
            {
                Debug.Assert(false);
            }

            Debug.Assert(this.pUFO != null);

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;
                //   Delay
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                DelayedObjectMan.Attach(pObserver);
                ScoreManager.UpdateScore(GameObject.Name.UFO);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            ExplosionRoot explosionRoot = (ExplosionRoot)GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
            Explosion explosion = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.ExplosionUFO, pUFO.x, pUFO.y);
            explosion.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Invaders));
            explosion.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            explosionRoot.Add(explosion);
            TimerEventMan.Add(TimerEvent.Name.Explosion, new ExplosionAnimCmd(explosion), 0.1f);

            this.pUFO.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveUFOObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pUFO;
    }
}

// --- End of File ---
