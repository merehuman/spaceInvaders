//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class Explosion : ExplosionCategory
    {
        public Explosion(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ExplosionCategory.Type.Explosion)
        {
            this.x = posX;
            this.y = posY;
            //this.delta = 5.0f;

            this.poColObj.pColSprite.SetColor(1, 1, 0);
        }

        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove();
        }

        public override void Update()
        {
            base.Update();
            //this.y -= delta;

            base.BaseUpdateBoundingBox(this);
        }
        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }
        ~Explosion()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitExplosion(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        // Data
        public float delta;

    }
}

// --- End of File ---
