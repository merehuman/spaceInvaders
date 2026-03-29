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
                // Shared SpriteGame instances; reset tint before Remove so other uses of the same sprite look correct.
                if (pExplosion.pSpriteProxy != null && pExplosion.pSpriteProxy.pRealSprite != null)
                {
                    if (pExplosion.spriteName == SpriteGame.Name.ExplosionBomb)
                    {
                        pExplosion.pSpriteProxy.pRealSprite.SwapColor(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                    else if (pExplosion.spriteName == SpriteGame.Name.ExplosionShip)
                    {
                        pExplosion.pSpriteProxy.pRealSprite.SwapColor(0.0f, 1.0f, 0.0f, 1.0f);
                    }
                }

                pExplosion.Remove();
            }
        }

        Explosion pExplosion = null;
    }
}

// --- End of File ---
