//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SampleCmd : Command
    {
        public SampleCmd(Ship ship)
        {
            this.Ship = ship;
        }

        public override void Execute(float deltaTime)
        {
            if (Ship.pSpriteProxy.pRealSprite.pImage.mName == Image.Name.Ship)
            {
                Ship.pSpriteProxy.pRealSprite.SwapImage(ImageMan.Find(Image.Name.ExplosionShip));
            }
            else if (Ship.pSpriteProxy.pRealSprite.pImage.mName == Image.Name.ExplosionShip)
            {
                Ship.pSpriteProxy.pRealSprite.SwapImage(ImageMan.Find(Image.Name.ExplosionShip2));
            }
            else if (Ship.pSpriteProxy.pRealSprite.pImage.mName == Image.Name.ExplosionShip2)
            {
                Ship.pSpriteProxy.pRealSprite.SwapImage(ImageMan.Find(Image.Name.Ship));
            }
        }

        private Ship Ship;
    }
}

// --- End of File ---
