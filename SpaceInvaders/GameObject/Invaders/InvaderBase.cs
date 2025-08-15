//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract class InvaderBase : Leaf
    {
        public enum Type
        {
            InvaderGrid,
            InvaderColumn,


            Crab,
            Octopus,
            Squid,
        }

        protected InvaderBase(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName,spriteName,_x,_y)
        {

        }
    }
}

// --- End of File ---
