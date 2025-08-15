//-----------------------------------------------------------------
// drum class
//-----------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SE456 {
    public class Drum
    {
        private InvaderGrid pGrid;
        private float minBeatInterval;
        private float maxBeatInterval;
        private float minSpeed;
        private float maxSpeed;
        private float beatInterval;
        private float movementSpeed;

        public Drum(InvaderGrid grid, float maxBeat, float minBeat, float minMove, float maxMove)
        {
            this.pGrid = grid;
            this.maxBeatInterval = maxBeat;
            this.minBeatInterval = minBeat;
            this.minSpeed = minMove;
            this.maxSpeed = maxMove;
            this.beatInterval = maxBeat;
            this.movementSpeed = minMove;
        }

        public void Update()
        {
            int totalInvaders = InvaderGrid.GetTotalInvaders();
            //Debug.WriteLine("Total Invaders: {0}", totalInvaders);

            if (totalInvaders > 0)
            {
                //use lerp here instead?
                this.beatInterval = maxBeatInterval - ((maxBeatInterval - minBeatInterval) * (55 - totalInvaders) / 55);
                this.movementSpeed = minSpeed + ((maxSpeed - minSpeed) * (55 - totalInvaders) / 55);
            }

            //Debug.WriteLine("Beat Interval: {0}", beatInterval);
        }

        public float GetBeat()
        {
            return beatInterval;
        }
        public float GetSpeed()
        {
            return movementSpeed;
        }
    }
}


