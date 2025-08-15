//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class InvaderColumn : Composite
    {
        public InvaderColumn()
            : base()
        {
            this.name = Name.InvaderColumn;

            this.poColObj.pColSprite.SetColor(0, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            // BirdColumn vs ShipRoot
            //Debug.WriteLine("         collide:  {0} <-> {1}", s.name, this.name);

            // ShipRoot vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

        public override void Update()
        {
            // Debug.WriteLine("update: {0}", this);
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Column:");

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            // Walk through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();
                Debug.Assert(pNode != null);

                pNode.Dump();
            }
        }

        private static SpriteGameProxyNull psSpriteGameProxyNull = new SpriteGameProxyNull();
    }
}

// --- End of File ---
