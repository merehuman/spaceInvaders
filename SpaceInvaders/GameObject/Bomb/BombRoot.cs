//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class BombRoot : Composite
    {
        public BombRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 1, 1);
        }

        ~BombRoot()
        {
        }

        public void RemoveActiveBombs()
        {
            IteratorForwardComposite pFor = new IteratorForwardComposite(this);
            Component pNode = pFor.First();
            Component pTmp = null;

            while (!pFor.IsDone())
            {
                pTmp = pFor.Curr();
                pFor.Next();
                GameObject pGameObj = (GameObject)pTmp;
                pGameObj.Remove();
            }
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBombRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BombRoot vs ShieldRoot
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(m), this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldRoot
            ColPair.Collide(m, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }



        // Data: ---------------


    }
}

// --- End of File ---
