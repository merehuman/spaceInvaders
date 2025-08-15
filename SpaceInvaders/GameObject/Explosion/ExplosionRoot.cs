//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionRoot : Composite
    {
        public ExplosionRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 1, 1);
        }

        ~ExplosionRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BombRoot
            // Call the appropriate collision reaction            
            other.VisitExplosionRoot(this);
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
