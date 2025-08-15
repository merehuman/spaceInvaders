//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class GridCmd : Command
    {
        public GridCmd(InvaderGrid grid, Drum pDrum)
        {
            Debug.Assert(grid != null);
            this.pGrid = grid;

            Debug.Assert(pDrum != null);
            this.pDrum = pDrum;
        }

        public override void Execute(float deltaTime)
        {
            //Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());
            if (pGrid != null)
            {
                count++;

                pGrid.MoveGrid();

                // Get updated beat interval from Drum
                float newDeltaTime = pDrum.GetBeat();

                // Schedule next movement with new beat interval
                TimerEventMan.Add(TimerEvent.Name.Grid, this, newDeltaTime);
            }

        }

        public int getCount()
        {
            return count;
        }

        private InvaderGrid pGrid;
        int count;
        private Drum pDrum;

    }
}

// --- End of File ---
