//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ExplosionCategory : Leaf
    {
        public enum Type
        {
            Explosion,
            ExplosionRoot,
            Unitialized
        }

        protected ExplosionCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, ExplosionCategory.Type explosionType)
            : base(name, spriteName, posX, posY)
        {
            this.ExplosionType = explosionType;
        }

        // Data: ---------------
        ~ExplosionCategory()
        {
        }

        // this is just a placeholder, who knows what data will be stored here
        protected ExplosionCategory.Type ExplosionType;

    }
}

// --- End of File ---
