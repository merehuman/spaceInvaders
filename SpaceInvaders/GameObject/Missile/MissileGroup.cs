//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class MissileGroup : Composite
    {
        public MissileGroup()
            : base()
        {
            this.name = Name.MissileGroup;

            this.poColObj.pColSprite.SetColor(0, 0, 1);
        }

        ~MissileGroup()
        {

        }

        public void RemoveActiveMissiles()
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
            // Important: at this point we have an MissileGroup
            // Call the appropriate collision reaction            
            other.VisitMissileGroup(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShieldRoot
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(b), this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldRoot
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
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

