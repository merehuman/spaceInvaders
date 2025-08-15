//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class InvaderCategory : Leaf
    {
        public enum Type
        {
            Crab,
            Octopus,
            Squid,
            UFO,
            
            Column,
            Grid,

            Unitialized
        }

        protected InvaderCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName,spriteName,_x,_y)
        {

        }
    }
}

// --- End of File ---
