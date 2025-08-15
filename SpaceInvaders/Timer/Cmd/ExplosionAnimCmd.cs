//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionAnimCmd : Command
    {
        public ExplosionAnimCmd(Explosion explosion)
        {
            this.pExplosion = explosion;
        }

        override public void Execute(float deltaTime)
        {
             if (pExplosion != null)
            {
                pExplosion.Remove();
            }
        }

        Explosion pExplosion = null;
    }
}

// --- End of File ---
