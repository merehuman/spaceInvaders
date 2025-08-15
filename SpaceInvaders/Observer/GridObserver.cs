//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class GridObserver : ColObserver
    {
        public GridObserver()
        {

        }
        override public void Notify()
        {
            //Debug.WriteLine("Grid_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // OK do some magic
            InvaderGrid pGrid = (InvaderGrid)this.pSubject.pObjA;

            WallCategory pWall = (WallCategory)this.pSubject.pObjB;
            if (pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                if (!pGrid.GetCollided()) // Only trigger the collision if it hasn't already collided
                {
                    pGrid.SetCollided(true);
                    pGrid.SetDelta(-1.0f);
                    //pGrid.SetY(20.0f);
                }
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                if (!pGrid.GetCollided()) // Only trigger the collision if it hasn't already collided
                {
                    pGrid.SetCollided(true);
                    pGrid.SetDelta(1.0f);
                    //pGrid.SetY(20.0f);
                }
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Bottom)
            {
                ShipRemoveObserver.count = 0;
            }
            else
            {
                Debug.Assert(false);
            }
            //some sorta bool here to only have wall collision happen once
        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.GridObserver;
        }


    }
}

// --- End of File ---
