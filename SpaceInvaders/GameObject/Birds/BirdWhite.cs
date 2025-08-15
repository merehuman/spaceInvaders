//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class BirdWhite : BirdCategory
    {
        public BirdWhite(SpriteGame.Name spriteName, float posX, float posY)
        : base(GameObject.Name.WhiteBird, spriteName, posX, posY)
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an WhiteBird
            // Call the appropriate collision reaction            
            other.VisitWhiteBird(this);
        }

        public override void Update()
        {
            base.Update();
        }

        // Data: ---------------

    }
}

// --- End of File ---
