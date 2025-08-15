//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveInvaderObserver : ColObserver
    {
        public RemoveInvaderObserver()
        {
            this.pInvader = null;
        }
        public RemoveInvaderObserver(RemoveInvaderObserver b)
        {
            Debug.Assert(b != null);
            this.pInvader = b.pInvader;
        }

        public override void Notify()
        {


            //this.pInvader = (InvaderCategory)this.pSubject.pObjB;
            if (this.pSubject.pObjA is InvaderCategory)
            {
                this.pInvader = (InvaderCategory)this.pSubject.pObjA;
            }
            else if (this.pSubject.pObjB is InvaderCategory)
            {
                this.pInvader = (InvaderCategory)this.pSubject.pObjB;
            }
            else
            {
                Debug.Assert(false);
            }
            Debug.Assert(this.pInvader != null);


            if (pInvader.bMarkForDeath == false)
            {
                pInvader.bMarkForDeath = true;
                //   Delay
                RemoveInvaderObserver pObserver = new RemoveInvaderObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
            else
            {
                pInvader.bMarkForDeath = true;
            }
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pInvader;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);
            GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);

            // Root - do not delete
            //GameObject pD = (GameObject)IteratorForwardComposite.GetParent(pC);

            if (pA.GetNumChildren() == 0)
            {
                InvaderGrid.DecreaseInvaderCount();
                ScoreManager.UpdateScore(pA.GetName());
                
                ExplosionRoot explosionRoot = (ExplosionRoot)GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
                Explosion explosion = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.ExplosionInvader, pA.x, pA.y);
                explosion.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Invaders));
                explosion.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
                explosionRoot.Add(explosion);
                TimerEventMan.Add(TimerEvent.Name.Explosion, new ExplosionAnimCmd(explosion), 0.1f);
                
                pA.Remove();

            }

            // Column 
            if (pB.GetNumChildren() == 0)
            {
                pB.Remove();
            }

            // Grid 
            if (pC.GetNumChildren() == 0)
            {
                InvaderGrid.NewLevel();
                //pC.Remove();
            }

        }
        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveInvaderObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pInvader;


    }
}

// --- End of File ---
