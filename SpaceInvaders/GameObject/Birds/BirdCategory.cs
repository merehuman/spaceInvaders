//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class BirdCategory : Leaf
    {
        public enum Type
        {
            Red,
            Yellow,
            Green,
            White,
            
            Column,
            Grid,

            Unitialized
        }

        protected BirdCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName,spriteName,_x,_y)
        {

        }
    }
}

// --- End of File ---
